# Migrations

## 1. Using the CLI

### Add a migration
```bash
dotnet ef migrations add [MigrationName] --project QuizApp.Data --startup-project QuizApp.WebAPI --context QuizAppDbContext --output-dir Migrations
```

### Update the database
```bash
dotnet ef database update --project QuizApp.Data --startup-project QuizApp.WebAPI --context QuizAppDbContext
```

### Roll back a migration
```bash
dotnet ef database update [MigrationName] --project QuizApp.Data --startup-project QuizApp.WebAPI --context QuizAppDbContext
```

### Drop the database
```bash
dotnet ef database drop --project QuizApp.Data --startup-project QuizApp.WebAPI --context QuizAppDbContext
```

### Remove a migration
```bash
dotnet ef migrations remove --project QuizApp.Data --startup-project QuizApp.WebAPI --context QuizAppDbContext
```

## 2. Using the Package Manager Console
### Add a migration
```bash
Add-Migration [MigrationName] -Project QuizApp.Data -StartupProject QuizApp.WebAPI -Context QuizAppDbContext -OutputDir QuizApp.Data/Migrations
```

### Update the database
```bash
Update-Database -Project QuizApp.Data -StartupProject QuizApp.WebAPI -Context QuizAppDbContext
```

### Roll back a migration
```bash
Update-Database [MigrationName] -Project QuizApp.Data -StartupProject QuizApp.WebAPI -Context QuizAppDbContext
```

### Remove a migration
```bash
Remove-Migration -Project QuizApp.Data -StartupProject QuizApp.WebAPI -Context QuizAppDbContext
```

[]: # Path: README.md


