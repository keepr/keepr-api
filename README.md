# Keeper

## Requirements
- Microsoft Sql Server
- .NET Core 3.1

## Config

Create a copy of the `appsettings.json` file which won't be committed to GitHub for development.

```
cp ./Keeper.API/appsettings.json ./Keeper.API/appsettings.Development.json
```

## Getting Started

```
dotnet restore
dotnet build
dotnet run
```

## Building for production

```
dotnet build --configuration Release
```

## Authors
[Francois Laubscher](https://francois.codes/) ([@fjlaubscher](https://github.com/fjlaubscher))