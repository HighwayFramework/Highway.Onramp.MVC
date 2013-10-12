Framework "4.0"

properties {
    $build_config = "Release"
    $pack_dir = ".\pack"
    $build_archive = ".\buildarchive"
    $nugetexe = ".\src\.nuget\NuGet.exe"
    $onramperexe = ".\Onramper.exe"
    $version_number = "3.0.0.0"
    $nuget_version_number = $version_number

    if ($Env:BUILD_NUMBER -ne $null) {
        $nuget_version_number += "-$Env:BUILD_NUMBER"
    }

    #$allVersions = @("MVC3","MVC4", "MVC5")
    $allVersions = @("MVC3")

    $packages = @{
        "Highway.Onramp.MVC" = $allVersions;
        "Highway.Onramp.MVC.All" = $allVersions;
        "Highway.Onramp.MVC.Data" = $allVersions;
        "Highway.Onramp.MVC.Logging" = $allVersions;
    }
}

task default -depends build
task build -depends build-all
task test -depends build-all, test-all
task pack -depends pack-all
task push -depends push-all



task build-all -depends compile-template, onramper-template

task compile-template {
    rebuild .\src\Highway.MVC.sln
}

task onramper-template -depends Update-Version {
    Reset-Directory .\build
    Remove-Item -Force -Recurse .\src\Highway.MVC\bin -ErrorAction SilentlyContinue
    & $onramperexe --source=.\src\Highway.MVC --destination=.\build --config=.\src\nuspec\ --execute=$nugetexe
}

task Update-Version {
    Get-Item .\src\nuspec\*.nuspec | % {
        $doc = [xml] (Get-Content $_)
        $doc.package.metadata.version = $version_number
        $writer = New-Object System.Xml.XmlTextWriter($_,$null)
        $writer.Formatting = [System.Xml.Formatting]::Indented
        $doc.Save($writer)
    }
}

##################################################################################################################

task test-all -depends clean-buildarchive, Clean-TestResults {
    $mstest = Get-ChildItem -Recurse -Force 'C:\Program Files (x86)\Microsoft Visual Studio *\Common7\IDE\MSTest.exe'
    $mstest = $mstest.FullName
    $test_dlls = Get-ChildItem -Recurse ".\src\**\**\bin\release\*Tests.dll" |
        ?{ $_.Directory.Parent.Parent.Name -eq ($_.Name.replace(".dll","")) }
    $test_dlls | % { 
        try {
            exec { & "$mstest" /testcontainer:$($_.FullName) } 
        } finally {
            cp .\TestResults\*.trx $build_archive -Verbose
        }
    }
}

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
    exec { msbuild $slnPath /t:rebuild /v:q /clp:ErrorsOnly /nologo /p:Configuration=$build_config }
}


function Reset-Directory($path) {
    if (Test-Path $path) {
        Remove-item $path -Recurse -Force
    }
    if (PathDoesNotExist $path) {
        New-Item -ItemType Directory -Path $path | Out-Null
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
