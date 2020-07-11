## Intro
**microsharkd3** is a monolithic asp.net core web application boilerplate featuring .env configuration, internationalization, OAuth2/OpenId, Vue.js SPA with cookie based authentication (<a href="https://www.rdegges.com/2018/please-stop-using-local-storage/" target="_blank">no token on local storage</a>) and some other things.
## Required tools
```sh
# redis
choco install redis-64 || brew install redis || apt install redis-server

# Fluent migrator
dotnet tool install -g FluentMigrator.Dotnet.Cli

# mkcert
brew install mkcert || choco install mkcert || apt install libnss3-tools
```

## Command line tool
The command line tool supports execution of database migration and scaffolding.<br />
```text
|---------------------------------------------------------------
| microshark3d cli
| Author: Matheus Vaz <git@matheusvaz.com>
|---------------------------------------------------------------

Commands           Description                                             Options
========           ===========                                             =======
publish            Run dotnet publish (Necessary to run migrations)

migrate            Migration runner interface
                   Apply all migrations                                    --all
                   List applied and pending migrations                     --list
                   Apply migrations up to given version (inclusive)        --up-to <MIGRATION_ID>
                   Apply migrations down to given version (exclusive)      --down-to <MIGRATION_ID>
                   Rollback all migrations                                 --rollback
                   Rollback to given version                               --rollback <MIGRATION_ID>
                   Rollback the last migration applied                     --rollback-last
                   Rollback by <n> steps                                   --rollback-steps <N>
                   Validate order of applied migrations                    --validate
                   Generate SQL file of the migration                      --sql-output <FILENAME>

migration          Create migration file                                   <NAME> <DOMAIN_NAME>

help               Shows this help menu
```

## Troubleshooting
If you experience "No migrations found" even if you already created an migration, just run:
```sh
bash microsharkd3 publish
```
