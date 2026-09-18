FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG TARGETARCH
WORKDIR /src
COPY ["src/OisExample.STS.Identity/OisExample.STS.Identity.csproj", "src/OisExample.STS.Identity/"]
COPY ["src/OisExample.Admin.EntityFramework.Shared/OisExample.Admin.EntityFramework.Shared.csproj", "src/OisExample.Admin.EntityFramework.Shared/"]
COPY ["src/OisExample.Shared/OisExample.Shared.csproj", "src/OisExample.Shared/"]
RUN dotnet restore -a $TARGETARCH "src/OisExample.STS.Identity/OisExample.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/OisExample.STS.Identity"
RUN dotnet build -a $TARGETARCH "OisExample.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -a $TARGETARCH "OisExample.STS.Identity.csproj" -c Release --no-restore -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "OisExample.STS.Identity.dll"]