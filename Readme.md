## FluentUriBuilder

This lets you do things like


```csharp

new UriBuilder()
         .WithParameter("awesome", "yodawg")
         .WithParameter("fun", ["cool", "yay"])
         .WithHost("awesome.com")
         .WithPathSegment("seg")
         .UseHttps()
         .ToString()

result: https://awesome.com/seg?awesome=yodawg&fun=cool,yay

```

or 

```csharp

new UriBuilder("https://awesome.com/yo)
    .WithParameter("id", "5")
    .ToString();

result: https://awesome.com/yo?id=5 

```