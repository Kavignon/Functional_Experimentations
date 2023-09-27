FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UnityCIServer/UnityCIServer.fsproj", "UnityCIServer/"]
RUN dotnet restore "UnityCIServer/UnityCIServer.fsproj"
COPY . .
WORKDIR "/src/UnityCIServer"
RUN dotnet build "UnityCIServer.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnityCIServer.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnityCIServer.dll"]
