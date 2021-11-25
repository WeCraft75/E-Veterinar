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


### AUTH
##### malo cancer ngl
```
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.0.0-rc.2.21480.10
dotnet add package Microsoft.AspNetCore.Identity.UI --version 6.0.0-rc.2.21480.10
dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore --version 6.0.0-rc.2.21480.10
```
create a file  odels\ApplicationUser.cs and add the following to it
```
using Microsoft.AspNetCore.Identity;

namespace E_Veterinar.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? City { get; set; }

    }
}
```
potem v eveterinarContext dodaj dependency `using Microsoft.AspNetCore.Identity.EntityFrameworkCore;` in spremeni dedovanje na `: IdentityDbContext<ApplicationUser>` . Dodaj še `base.OnModelCreating(modelBuilder);` za `OnModelCreatingPartial(modelBuilder);`

nato
```
dotnet ef migrations add AppUser
# izbrišemo obstoječo bazo s podatki in nato ustvarimo novo, to je varno, ne bi smelo povzročiti težav
dotnet ef database drop
dotnet ef database update
# generiramo login stran
dotnet-aspnet-codegenerator identity -dc E_Veterinar.Data.eveterinarContext -fi "Account.Register;Account.Login;Account.Logout;Account.RegisterConfirmation" --generateLayout
```

nato v Program.cs spremeni
```
# dodaj
using web.Models;

# nastavi spremenljivko connectionString za .useSqlServer(connectionString)
var connectionString = builder.Configuration.GetConnectionString("EVeterinaContext");

// odstrani stari .AddDbContext
//builder.Services.AddDbContext<SchoolContext>(options =>
//            options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolContext")));

// prilagodi RequireConfirmedAccount = false
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SchoolContext>();

// dodaj app.MapRazorPages(); za app.useAuthentication();
app.MapRazorPages();
```

nato dodaj `<partial name="_LoginPartial" />` v `/Views/Shared/_Layout.cshtml` na konec navbara

Potem lahko dodajaš najprej dependency `using Microsoft.AspNetCore.Authorization;`, ter potem `[Authorize]` za namespace in pred definicijo class-a v katerikoli controller rabiš,  