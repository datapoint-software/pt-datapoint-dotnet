If (-Not (Test-Path src)) { 
    throw "Script must run from the project root path."
}

dotnet restore src\Datapoint.sln
dotnet pack src\Datapoint.sln --no-restore --configuration Release --output dist\nuget
tar -czvf dist\release-artifacts.tar.gz -C dist nuget
