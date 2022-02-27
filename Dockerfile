# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal as runtime
WORKDIR /app

# Build
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal as build
ENV \
    # Disable Telemetry message
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    # Disable Dotnet Logo
    DOTNET_NOLOGO=1 \
    # Skip first usage message
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1
COPY . /work
WORKDIR /work/src/Api
RUN dotnet restore
RUN dotnet build --no-restore "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM build AS debug
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+8080
# Install .NET debugger
RUN apt-get update
RUN apt-get install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg
CMD dotnet run --no-restore "Api.csproj"

FROM runtime AS prod
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]
