#region

using System;
using System.Linq;
using System.Web.Http.Tracing;
using Saturn72.Common.Data.Repositories;
using Saturn72.Common.WebApi;
using Saturn72.Core.Configuration;
using Saturn72.Core.Configuration.Maps;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;
using Saturn72.Module.EntityFramework;
using Titan.Common.Data.Repositories;
using Titan.Common.Domain.Clients;
using Titan.DB.Model.Models.Server;
using Titan.DB.Model.Repositories;
using Titan.Server.Services.Clients;

#endregion

namespace Titan.Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int RegistrationOrder => 100;

        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
            return reg =>
            {
                reg.RegisterType<SystemDiagnosticsTraceWriter, ITraceWriter>(LifeCycle.SingleInstance);

                RegisterApiControllers(reg, typeFinder);

                RegisterRepositories(reg);

                RegisterServices(reg);
            };
        }

        private void RegisterServices(IIocRegistrator reg)
        {
            reg.RegisterType<AutomationClientService, IAutomationClientService>(LifeCycle.PerRequest);
        }

        private void RegisterRepositories(IIocRegistrator reg)
        {
            //Get connecrtion strings
            const string connectionStringKey = "TitanServerDB";
            var c = ConfigManager.GetConfigMap<DefaultConfigMap>("Default");
            var connectionString = c.ConnectionStrings[connectionStringKey];

            if (connectionString == null)
                throw new NullReferenceException("Missing connectionString for " + connectionStringKey);


            reg.Register<IUnitOfWork<AutomationClientDomainModel, long>>(
                () => new EfUnitOfWork<AutomationClientDomainModel, long, AutomationClient>(connectionString.Name),
                LifeCycle.PerDependency);
            reg.RegisterType<AutomationClientRepository, IAutomationClientRepository>(LifeCycle.PerDependency);


            //reg.RegisterType<ExpressionRepository, IRepository<ExpressionDomainModel, long>>(LifeCycle.PerRequest);
        }

        private void RegisterApiControllers(IIocRegistrator registrator, ITypeFinder typeFinder)
        {
            var allApiControllers = typeFinder.FindClassesOfType<Saturn72ApiControllerBase>();
            registrator.RegisterTypes(LifeCycle.PerRequest, allApiControllers.ToArray());
        }
    }
}