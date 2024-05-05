
# Database 

## Update the database 
- Do this command if you don't have the databse yet or after a new migration command.
dotnet ef database update --startup-project .\DevGardenAPI\ --project .\DatabaseEf\

## Revert all migrations
- Do this command before the remove migrations command if migration already applied to db
dotnet ef database update 0 --startup-project .\DevGardenAPI\ --project .\DatabaseEf\

# Migrations

## Create a new migration
dotnet ef migrations add InitialMigration --startup-project .\DevGardenAPI\ --project .\DatabaseEf\

## Remove the existing migration 
dotnet ef migrations remove --startup-project .\DevGardenAPI\ --project .\DatabaseEf\

