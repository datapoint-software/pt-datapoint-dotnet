If (-Not (Test-Path src)) { 
    throw "Script must run from the project root path."
}

If (Test-Path dist) {
    Remove-Item -Recurse -Force dist
}

dotnet restore src\Datapoint.sln
dotnet pack src\Datapoint.sln --no-restore --configuration Debug --output dist\nuget

dotnet nuget push dist\nuget\*.nupkg -s S:\NuGet
dotnet nuget push dist\nuget\*.snupkg -s S:\NuGet
