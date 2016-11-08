using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class TerribleDevUriExtensions
    {
        public static UriBuilder WithParameter(this UriBuilder bld, string key, string value)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if(string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            if(!string.IsNullOrWhiteSpace(bld.Query))
            {
                bld.Query += $"&{key}={value}";
                return bld;
            }
            bld.Query = $"{key}={value}";
            return bld;
        }

        public static UriBuilder WithParameter(this UriBuilder bld, string key, IEnumerable<object> values)
        {
            var isfirst = string.IsNullOrWhiteSpace(bld.Query);
            var intitialValue = isfirst ? "?" : $"{bld.Query}& ";
            var sb = new StringBuilder($"{intitialValue}{key}=");
            foreach(var value in values)
            {
                var toSValue = value?.ToString();
                if(string.IsNullOrWhiteSpace(toSValue)) continue;
                sb.Append($"{value},");
            }
            bld.Query = sb.ToString().TrimEnd(',');
            return bld;
        }

        public static UriBuilder WithPort(this UriBuilder bld, int port)
        {
            if(port < 1) throw new ArgumentOutOfRangeException(nameof(port));
            bld.Port = port;
            return bld;
        }

        public static UriBuilder WithPathSegment(this UriBuilder bld, string pathSegment)
        {
            var path = pathSegment.TrimStart('/');
            if(string.IsNullOrWhiteSpace(bld.Path))
            {
                bld.Path = path;
                return bld;
            }
            bld.Path = $"{bld.Path.TrimEnd('/')}/{path}";
            return bld;
        }

        public static UriBuilder WithScheme(this UriBuilder bld, string scheme)
        {
            if(string.IsNullOrWhiteSpace(scheme)) throw new ArgumentNullException(nameof(scheme));
            bld.Scheme = scheme;
            return bld;
        }

        public static UriBuilder WithHost(this UriBuilder bld, string host)
        {
            if(string.IsNullOrWhiteSpace(host)) throw new ArgumentNullException(nameof(host));
            bld.Host = host;
            return bld;
        }

        public static UriBuilder UseHttps(this UriBuilder bld, bool predicate = true)
        {
            if(predicate) bld.Scheme = "https";
            return bld;
        }
    }
}