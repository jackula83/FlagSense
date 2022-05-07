# flags

An on-prem feature flagging service that runs on any environment

## Setup

**Support for SQL Server _ONLY_ at this stage**

1. Update `appSettings.Development.json` to ensure the `Database` section has the correct creds for your database, ensure this is consistent with the settings in your `docker-compose.yml`
2. Perform your initial efcore migration: `add-migration initial -p flagservice.domain`, then `update-database`
3. Run `docker-compose up --build` in the root directory
