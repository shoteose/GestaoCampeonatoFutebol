﻿using Microsoft.AspNetCore.Identity;

namespace GestaoCampeonatoFutebol.Data
{
    public class SeedRoles
    {
        public static void Seed(RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.Roles.Any())
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();

                roleManager.CreateAsync(new IdentityRole("Utilizador")).Wait();


            }
        }
    }
}
