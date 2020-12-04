# updates prerelease dependencies for packages listed below as $packages
# expected to be run where working directory is the root of the repo

$packages = "Microsoft.Graph.Beta","Microsoft.Graph.Auth"

$projectFile = (Resolve-Path "msgraph-sdk-raptor-compiler-lib\msgraph-sdk-raptor-compiler-lib.csproj").Path

foreach ($package in $packages)
{
    dotnet remove $projectFile package $package
    dotnet add $projectFile package $package --prerelease
}