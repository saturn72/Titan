#region

using System.Collections.Generic;
using System.Linq;
using Saturn72.Common.App;
using Saturn72.Extensions;

#endregion

namespace Titan.Framework.Apps
{
    public class TitanApp : Saturn72AppBase
    {
        private const string AppName = "titan_server";
        private readonly IEnumerable<IAppVersion> _versions;

        public TitanApp() : base(AppName)
        {
            _versions = LoadVersions();
            LatestVersion = _versions.First(v => v.IsLatest);
        }

        public override string Name
        {
            get { return AppName; }
        }

        public override IEnumerable<IAppVersion> Versions
        {
            get { return _versions; }
        }

        public override IAppVersion LatestVersion { get; }

        private IEnumerable<IAppVersion> LoadVersions()
        {
            var versions = new IAppVersion[]
            {
                new CalculatorVersion1()
            };

            Guard.MustFollow(() => versions.Count(v => v.IsLatest) == 1,
                "Multiple versions marked as latest, Or noversion is marked as latest");

            return versions;
        }
    }
}