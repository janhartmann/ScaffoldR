Function New-Solution {
    
    Param(
        [Parameter(Mandatory=$true)]
        [string]$solution,
        [Parameter(Mandatory=$true)]
        [string]$configuration
    )

    # Set the path to the .NET folder in order to use "msbuild.exe"
    $env:PATH = "C:\Program Files (x86)\MSBuild\14.0\Bin"

    Invoke-Expression "msbuild.exe $solution /nologo /v:m /p:Configuration=$configuration /t:Clean"
    Invoke-Expression "msbuild.exe $solution /nologo /v:m /p:Configuration=$configuration /clp:ErrorsOnly"
}

Function Update-AssemblyVersion() {

    Param(
        [Parameter(Mandatory=$true)]
        [string]$filePath,
        [Parameter(Mandatory=$true)]
        [string]$publishVersion
    )

    Write-Host ("-- Updating '{0}' to version '{1}'" -f $filePath, $publishVersion)

    $assemblyVersionPattern = 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyVersion = 'AssemblyVersion("' + $publishVersion + '")';
    $assemblyFileVersionPattern = 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)'
    $assemblyFileVersion = 'AssemblyFileVersion("' + $publishVersion + '")';

    (Get-Content $filePath) | ForEach-Object { 
            % { $_ -Replace $assemblyVersionPattern, $assemblyVersion } |
            % { $_ -Replace $assemblyFileVersionPattern, $assemblyFileVersion } 
    } | Set-Content $filePath
}

Function Update-NugetSpecDependencyVersion
{
    Param
    (
        [Parameter(Mandatory=$true)]
        [string]$filePath,
        [Parameter(Mandatory=$true)]
        [string]$packageId,
        [Parameter(Mandatory=$true)]
        [string]$publishVersion
    )

    [xml] $toFile = (Get-Content $filePath)

    $nodes = $toFile.SelectNodes("//package/metadata/dependencies/dependency[starts-with(@id, $packageId)]")
    foreach ($node in $nodes) 
    {
        Write-Host ("-- Updating '{0}' in '{1}' to version '{2}'" -f $node.id, $filePath, $publishVersion)
        $node.version = "[{0}]" -f $publishVersion
    }

    $toFile.Save($filePath)

}

Function Invoke-PackNuget {

    Param(
        [Parameter(Mandatory=$true)]
        [string]$project,
        [Parameter(Mandatory=$true)]
        [string]$configuration,
        [Parameter(Mandatory=$true)]
        [string]$outputFolder
    )

    if (!(Test-Path -Path $outputFolder)) {
        New-Item $outputFolder -Type Directory
    }

    Write-Host "-- Packaging '$project'"
    Invoke-Expression ".nuget\NuGet.exe pack $project -OutputDirectory '$outputFolder' -Prop Configuration=$configuration"
}

Function Publish-NugetPackage() {
  
    Param(
        [Parameter(Mandatory=$true)]
        [string]$package
    )

    Write-Host "-- Publishing '$package'"
    Invoke-Expression ".nuget\NuGet.exe push $package"
      
}