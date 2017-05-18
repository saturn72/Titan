using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Saturn72.Common.App;

namespace Titan.Framework.App
{
    public class TestApp : Saturn72AppBase
    {
        private const string ApplicationName = "test-app";
        private readonly IEnumerable<IAppVersion> _versions = new[] {new TestAppAlphaVersion()};


        public TestApp() : base(ApplicationName)
        {
        }

        public override string Name
        {
            get { return ApplicationName; }
        }

        public override IEnumerable<IAppVersion> Versions
        {
            get { return _versions; }
        }
    }
}