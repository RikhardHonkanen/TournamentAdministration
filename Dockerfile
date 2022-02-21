#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /TournamentAdministration
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /TournamentAdministration
COPY ["TournamentAdministration.csproj", "."]
RUN dotnet restore "./TournamentAdministration.csproj"
COPY . .
WORKDIR "/TournamentAdministration/."
RUN dotnet build "TournamentAdministration.csproj" -c Release -o /TournamentAdministration/build

FROM build AS publish
RUN dotnet publish "TournamentAdministration.csproj" -c Release -o /TournamentAdministration/publish

FROM base AS final
WORKDIR /TournamentAdministration
COPY --from=publish /TournamentAdministration/publish .
ENTRYPOINT ["dotnet", "TournamentAdministration.dll"]