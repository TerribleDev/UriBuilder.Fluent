Get-ChildItem -Path $ENV:APPVEYOR_BUILD_FOLDER -Recurse –File -Filter project.json | foreach {
    $jsonFile = Get-Content $_.FullName -raw | ConvertFrom-Json
    if($jsonFile.version)
    {
        $jsonFile.version = $ENV:APPVEYOR_BUILD_VERSION
        $jsonFile | ConvertTo-Json -Depth 999 | Out-File $_.FullName
    }
}