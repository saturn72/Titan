using System;
using Moq;
using Saturn72.Core.Configuration;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;
using Saturn72.Core.Services.Events;

namespace Titan.Framework.SystemTests.Throws
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int RegistrationOrder => 100;

        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
            return reg =>
            {
                var ep = new Mock<IEventPublisher>();
                reg.RegisterInstance(ep.Object);
            };
        }
    }
}