Param(
    [Parameter(Mandatory=$true)]
    [string]$version,
    [string]$configuration = "Release",
    [boolean]$build = $true,
    [boolean]$nuspec = $true,
    [boolean]$publish = $false,
    [boolean]$pack = $false,
    [string]$outputFolder = "build\packages"
)

# Include build functions
. "./BuildFunctions.ps1"

# The solution we are building
$solution = "NerveFramework.sln"
$assemblies = "NerveFramework", "NerveFramework.EntityFramework"
$testAssemblies = "NerveFramework.UnitTests", "NerveFramework.EntityFramework.UnitTests"
$releaseAssemblies = "NerveFramework", "NerveFramework.EntityFramework"

# Start by changing the assembly version
Write-Host "Changing the assembly versions to '$version'..."
Get-ChildItem ($assemblies + $testAssemblies) -Filter "AssemblyInfo.cs" -Recurse | 
    % { Update-AssemblyVersion $_.FullName $version }

# Build the entire solution
if ($build) {
    Write-Host "Cleaning and building $solution (Configuration: $configuration)"
    New-Solution $solution $configuration
}

# Change dependency version on all depending assemblies
if ($nuspec) {
    Write-Host "Changing the NerveFramework(s) NuGet Spec version dependencies to '$version'..."
    Get-ChildItem $assemblies -Filter "NerveFramework*.nuspec" -Recurse | 
        % { Update-NugetSpecDependencyVersion $_.FullName "NerveFramework" $version }
}

# Pack the assemblies and move to output folder
if ($pack) {
    Write-Host "Packaging projects..."
    Get-ChildItem $releaseAssemblies -Filter "NerveFramework*.csproj" -Recurse | 
        % { Invoke-PackNuget $_.FullName $configuration $outputFolder } 
}

# Publish the assemblies
if ($publish) {
    Write-Host "Publishing packages..."
    Get-ChildItem $outputFolder -Filter "*$version.nupkg" -Recurse | 
        % { Publish-NugetPackage $_.FullName } 
}