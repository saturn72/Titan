using Saturn72.Common.App;

namespace Titan.Framework.App
{
    public class TestAppAlphaVersion : IAppVersion
    {
        public string Key
        {
            get { return "TestAppAlphaVersion"; }
        }

        public int Index
        {
            get { return 0; }
        }

        public bool IsLatest
        {
            get { return true; }
        }

        public bool Publish
        {
            get { return false; }
        }

        public AppVersionStatusType Status
        {
            get { return AppVersionStatusType.Alpha; }
        }
    }
}