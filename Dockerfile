FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN mkdir FitnessApp.SettingsApi
COPY FitnessApp.SettingsApi ./FitnessApp.SettingsApi
COPY nuget.config ./FitnessApp.SettingsApi
WORKDIR /src/FitnessApp.SettingsApi

RUN dotnet restore "FitnessApp.SettingsApi.csproj" --configfile nuget.config
RUN dotnet build "FitnessApp.SettingsApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FitnessApp.SettingsApi.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80 443
ENTRYPOINT ["dotnet", "FitnessApp.SettingsApi.dll"]