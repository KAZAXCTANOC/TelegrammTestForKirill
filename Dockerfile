FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /TelegrammTestForKirill

COPY . .

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /TelegrammTestForKirill
RUN dotnet restore

RUN dotnet build  -c Release -o /TelegrammTestForKirill

FROM build AS publish
RUN dotnet publish -c Release -o /TelegrammTestForKirill

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /TelegrammTestForKirill
COPY --from=build-env /TelegrammTestForKirill .
ENTRYPOINT ["TelegrammTestForKirill.exe"]


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /TelegrammTestForKirill
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /bin
COPY Solution.sln ./
COPY ClassLibraryProject/*.csproj ./ClassLibraryProject/
COPY WebAPIProject/*.csproj ./WebAPIProject/

RUN dotnet restore
COPY . .
WORKDIR /src/ClassLibraryProject
RUN dotnet build -c Release -o /app

WORKDIR /src/WebAPIProject
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebAPIProject.dll"]