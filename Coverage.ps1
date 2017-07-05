if($env:APPVEYOR_REPO_TAG -eq "true") 
{
    "do not publish coverall data on tag builds"
    return
}
nuget install OpenCover -Source  https://api.nuget.org/v3/index.json -Version 4.6.519 -OutputDirectory tools
nuget install coveralls.net -Source  https://api.nuget.org/v3/index.json -Version 0.7.0 -OutputDirectory tools

.\tools\OpenCover.4.6.519\tools\OpenCover.Console.exe -oldStyle -target:"C:\Program Files\dotnet\dotnet.exe" -targetargs:" test "".\src\UriBuilder.Fluent.UnitTests\UriBuilder.Fluent.UnitTests.csproj"" -f net461 --no-build" -register:user -filter:"+[UriBuilder*]* -[*Tests]*" -returntargetcode -output:opencover_results.xml

.\tools\coveralls.net.0.7.0\tools\csmacnz.Coveralls.exe --opencover -i .\opencover_results.xml