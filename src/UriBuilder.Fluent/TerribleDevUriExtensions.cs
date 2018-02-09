using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class TerribleDevUriExtensions
    {
        /// <summary>
        /// Appends a query string parameter with a key, and many values. Multiple values will be comma seperated. If only 1 value is passed and its null or value, the key will be added to the QS.
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder bld, string key, params string[] values) => bld.WithParameter(key, valuesEnum: values);

        /// <summary>
        /// Appends query strings from dictionary
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="parameterDictionary"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder bld, IDictionary<string, string> parameterDictionary)
        {
            return bld.WithParameter((IEnumerable <KeyValuePair<string, string>>)parameterDictionary);
        }

        /// <summary>
        /// Appends query strings from an enumerable of key-value pairs
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="parameterEnumerable"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder bld, IEnumerable<KeyValuePair<string, string>> parameterEnumerable)
        {
            if (parameterEnumerable == null) throw new ArgumentNullException(nameof(parameterEnumerable));
            foreach (var item in parameterEnumerable)
            {
                bld.WithParameter(item.Key, item.Value);
            }
            return bld;
        }

        /// <summary>
        /// Appends a query string parameter with a key, and many values. Multiple values will be comma seperated. If only 1 value is passed and its null or value, the key will be added to the QS.
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="key"></param>
        /// <param name="valuesEnum"></param>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder bld, string key, IEnumerable<object> valuesEnum)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if(valuesEnum == null)
            {
                valuesEnum = new string[0];
            }
            var intitialValue = string.IsNullOrWhiteSpace(bld.Query) ? "" : $"{bld.Query.TrimStart('?')}&";
            bld.Query = intitialValue.AppendKeyValue(key, valuesEnum);
            return bld;
        }

        /// <summary>
        /// Appends a fragments parameter with a key, and many values. Multiple values will be comma seperated. If only 1 value is passed and its null or value, the key will be added to the fragment.
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="key"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static UriBuilder WithFragment(this UriBuilder bld, string key, params string[] values) => bld.WithFragment(key, valuesEnum: values);

        /// <summary>
        /// Appends fragments from dictionary
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="fragmentDictionary"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithFragment(this UriBuilder bld, IDictionary<string, string> fragmentDictionary)
        {
            if(fragmentDictionary == null) throw new ArgumentNullException(nameof(fragmentDictionary));
            foreach(var item in fragmentDictionary)
            {
                bld.WithFragment(item.Key, item.Value);
            }
            return bld;
        }

        /// <summary>
        /// Appends a fragments with a key, and many values. Multiple values will be comma seperated. If only 1 value is passed and its null or value, the key will be added to the fragment.
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="key"></param>
        /// <param name="valuesEnum"></param>
        /// <returns></returns>
        public static UriBuilder WithFragment(this UriBuilder bld, string key, IEnumerable<object> valuesEnum)
        {
            if(string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if(valuesEnum == null)
            {
                valuesEnum = new string[0];
            }
            var intitialValue = string.IsNullOrWhiteSpace(bld.Fragment) ? "" : $"{bld.Fragment.TrimStart('#')}&";
            bld.Fragment = intitialValue.AppendKeyValue(key, valuesEnum);
            return bld;
        }

        /// <summary>
        /// Sets the port to be the port number
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="port"></param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if port is less than one</exception>
        /// <returns></returns>
        public static UriBuilder WithPort(this UriBuilder bld, int port)
        {
            if(port < 1) throw new ArgumentOutOfRangeException(nameof(port));
            bld.Port = port;
            return bld;
        }

        /// <summary>
        /// Removes the port number for default ports
        /// </summary>
        /// <param name="bld"></param>
        /// <returns></returns>
        public static UriBuilder WithoutDefaultPort(this UriBuilder bld)
        {
            if (bld.Uri.IsDefaultPort) bld.Port = -1;
            return bld;
        }

        /// <summary>
        /// appends a path segment to the path. Can be called multiple times to append multiple segments
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="pathSegment"></param>
        /// <exception cref="ArgumentNullException">You pass a string as a path segment</exception>
        /// <returns></returns>
        public static UriBuilder WithPathSegment(this UriBuilder bld, string pathSegment)
        {
            if(string.IsNullOrWhiteSpace(pathSegment))
            {
                throw new ArgumentNullException(nameof(pathSegment));
            }
            var path = pathSegment.TrimStart('/');
            bld.Path = $"{bld.Path.TrimEnd('/')}/{path}";
            return bld;
        }

        /// <summary>
        /// Sets your Uri Scheme
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="scheme"></param>
        /// <exception cref="ArgumentNullException">You must pass a scheme</exception>
        /// <returns></returns>
        public static UriBuilder WithScheme(this UriBuilder bld, string scheme)
        {
            if(string.IsNullOrWhiteSpace(scheme)) throw new ArgumentNullException(nameof(scheme));
            bld.Scheme = scheme;
            return bld;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="host"></param>
        /// <exception cref="ArgumentNullException">You must pass a ho0st</exception>
        /// <returns></returns>
        public static UriBuilder WithHost(this UriBuilder bld, string host)
        {
            if(string.IsNullOrWhiteSpace(host)) throw new ArgumentNullException(nameof(host));
            bld.Host = host;
            return bld;
        }

        public static string PathAndQuery(this UriBuilder bld) => (bld.Path + bld.Query);

        /// <summary>
        /// Use Https?
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="predicate">default true, if false sets scheme to http</param>
        /// <returns></returns>
        public static UriBuilder UseHttps(this UriBuilder bld, bool predicate = true)
        {
            bld.Scheme = predicate ? "https" : "http";
            return bld;
        }

        /// <summary>
        /// Escape Url query string
        /// </summary>
        /// <param name="bld"></param>
        /// <returns></returns>
        public static string ToEscapeString(this UriBuilder bld) => Uri.EscapeUriString(bld.Uri.ToString());

        /// <summary>
        /// Escape the whole Url string
        /// </summary>
        /// <param name="bld"></param>
        /// <returns></returns>
        public static string ToEscapeDataString(this UriBuilder bld) => Uri.EscapeDataString(bld.Uri.ToString());

        /// <summary>
        /// Appends x-www-form-urlencoded key and valuesEnum into initialValue.
        /// </summary>
        private static string AppendKeyValue(this string intitialValue, string key, IEnumerable<object> valuesEnum)
        {
            var sb = new StringBuilder($"{intitialValue}{key}");
            var validValueHit = false;
            foreach(var value in valuesEnum)
            {
                var toSValue = value?.ToString();
                if(string.IsNullOrWhiteSpace(toSValue)) continue;
                // we can't just have an = sign since its valid to have query string paramters with no value;
                if(!validValueHit) toSValue = "=" + value;
                validValueHit = true;
                sb.Append($"{toSValue},");
            }
            return  sb.ToString().TrimEnd(',');
        }
    }
}