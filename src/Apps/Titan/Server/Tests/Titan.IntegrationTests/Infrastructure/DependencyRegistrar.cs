using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moq;
using Saturn72.Common.Data.Repositories;
using Saturn72.Common.WebApi;
using Saturn72.Core.Configuration;
using Saturn72.Core.Domain;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;
using Saturn72.Core.Logging;
using Saturn72.Core.Services.Events;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Clients;
using Titan.Common.Domain.Testing;
using Titan.DB.Model.Repositories;
using Titan.IntegrationTests.TestObjects;
using Titan.Server.Services.Clients;
using Titan.Server.Services.Execution;
using Titan.Server.Services.Testing;

namespace Titan.IntegrationTests.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
            return reg =>
            {
                RegisterComponents(reg);
                RegisterServices(reg);
                RegisterRepositories(reg);
                RegisterApiControllers(reg, typeFinder);
            };
        }

        public int RegistrationOrder => 100;

        private void RegisterComponents(IIocRegistrator reg)
        {
            reg.RegisterType<MemoryLogger, ILogger>(LifeCycle.SingleInstance);
            reg.RegisterType<DummyEventPublisher, IEventPublisher>(LifeCycle.SingleInstance);
        }

        private void RegisterRepositories(IIocRegistrator reg)
        {
            var autoClients = new List<AutomationClientDomainModel>
            {
                new AutomationClientDomainModel {Id = 1},
                new AutomationClientDomainModel {Id = 2},
                new AutomationClientDomainModel {Id = 3},
                new AutomationClientDomainModel {Id = 4}
            };
            var autoClientUOW = BuildUnitOfWork<AutomationClientDomainModel, long>(autoClients);
            reg.RegisterInstance(autoClientUOW, LifeCycle.SingleInstance);
            reg.RegisterType<AutomationClientRepository, IAutomationClientRepository>(LifeCycle.PerDependency);


            var testUOW =
                BuildUnitOfWork<TestDomainModel, long>(new List<TestDomainModel> {new TestDomainModel {Id = 1}});
            reg.RegisterInstance(testUOW, LifeCycle.SingleInstance);
            reg.RegisterType<TestRepository, ITestRepository>(LifeCycle.PerDependency);
        }

        private IUnitOfWork<TDomainModel, TId> BuildUnitOfWork<TDomainModel, TId>(ICollection<TDomainModel> collection)
            where TDomainModel : DomainModelBase<TId>
        {
            var random = new Random();

            var uow = new Mock<IUnitOfWork<TDomainModel, TId>>();
            uow.Setup(u => u.GetById(It.IsAny<TId>()))
                .Returns<long>(
                    id => SleepAndRunFunc(random.Next(10, 3000), () => collection.FirstOrDefault(e => e.Id.Equals(id))));

            return uow.Object;
        }

        private T SleepAndRunFunc<T>(int threadSleepTime, Func<T> func)
        {
            Thread.Sleep(threadSleepTime);
            return func();
        }

        private void RegisterServices(IIocRegistrator reg)
        {
            reg.RegisterType<AutomationClientService, IAutomationClientService>(LifeCycle.PerDependency);
            reg.RegisterType<TestService, ITestService>(LifeCycle.PerDependency);
            reg.RegisterType<ExecutionService, IExecutionService>(LifeCycle.PerDependency);
        }

        private void RegisterApiControllers(IIocRegistrator registrator, ITypeFinder typeFinder)
        {
            var allApiControllers = typeFinder.FindClassesOfType<Saturn72ApiControllerBase>();
            registrator.RegisterTypes(LifeCycle.PerRequest, allApiControllers.ToArray());
        }
    }
}