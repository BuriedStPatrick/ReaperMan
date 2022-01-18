FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ReaperMan.Cli/ReaperMan.Cli.csproj", "ReaperMan.Cli/"]
RUN dotnet restore "ReaperMan.Cli/ReaperMan.Cli.csproj"
COPY . .
WORKDIR "/src/ReaperMan.Cli"
RUN dotnet build "ReaperMan.Cli.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReaperMan.Cli.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["reaperman.exe"]
