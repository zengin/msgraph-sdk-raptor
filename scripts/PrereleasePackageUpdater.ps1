# updates prerelease dependencies for packages listed below as $packages
# expected to be run where working directory is the root of the repo

$packages = "Microsoft.Graph.Beta","Microsoft.Graph.Auth"

$projectFile = "msgraph-sdk-raptor-compiler-lib\msgraph-sdk-raptor-compiler-lib.csproj"
$fullFileName = Resolve-Path $projectFile

Write-Host "Searching latest updates including prerelease for $projectFile. NuGet packages in search are:"
Write-Host ($packages -join [System.Environment]::NewLine)

# Read .csproj file as UTF-8
$project = New-Object -TypeName XML
$utf8Encoding = (New-Object System.Text.UTF8Encoding($false))
$streamReader = New-Object System.IO.StreamReader($fullFileName, $utf8Encoding, $false)
$project.Load($streamReader)
$streamReader.Close()

$packageReferences = $project.Project.ItemGroup.PackageReference 

$updated = $false
foreach ($package in $packages)
{
    # get latest nuget package version
    $lowerCasePackage = $package.ToLower()
    $nugetUrl = "https://api.nuget.org/v3/registration5-gz-semver2/$lowerCasePackage/index.json"
    $res = Invoke-RestMethod $nugetUrl
    $latestVersion = $res.items.items.catalogEntry.version[-1]
    Write-Host "Found version $latestVersion in NuGet server for $package"

    # get version from project file
    $packageReference = $packageReferences | Where-Object { $_.Include -eq $package }
    $projectVersion = $packageReference | Select-Object -ExpandProperty Version
    Write-Host "Found version $projectVersion in the project file for $package"

    if ($latestVersion -eq $projectVersion)
    {
        Write-Host "Latest version is the same as project version for $package"
    }
    else
    {
        $packageReference.Version = $latestVersion
        $updated = $true;
    }
}

if ($updated)
{
    $project.Save($fullFileName)
}
else
{
    Write-Host "There are no updated dependencies"
}