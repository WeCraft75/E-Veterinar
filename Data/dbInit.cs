using E_Veterinar.Models;
using System;
using System.Collections.Generic;
using E_Veterinar.Data;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace E_Veterinar.Data
{
    public static class dbInit
    {
        public static void Initialize(eveterinarContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {

            }


            if (context.Posta.Any())
            {
                return;
            }

            var poste = new Postum[]
            {
                new Postum{Naziv="Vipava",Stevilka=5271},
                new Postum{Naziv="Ajdovščina",Stevilka=5270},
                new Postum{Naziv="Ljubljana",Stevilka=1000}
            };
            context.Posta.AddRange(poste);
            context.SaveChanges();



            var veterinarji = new Veterinar[]{
                new Veterinar{IdVeterinar=1,Ime="Anton",Priimek="Lavrenčič",Kraj="Vipava", NaDom=true,Stevilka=5271,StevilkaNavigation=poste[0]},
                new Veterinar{IdVeterinar=2,Ime="Gospod",Priimek="Debevec",Kraj="Ljubljana", NaDom=false,Stevilka=1000,StevilkaNavigation=poste[2]}
            };
            context.Veterinars.AddRange(veterinarji);
            context.SaveChanges();

            var izdelki = new Izdelek[]{
                new Izdelek{IdIzdelek=1,Ime="Ivermeticin",Opis="Magično zdravilo za COVID-19, ki je izmišljena bolezein in seveda ne obstaja",Cena=11.12M},
                new Izdelek{IdIzdelek=2,Ime="Odvajala za pse",Opis="Za idiote, ki dajejo psom piškote s čokolado",Cena=10.0M}
            };
            context.Izdeleks.AddRange(izdelki);
            context.SaveChanges();

            var zaloge = new Zaloga[]{
                new Zaloga{IdIzdelek=1,IdIzdelekNavigation=izdelki[0],IdVeterinar=2,IdVeterinarNavigation=veterinarji[1],Kolicina=5}
            };
            context.Zalogas.AddRange(zaloge);
            context.SaveChanges();


            var stranke = new Stranka[]{
                new Stranka{IdStranka=1,Ime="Janez",Priimek="Kranjski",Kraj="Ajdovščina",Naslov="Na brajdi 5", Stevilka=5270,StevilkaNavigation=poste[1]}
            };
            context.Strankas.AddRange(stranke);
            context.SaveChanges();

            var termini = new Termin[]{
                new Termin{IdVeterinar=1,DatumZacetka=DateTime.Parse("29.11.2021 8:00:00"),DatumKonca=DateTime.Parse("29.11.2021 8:30:00"),JePotrjen=false,JeZaseden=false}
            };
            context.Termins.AddRange(termini);
            context.SaveChanges();

            var rolez = new IdentityRole[]{
                new IdentityRole{Id="1",Name="Administrator"},
                new IdentityRole{Id="2",Name="Veterinar"},
                new IdentityRole{Id="3",Name="User"}
            };
            context.Roles.AddRange(rolez);
            context.SaveChanges();

            // ADMIN
            var user = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "",
                City = "",
                Email = "admin@eveterinar.si",
                NormalizedEmail = "ADMIN@EVETERINAR.SI",
                UserName = "admin@eveterinar.si",
                NormalizedUserName = "admin@eveterinar.si",
                PhoneNumber = "",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Administrator");
                user.PasswordHash = hashed;
                context.Users.Add(user);
            }
            context.SaveChanges();

            var UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = rolez[0].Id, UserId=user.Id}
            };
            context.UserRoles.AddRange(UserRoles);
            context.SaveChanges();

            // VETERINAR
            var vet1 = new ApplicationUser
            {
                FirstName = "Mark",
                LastName = "Žgavec",
                City = "Vipava",
                Email = "markzgavec@eveterinar.si",
                NormalizedEmail = "MARKZGAVEC@EVETERINAR.SI",
                UserName = "markzgavec@eveterinar.si",
                NormalizedUserName = "markzgavec@eveterinar.si",
                PhoneNumber = "068613560",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == vet1.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(vet1, "@Mark2021");
                vet1.PasswordHash = hashed;
                context.Users.Add(vet1);
            }
            context.SaveChanges();

            UserRoles = new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>{RoleId = rolez[1].Id, UserId=vet1.Id}
            };
            context.UserRoles.AddRange(UserRoles);
            context.SaveChanges();
        }
    }
}