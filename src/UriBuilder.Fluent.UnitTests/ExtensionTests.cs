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
        public void WithPort()
        {
            var url = new UriBuilder().WithPort(22);
            Assert.Equal(url.Port, 22);
        }

        [Fact]
        public void WithHttps()
        {
            var url = new UriBuilder().UseHttps(true);
            Assert.Equal(url.Scheme, "https");
        }

        [Fact]
        public void WithHttp()
        {
            var url = new UriBuilder().UseHttps(false);
            Assert.Equal(url.Scheme, "http");
        }

        [Fact]
        public void WithScheme()
        {
            //the jesus scheme?
            var url = new UriBuilder().WithScheme("jesus");
            Assert.Equal(url.Scheme, "jesus");
        }

        [Fact]
        public void WithHost()
        {
            //the jesus scheme?
            var url = new UriBuilder().WithHost("yodawg.com");
            Assert.Equal(url.Host, "yodawg.com");
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
    }
}