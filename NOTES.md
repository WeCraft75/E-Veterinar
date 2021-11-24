# NOTES
## Creating database on docker
Create container with
```
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=StrongPassw0rd!" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-CU13-ubuntu-20.04
docker ps -a
docker rename [name of container] eveterinar
```

The login credentials should be:  
username = sa  
password = StrongPassw0rd!  

Then connect to the database and run the crebas.sql script.

## Using dotnet-ef and dotnet-aspnet-codegenerator
Najprej dodamo package preko cli
```
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```
### Create models from database
```
dotnet ef dbcontext scaffold 'Server=localhost;Database=eveterinar;User Id=sa;Password=StrongPassw0rd!;' Microsoft.EntityFrameworkCore.SqlServer -o Models
```
### Create controllers from models
```
dotnet-aspnet-codegenerator controller -name EvidencaController -m Evidenca -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name IzdelekController -m Izdelek -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name NarociloController -m Narocilo -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name PostumController -m Postum -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name StoritevController -m Storitev -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name StrankaController -m Stranka -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name TerminController -m Termin -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name VeterinarController -m Veterinar -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
dotnet-aspnet-codegenerator controller -name ZalogaController -m Zaloga -dc E_Veterinar.Data.eveterinarContext -udl -outDir Controllers
```
Then you need to fix Views/Shared/_Layout.cshtml add lines for each controller like so:
```
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Postum" asp-action="Index">Postum</a>
</li>
```

### Creating a custom layout
Just mess around with the Views/Shared/_Layout.cshtml.css file until it looks nice