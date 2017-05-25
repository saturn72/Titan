using System;
using Saturn72.Core.Configuration;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;

namespace Titan.Framework.SystemTests
{
    public class DependencyRegistrar:IDependencyRegistrar
    {
        public int RegistrationOrder => 10;
        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
            return reg => reg.RegisterType<DummyCommander, IDummyCommander1>(LifeCycle.PerDependency);
        }
    }
}
