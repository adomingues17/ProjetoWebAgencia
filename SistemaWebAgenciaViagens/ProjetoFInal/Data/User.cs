﻿using Microsoft.AspNetCore.Identity;
//seed
namespace ProjetoFinal.Data;

public class User
{
    public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roleNames = { "Admin", "Usuario" };
        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Cria o papel se ele não existir
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

        // Criar um usuário Admin padrão
        var adminEmail = configuration["AdminUser:Email"] ?? "admin2@mail.com";
        var adminPassword = configuration["AdminUser:Password"] ?? "Admin@123";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new IdentityUser()
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true, // Opcional: Confirma o email direto
            };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }


}
