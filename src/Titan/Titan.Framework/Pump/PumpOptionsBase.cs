
using System.Collections.Generic;

namespace Titan.Framework.Pump
{
    public abstract class PumpOptionsBase
    {
        private IDictionary<string, object> _paramters;

        public IDictionary<string, object> Parameters
        {
            get { return _paramters ?? (_paramters = new Dictionary<string, object>()); }
            protected set { _paramters = value; }
        }
    }
}