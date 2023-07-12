clean:
	dotnet clean && rm -rf ./**/bin ./**/obj && rm -rf output
build: clean restore
	dotnet build --no-restore
restore: clean
	dotnet restore
test:
	dotnet test /p:CollectCoverage=true
release-build: clean restore
	dotnet build -c Release --no-restore 
pack: release-build
	dotnet pack src/UriBuilder.Fluent/UriBuilder.Fluent.csproj --configuration Release --output output
publish:
	dotnet nuget push output/*.nupkg --source https://api.nuget.org/v3/index.json --skip-duplicate --api-key $(NUGET_API_KEY)
	dotnet nuget push output/*.snupkg --source https://api.nuget.org/v3/index.json --skip-duplicate --api-key $(NUGET_API_KEY)
coveralls-push:
	dotnet tool install -g coveralls.net
	coverallsnet --opencover -i UriBuilder.Fluent.UnitTests/coverage.opencover.xml --useRelativePaths
check-format:
	dotnet format --verify-no-changes
default: clean build