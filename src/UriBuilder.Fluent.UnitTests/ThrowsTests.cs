using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentUriBuilder.Tests
{
    public class ThrowsTests
    {
        [Fact]
        public void ThrowsArgNull()
        {
            var tstObj = new UriBuilder();
            Assert.Throws<ArgumentNullException>(() => tstObj.WithParameter(string.Empty, string.Empty));
            Assert.Throws<ArgumentNullException>(() => tstObj.WithFragment(string.Empty, string.Empty));
            Assert.Throws<ArgumentNullException>(() => tstObj.WithPathSegment(null));
            Assert.Throws<ArgumentNullException>(() => tstObj.WithScheme(null));
            Assert.Throws<ArgumentNullException>(() => tstObj.WithHost(null));
            Assert.Throws<ArgumentNullException>(() => tstObj.WithParameter((IEnumerable<KeyValuePair<string, string>>)null));
            Assert.Throws<ArgumentNullException>(() => tstObj.WithFragment(fragmentDictionary: null));
            Assert.Throws<ArgumentOutOfRangeException>(() => tstObj.WithPort(-1));
            UriBuilder nullPtr = null;
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithFragment("yo", "dawg"));
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithFragment(new Dictionary<string, string>()));
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithParameter("yo", "dawg"));
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithPathSegment("yo"));
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithScheme("yo"));
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithFragment("yo"));
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithoutDefaultPort());
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithoutDefaultPort());
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithPort(1));
            Assert.Throws<ArgumentNullException>(() => nullPtr.WithHost("yo"));
            Assert.Throws<ArgumentNullException>(() => nullPtr.UseHttps());
        }
    }
}