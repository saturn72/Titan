
using System;
using Calculator.Test.Framework.Selenium.Module;
using Calculator.Test.Framework.SystemBlocks;
using Saturn72.Core.Configuration;
using Saturn72.Core.Infrastructure.AppDomainManagement;
using Saturn72.Core.Infrastructure.DependencyManagement;

namespace Calculator.Test.Framework.Infrastructure
{
public     class DependecyRegistrar:IDependencyRegistrar
    {
        public int RegistrationOrder
        {
            get { return 100; }
        }

        public Action<IIocRegistrator> RegistrationLogic(ITypeFinder typeFinder, Saturn72Config config)
        {
         return reg =>
         {
             reg.RegisterType<SeleniumCalculator, ICalculator>(LifeCycle.PerDependency);
         };
        }
    }
}
