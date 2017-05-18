using System.Collections.Generic;
using Saturn72.Common.App;

namespace Titan.Framework.App
{
    public class TestApp : Saturn72AppBase
    {
        private const string ApplicationName = "test-app";


        public TestApp() : base(ApplicationName)
        {
        }

        public override string Name => ApplicationName;

        public override IEnumerable<IAppVersion> Versions { get; } = new[] {new TestAppAlphaVersion()};
    }
}