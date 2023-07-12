using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FluentUriBuilder.Tests
{
    public class ExtensionTests
    {
        [Theory]
        [InlineData("/awesome", "awesome")]
        [InlineData("/////awesome", "awesome")]
        [InlineData("awesome/", "awesome/")]
        public void PathDoesNotGetMultipleAppends(string pathWithSlashes, string expectedPath)
        {
            var url = new UriBuilder("http://awesome.com")
                .WithPathSegment(pathWithSlashes);
            Assert.Equal("http://awesome.com/" + expectedPath, url.Uri.ToString());
        }

        [Fact]
        public void MultiplePathSegementsWork()
        {
            var url = new UriBuilder("http://awesome.com")
                .WithPathSegment("yodawg")
                .WithPathSegment("/immahslash/");
            Assert.Equal("http://awesome.com/yodawg/immahslash/", url.Uri.ToString());
        }

        [Fact]
        public void TestAddUrlParameter()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("awesome", "yodawg");
            Assert.Equal("http://awesome.com/?awesome=yodawg", url.Uri.ToString());
        }

        [Fact]
        public void PathAndQuery()
        {
            var url = new UriBuilder().WithPathSegment("/awesome/v1/").WithParameter("awesome", "cool").PathAndQuery();
            Assert.Equal("/awesome/v1/?awesome=cool", url);
            url = new UriBuilder().WithPathSegment("/awesome/v1").WithParameter("awesome", "cool").PathAndQuery();
            Assert.Equal("/awesome/v1?awesome=cool", url);
            url = new UriBuilder().WithPathSegment("/awesome/v1").PathAndQuery();
            Assert.Equal("/awesome/v1", url);
        }

        [Fact]
        public void TestAddParameterArray()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("awesome", "cool", "dawg");
            Assert.Equal("http://awesome.com/?awesome=cool,dawg", url.Uri.ToString());
        }

        [Fact]
        public void TestAddParameterArrayint()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("awesome", new List<int>() { 1, 2 }.Cast<object>());
            Assert.Equal("http://awesome.com/?awesome=1,2", url.Uri.ToString());
        }

        [Fact]
        public void TestAddParameterNoValue()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("awesome");
            Assert.Equal("http://awesome.com/?awesome", url.Uri.ToString());
        }

        [Fact]
        public void TestAddFragmentArray()
        {
            var url = new UriBuilder("http://awesome.com")
                .WithFragment("awesome", "cool", "dawg");
            Assert.Equal("http://awesome.com/#awesome=cool,dawg", url.Uri.ToString());
        }

        [Fact]
        public void TestAddFragmentArrayint()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithFragment("awesome", new List<int>() { 1, 2 }.Cast<object>());
            Assert.Equal("http://awesome.com/#awesome=1,2", url.Uri.ToString());
        }

        [Fact]
        public void TestAddFragmentNoValue()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithFragment("awesome");
            Assert.Equal("http://awesome.com/#awesome", url.Uri.ToString());
        }

        [Fact]
        public void WithPort()
        {
            var url = new UriBuilder().WithPort(22);
            Assert.Equal(22, url.Port);
        }

        [Fact]
        public void WithoutDefaultPort()
        {
            var url = new UriBuilder("http://awesome.com:80")
                .WithoutDefaultPort();
            Assert.Equal("http://awesome.com/", url.Uri.ToString());

            url = new UriBuilder("http://awesome.com:443")
                .WithoutDefaultPort();
            Assert.Equal("http://awesome.com:443/", url.Uri.ToString());

            url = new UriBuilder("https://awesome.com:443")
                .WithoutDefaultPort();
            Assert.Equal("https://awesome.com/", url.Uri.ToString());
        }

        [Fact]
        public void WithHttps()
        {
            var url = new UriBuilder().UseHttps(true);
            Assert.Equal("https", url.Scheme);
        }

        [Fact]
        public void WithHttp()
        {
            var url = new UriBuilder().UseHttps(false);
            Assert.Equal("http", url.Scheme);
        }

        [Fact]
        public void WithScheme()
        {
            //the jesus scheme?
            var url = new UriBuilder().WithScheme("jesus");
            Assert.Equal("jesus", url.Scheme);
        }

        [Fact]
        public void WithHost()
        {
            //the jesus scheme?
            var url = new UriBuilder().WithHost("yodawg.com");
            Assert.Equal("yodawg.com", url.Host);
        }

        [Fact]
        public void AddParamWithNoValue()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("awesome", "yodawg")
                    .WithParameter("fun", null)
                    .WithParameter("cool", string.Empty);
            Assert.Equal("http://awesome.com/?awesome=yodawg&fun&cool", url.Uri.ToString());
        }

        [Fact]
        public void TestAddTwoUrlParameters()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("awesome", "yodawg")
                    .WithParameter("supg", "no2")
                    .WithParameter("supgf", "no22");
            Assert.Equal("http://awesome.com/?awesome=yodawg&supg=no2&supgf=no22", url.Uri.ToString());
        }

        [Fact]
        public void AddDictOfParams()
        {
            var dictionary = new Dictionary<string, string>()
            {
                ["yo"] = "dawg",
                ["troll"] = "toll",
                ["hammer"] = string.Empty
            };
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter(dictionary);
            Assert.Equal("http://awesome.com/?yo=dawg&troll=toll&hammer", url.Uri.ToString());
        }

        [Fact]
        public void AddFragmentWithNoValue()
        {
            var url = new UriBuilder("http://awesome.com")
                .WithFragment("awesome", "yodawg")
                .WithFragment("fun", null)
                .WithFragment("cool", string.Empty);
            Assert.Equal("http://awesome.com/#awesome=yodawg&fun&cool", url.Uri.ToString());
        }

        [Fact]
        public void TestAddTwoUrlFragments()
        {
            var url = new UriBuilder("http://awesome.com")
                .WithFragment("awesome", "yodawg")
                .WithFragment("supg", "no2")
                .WithFragment("supgf", "no22");
            Assert.Equal("http://awesome.com/#awesome=yodawg&supg=no2&supgf=no22", url.Uri.ToString());
        }

        [Fact]
        public void AddDictOfFragments()
        {
            var dictionary = new Dictionary<string, string>()
            {
                ["yo"] = "dawg",
                ["troll"] = "toll",
                ["hammer"] = string.Empty
            };
            var url = new UriBuilder("http://awesome.com")
                .WithFragment(dictionary);
            Assert.Equal("http://awesome.com/#yo=dawg&troll=toll&hammer", url.Uri.ToString());
        }

        [Fact]
        public void TestToEscapedString()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("yo", "dawg<");
            Assert.Equal("http://awesome.com/?yo=dawg%3C", url.ToEscapeString());
        }

        [Fact]
        public void TestToEscapedDataString()
        {
            var url = new UriBuilder("http://awesome.com")
                    .WithParameter("yo", "dawg<");
            Assert.Equal("http%3A%2F%2Fawesome.com%2F%3Fyo%3Ddawg%3C", url.ToEscapeDataString());
        }
    }
}