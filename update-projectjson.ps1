$projectJsonFileLocation = "src/UriBuilder.Fluent/project.json"
$newVersion = $env:APPVEYOR_REPO_TAG_NAME
if($newVersion -eq $null)
{
 $newVersion = "1.0.0-${env:APPVEYOR_BUILD_VERSION}"
}
if($newVersion -eq $null)
{
  return
}

Write-Host "$projectJsonFileLocation will be update with new version '$newVersion'"

$json = (Get-Content $projectJsonFileLocation -Raw) | ConvertFrom-Json
$json.version = $newVersion
$json | ConvertTo-Json -depth 100 | Out-File $projectJsonFileLocation