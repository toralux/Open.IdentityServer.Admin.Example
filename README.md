# Open.IdentityServer.Admin — Example

A runnable example of [Toralux Open.IdentityServer.Admin](https://github.com/toralux/Open.IdentityServer.Admin) **consumed as published NuGet packages** — no source checkout of the main repo required.

This entire solution was generated with the `dotnet new` template at package version 0.2.1:

```sh
dotnet new install Toralux.Open.IdentityServer.Admin.Templates::0.2.1

dotnet new toralux.open-isadmin \
  --name OisExample \
  --title "OIS Example" \
  --adminemail admin@example.com \
  --adminpassword Passw0rd-123 \
  --adminrole OisExampleAdmin \
  --adminclientid oisexample_admin \
  --adminclientsecret ExampleSecret-123 \
  --dockersupport true \
  --requirepushedauthorization false
```

The generated projects reference the published `Toralux.Open.IdentityServer.*` 0.2.1 packages — building this solution is the library-consumer test. The only modifications on top of the pristine template output:

- Connection strings in the three host projects' `appsettings.json` point at the Docker SQL Server below (the template defaults to Windows LocalDB)
- Two compose files for the database

## Run it

Prereqs: [.NET 10 SDK](https://dotnet.microsoft.com/download), Docker Desktop (or colima), and once: `dotnet dev-certs https --trust`

```sh
git clone https://github.com/toralux/Open.IdentityServer.Admin.Example.git
cd Open.IdentityServer.Admin.Example

# 1. database (SQL Server 2022, native on Intel Macs)
docker compose -f docker-compose.sqlserver.yml up -d

# 2. Admin API — applies migrations and seeds on startup
dotnet run --project src/OisExample.Admin.Api

# 3. STS (identity provider)                     https://localhost:44310
dotnet run --project src/OisExample.STS.Identity

# 4. Admin UI host                                https://localhost:7127
dotnet run --project src/OisExample.Admin
```

**Login:** `admin@example.com` / `Passw0rd-123` (seeded example credentials — obviously not for anything real)

### PostgreSQL instead

The template also generates EF migrations for PostgreSQL. Start `docker-compose.postgres.yml` and swap the provider in the hosts' `appsettings.json` (`EntityFramework--Provider` / the PostgreSQL connection strings) — see the main repo's docs.

## What this validates

- The `dotnet new` template installs and generates from the live NuGet feed
- The published packages restore/build/serve as dependencies
- Migrations + seed run against a real SQL Server container
- The full admin experience on a plain consumer machine (developed and CI-tested on Linux/Windows; this example is the macOS runbook)
