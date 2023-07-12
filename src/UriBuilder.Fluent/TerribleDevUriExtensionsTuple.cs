using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static partial class TerribleDevUriExtensions
    {
        /// <summary>
        /// Appends query strings from dictionary
        /// </summary>
        /// <param name="bld"></param>
        /// <param name="parameterDictionary"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static UriBuilder WithParameter(this UriBuilder bld, IEnumerable<(string, string)> parameterDictionary)
        {
            if (bld == null)
            {
                throw new ArgumentNullException(nameof(bld));
            }
            if (parameterDictionary == null) throw new ArgumentNullException(nameof(parameterDictionary));
            foreach (var item in parameterDictionary)
            {
                bld.WithParameter(item.Item1, item.Item2);
            }
            return bld;
        }
    }
}