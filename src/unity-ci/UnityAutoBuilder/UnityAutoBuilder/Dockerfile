FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UnityAutoBuilder/UnityAutoBuilder.fsproj", "UnityAutoBuilder/"]
RUN dotnet restore "UnityAutoBuilder/UnityAutoBuilder.fsproj"
COPY . .
WORKDIR "/src/UnityAutoBuilder"
RUN dotnet build "UnityAutoBuilder.fsproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UnityAutoBuilder.fsproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UnityAutoBuilder.dll"]
