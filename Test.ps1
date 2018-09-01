$basePath = $PSScriptRoot

& dotnet restore
& dotnet build -c Release

$netcoreTestPath = Join-Path -Path $basePath -ChildPath "src\Duracellko.GlobeTime.Services.Tests\bin\Release\netcoreapp2.1\Duracellko.GlobeTime.Services.Tests.dll"
$net471TestPath = Join-Path -Path $basePath -ChildPath "src\Duracellko.GlobeTime.Services.Tests\bin\Release\net471\Duracellko.GlobeTime.Services.Tests.dll"

& dotnet vstest $netcoreTestPath
& dotnet vstest $net471TestPath
