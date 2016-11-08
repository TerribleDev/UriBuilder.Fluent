using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class TerribleDevUriExtensions
    {
        public static void WithParameter(this UriBuilder bld, string key, string value)
        {
            if(!string.IsNullOrWhiteSpace(bld.Query))
            {
                bld.Query += $"&{key}={value}";
                return;
            }
            bld.Query = $"{key}={value}";
        }

        public static void WithParameter(this UriBuilder bld, string key, IEnumerable<object> values)
        {
            var isfirst = string.IsNullOrWhiteSpace(bld.Query);
            var intitialValue = isfirst ? "?" : "&";
            var sb = new StringBuilder($"{intitialValue}{key}=");
            foreach(var value in values)
            {
                sb.Append($"{value.ToString()},");
            }
            bld.Query = sb.ToString().TrimEnd(',');
        }
    }
}