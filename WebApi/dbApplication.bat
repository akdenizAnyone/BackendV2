dotnet ef migrations add %1 -c ApplicationDbContext -p ../Infrastructure.Persistence/
dotnet ef database update -c ApplicationDbContext