Framework "4.0"

properties {
    $msbuildexe = Get-Item "C:\Program Files (x86)\MSBuild\12.0\bin\amd64\msbuild.exe" -ErrorAction SilentlyContinue
    if ($msbuildexe -eq $null) {
        $msbuildexe = Get-Item "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe" -ErrorAction SilentlyContinue
        Assert ($msbuildexe -ne $null) "Unable to locate MSBuild.exe"
    }

    $build_config = "Release"
    $pack_dir = ".\pack"

    Reset-Directory .\build
    $build_dir = Get-Item ".\build"
    $build_archive = ".\buildarchive"
    $nugetexe = Get-Item ".\src\.nuget\NuGet.exe"
    $onramperexe = ".\Onramper.exe"
    $version_number = "3.0.1.0"
    $nuget_version_number = $version_number

    if ($Env:BUILD_NUMBER -ne $null) {
        $nuget_version_number += "-$Env:BUILD_NUMBER"
    }

    #$allVersions = @("MVC3","MVC4", "MVC5")
    $allVersions = @("MVC3", "MVC4")

    $packages = @{
        "Highway.Onramp.MVC" = $allVersions;
        "Highway.Onramp.MVC.All" = $allVersions;
        "Highway.Onramp.MVC.Data" = $allVersions;
        "Highway.Onramp.MVC.Logging" = $allVersions;
    }
}

task default -depends build
task build -depends build-all
task test -depends build-all #, test-all
task pack -depends pack-all
task push -depends push-all



task build-all -depends compile-template, onramper-template

task compile-template {
    rebuild .\src\Highway.MVC.sln
}

task onramper-template -depends Update-Version, clean-buildarchive {
    Remove-Item -Force -Recurse .\src\Templates\bin -ErrorAction SilentlyContinue
    & $onramperexe --source=.\src\Templates --destination=$build_dir --config=.\src\nuspec\ --execute=$nugetexe
    Move-Item "$($build_dir.FullName)\*.nupkg" $build_archive -Force
}

task Update-Version {
    Get-Item .\src\nuspec\*.nuspec | % {
        $doc = [xml] (Get-Content $_)
        $doc.package.metadata.version = $nuget_version_number
        $writer = New-Object System.Xml.XmlTextWriter($_,$null)
        $writer.Formatting = [System.Xml.Formatting]::Indented
        $doc.Save($writer)
    }
}

task test-all -depends build-all {
    $packages.Keys | % {
        $key = $_
        $versions = $packages[$key]

        $versions | % {
            $version = $_

            Write-Host "########################################################################################"
            Write-Host "# $key in $version"
            Write-Host "########################################################################################"

            Reset-Directory .\test-bed
            Expand-ZIPFile -File ".\test-projects\$version.zip" -Destination .\test-bed

            Add-PackageToConfig $key .\test-bed\TestProject\packages.config

            Set-Content Env:\EnableNuGetPackageRestore -Value true
            & $nugetexe restore .\test-bed\TestProject.sln -source $build_dir
            rebuild .\test-bed\TestProject.sln
        }
    }
}

##################################################################################################################

task pack-ci -depends clean-buildarchive, pack-all -precondition { Test-IsCI } {
    dir -Path "$pack_dir\*.nupkg" | % { 
        cp $_ $build_archive
    } 
}

task pack-all -depends Update-Version, clean-nuget -precondition { Test-PackageDoesNotExist } {
	pack-nuget .\src\Highway.Pavement\Highway.Pavement\Highway.Pavement.csproj
}

task push-all -depends pack-all, clean-nuget {
    Get-ChildItem -Path "$pack_dir\*.nupkg" |
        %{ 
            push-nuget $_
            mv $_ .\nuget\.
        }
    rm $pack_dir -Recurse -Force
}


task clean-buildarchive {
    Reset-Directory $build_archive
}

task clean-nuget {
    Reset-Directory $pack_dir
}

task clean-testresults {
    Reset-Directory .\TestResults
}


##########################################################################################
# Functions
##########################################################################################


function rebuild([string]$slnPath) { 
    Set-Content Env:\EnableNuGetPackageRestore -Value true
    & $nugetexe restore $slnPath
    exec { & $msbuildexe $slnPath /t:rebuild /v:q /clp:ErrorsOnly /nologo /p:Configuration=$build_config }
}


function Reset-Directory($path) {
    if (Test-Path $path) {
        Remove-item $path -Recurse -Force
    }
    if (PathDoesNotExist $path) {
        New-Item -ItemType Directory -Path $path | Out-Null
    }
}

function Expand-ZIPFile($File, $Destination) {
    Add-Type -As System.IO.Compression.FileSystem

    $ZipFile = Get-Item $File
    $Dest = Get-Item $Destination

    [IO.Compression.ZipFile]::ExtractToDirectory( $ZipFile, $Dest )
}

function Add-PackageToConfig($package,$config) {
    $content = [xml] (Get-Content $config) 
    $content.packages.InnerXml += "<package id=`"$package`" version=`"$version_number`" targetFramework=`"net45`" />"
    $writer = New-Object System.Xml.XmlTextWriter($config,$null)
    try {
        $writer.Formatting = [System.Xml.Formatting]::Indented
        $content.Save($writer)
    } finally {
        $writer.Dispose()
    }
}

##########################################################################################

function Test-IsCI {
    $Env:TEAMCITY_VERSION -ne $null
}

function Test-PackageDoesNotExist() {
    (ls ".\nuget\*$version_number.nupkg" | Measure-Object).Count -gt 0
}

function Test-ModifiedInGIT($path) {
    if (Test-IsCI -eq $false) { return $false }
    $status_result = & git status $path --porcelain
    $status_result -ne $null
}


function pack-nuget($prj) {
    exec { 
        & .\src\Highway.Pavement\.nuget\nuget.exe pack $prj -o pack -prop configuration=$build_config
    }
}

function push-nuget($prj) {
    exec { 
        & .\src\Highway.Pavement\.nuget\nuget.exe push $prj
    }
}

function PathDoesNotExist($path) {
    (Test-Path $path) -eq $false
}
