if ((Get-Item .\build -ea si) -ne $null) 
{
	rmdir .\build -recurse 
}
mkdir .\build
.\Onramper.exe --source=.\src\Highway.MVC --destination=.\build --config=.\src\nuspec\ --execute=.\src\.nuget\nuget.exe
