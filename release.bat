@echo off
dotnet build src/Skybrud.WebApi.Json --configuration Release /t:rebuild /t:pack -p:BuildTools=1 -p:PackageOutputPath=../../releases/nuget