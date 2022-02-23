# Build Environment
FROM mcr.microsoft.com/dotnet/aspnet:5.0.14-alpine3.14-amd64 as build
ENV \
    # SDK version
    DOTNET_SDK_VERSION=5.0.405 \
    # Disable Telemetry message
    DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    # Disable Dotnet Logo
    DOTNET_NOLOGO=1 \
    # Skip first usage message
    DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1 \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip

RUN apk add --no-cache \
    curl \
    icu-libs \
    git

# Install .NET SDK
RUN wget -O dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/$DOTNET_SDK_VERSION/dotnet-sdk-$DOTNET_SDK_VERSION-linux-musl-x64.tar.gz \
    && dotnet_sha512='b37393cbb6cb1cab4efe88741b29a70d9a519a21f9a0ba870a2be58e7fee30d6b9e7da57aa572df5d4f0c876dd43a1ad11517d4988b5ec26511215f6c362debd' \
    && echo "$dotnet_sha512  dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -oxzf dotnet.tar.gz -C /usr/share/dotnet ./packs ./sdk ./templates ./LICENSE.txt ./ThirdPartyNotices.txt \
    && rm dotnet.tar.gz

# Copy/Restore and Publish Solution
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
RUN ["dotnet", "publish", "-c", "Release", "-o", "out"]

# Build Runtime
FROM mcr.microsoft.com/dotnet/aspnet:5.0.14-alpine3.14-amd64 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Api.dll"]
