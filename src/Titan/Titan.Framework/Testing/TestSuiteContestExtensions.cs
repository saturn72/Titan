using System.Linq;

namespace Titan.Framework.Testing
{
    public static class TestSuiteContestExtensions
    {
        public static TestContext CurrentTestContext(this TestSuiteContext tsc)
        {
            return tsc.TestContexts.LastOrDefault();
        }
    }
}