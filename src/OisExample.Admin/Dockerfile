FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:22-bookworm-slim AS node

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETARCH
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
    NUGET_XMLDOC_MODE=skip
WORKDIR /src

COPY --from=node /usr/local/bin/node /usr/local/bin/node
COPY --from=node /usr/local/lib/node_modules /usr/local/lib/node_modules
RUN ln -s /usr/local/lib/node_modules/npm/bin/npm-cli.js /usr/local/bin/npm \
    && npm install -g npm@latest \
    && node --version && npm --version

COPY ["src/OisExample.Admin/OisExample.Admin.csproj", "src/OisExample.Admin/"]
COPY ["src/OisExample.Admin.EntityFramework.Identity/OisExample.Admin.EntityFramework.Identity.csproj", "src/OisExample.Admin.EntityFramework.Identity/"]
COPY ["src/OisExample.Admin.EntityFramework/OisExample.Admin.EntityFramework.csproj", "src/OisExample.Admin.EntityFramework/"]
COPY ["src/OisExample.Admin.EntityFramework.Extensions/OisExample.Admin.EntityFramework.Extensions.csproj", "src/OisExample.Admin.EntityFramework.Extensions/"]
COPY ["src/OisExample.Admin.EntityFramework.Configuration/OisExample.Admin.EntityFramework.Configuration.csproj", "src/OisExample.Admin.EntityFramework.Configuration/"]
COPY ["src/OisExample.Admin.UI/OisExample.Admin.UI.csproj", "src/OisExample.Admin.UI/"]
COPY ["src/OisExample.Shared.Configuration/OisExample.Shared.Configuration.csproj", "src/OisExample.Shared.Configuration/"]
COPY ["src/OisExample.Admin.EntityFramework.Shared/OisExample.Admin.EntityFramework.Shared.csproj", "src/OisExample.Admin.EntityFramework.Shared/"]

RUN dotnet restore -a $TARGETARCH "src/OisExample.Admin/OisExample.Admin.csproj"

COPY . .
WORKDIR "/src/src/OisExample.Admin"
RUN dotnet build -a $TARGETARCH "OisExample.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -a $TARGETARCH "OisExample.Admin.csproj" -c Release --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "OisExample.Admin.dll"]
