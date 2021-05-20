#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SiteMonitoring/SiteMonitoring.csproj", "SiteMonitoring/"]
COPY ["SiteMonitoring.Core/SiteMonitoring.Core.csproj", "SiteMonitoring.Core/"]
RUN dotnet restore "SiteMonitoring/SiteMonitoring.csproj"
COPY . .
WORKDIR "/src/SiteMonitoring"
RUN dotnet build "SiteMonitoring.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SiteMonitoring.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SiteMonitoring.dll"]