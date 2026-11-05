FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG TARGETARCH
WORKDIR /src
COPY ["src/OisExample.Admin.Api/OisExample.Admin.Api.csproj", "src/OisExample.Admin.Api/"]
COPY ["src/OisExample.Admin.EntityFramework.Shared/OisExample.Admin.EntityFramework.Shared.csproj", "src/OisExample.Admin.EntityFramework.Shared/"]
COPY ["src/OisExample.Admin.EntityFramework.SqlServer/OisExample.Admin.EntityFramework.SqlServer.csproj", "src/OisExample.Admin.EntityFramework.SqlServer/"]
COPY ["src/OisExample.Shared/OisExample.Shared.csproj", "src/OisExample.Shared/"]
COPY ["src/OisExample.Admin.EntityFramework.PostgreSQL/OisExample.Admin.EntityFramework.PostgreSQL.csproj", "src/OisExample.Admin.EntityFramework.PostgreSQL/"]
RUN dotnet restore -a $TARGETARCH "src/OisExample.Admin.Api/OisExample.Admin.Api.csproj"
COPY . .
WORKDIR "/src/src/OisExample.Admin.Api"
RUN dotnet build -a $TARGETARCH "OisExample.Admin.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -a $TARGETARCH "OisExample.Admin.Api.csproj" -c Release --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "OisExample.Admin.Api.dll"]
