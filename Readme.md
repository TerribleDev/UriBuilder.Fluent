# UriBuilder.Fluent
[![Coverage Status](https://coveralls.io/repos/github/TerribleDev/UriBuilder.Fluent/badge.svg)](https://coveralls.io/github/TerribleDev/UriBuilder.Fluent) [![Build status](https://ci.appveyor.com/api/projects/status/cp704w3bgaerufxm/branch/master?svg=true)](https://ci.appveyor.com/project/tparnell8/uribuilder-fluent/branch/master)

This places extension methods over System.UriBuilder to help deal with query string parameters, and create more of a fluent interface. Unlike other projects, this NetStandardLibrary compliant package builds ontop of trusty UriBuilder, does not use custom Uri generators, or have outside dependencies. Unit tests continue to be a first class citizen!

## Getting started

Just install the nuget package `install-package UriBuilder.Fluent` and thats it. The extension methods should be available to you!

## Code Example

This lets you do things like

```csharp

new UriBuilder()
         .WithParameter("awesome", "yodawg")
         .WithParameter("fun", ["cool", "yay"])
         .WithHost("awesome.com")
         .WithPathSegment("seg")
         .UseHttps()
         .Uri
         .ToString()

```
result: `https://awesome.com/seg?awesome=yodawg&fun=cool,yay`

or 

```csharp

new UriBuilder("https://awesome.com/yo)
    .WithParameter("id", "5")
    .Uri
    .ToString();

```
result: `https://awesome.com/yo?id=5`

you can even pass a dictionary of parameters

```csharp

var dictionary = new Dictionary<string, string>()
{
    ["yo"] = "dawg"
};

new UriBuilder("http://awesome.com")
        .WithParameter(dictionary);

```
result: `http://awesome.com/?yo=dawg`

### ToEscapeString() vs. ToEscapeDataString()

There are two extension methods for performing Uri encoding.

#### ToEscapeString() 
Performs encoding only on the Uri query string and returns the Uri as a string.

```csharp

var uri = new UriBuilder("https://awesome.com")
    .WithParameter("yo", "dawg<")
    .ToEscapeString();

```

result: `http://awesome.com/?yo=dawg%3C`

#### ToEscapeDataString() 
Performs encoding on the whole Uri and returns the Uri as a string.

```csharp

var uri = new UriBuilder("https://awesome.com")
    .WithParameter("yo", "dawg<")
    .ToEscapeDataString();

```

result: `http%3A%2F%2Fawesome.com%2F%3Fyo%3Ddawg%3C`

