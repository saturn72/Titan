using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Newtonsoft.Json;

namespace Titan.Framework.Lifetime.Interceptors
{
    public sealed class InvocationUtil
    {
        internal static string ExtractMethodParameters(IInvocation invocation)
        {
            var paramNames = invocation.Method.GetParameters().Select(p => p.Name).ToArray();

            var result = new Dictionary<string, object>();
            for (var i = 0; i < paramNames.Length; i++)
                result.Add(paramNames[i], invocation.Arguments[i] ?? "");

            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }
    }
}