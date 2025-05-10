using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.Identiy;
using Domain.Models.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class DBInitializer(StoreDBContext context,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,StoreIdentityDbContext identityDbContext) : IDBInitializer
    {
  

        public async Task InializeAsync()
        {


          if((await context.Database.GetPendingMigrationsAsync()).Any())
                {
                await context.Database.MigrateAsync();
            }

            try
            {
                if (!context.Set<DelvieryMethod>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeds\brands.json");
                    var Objects = JsonSerializer.Deserialize<List<DelvieryMethod>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<DelvieryMethod>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<DelvieryMethod>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeds\delivery.json");
                    var Objects = JsonSerializer.Deserialize<List<DelvieryMethod>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<DelvieryMethod>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<ProductType>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeds\types.json");
                    var Objects = JsonSerializer.Deserialize<List<ProductType>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<ProductType>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
                if (!context.Set<Product>().Any())
                {
                    var data = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\Seeds\products.json");
                    var Objects = JsonSerializer.Deserialize<List<Product>>(data);

                    if (Objects is not null && Objects.Any())
                    {
                        context.Set<Product>().AddRange(Objects);
                        await context.SaveChangesAsync();
                    }
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        
        }
        public async Task IdentityInializeAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!userManager.Users.Any())
                {
                    var User1 = new ApplicationUser
                    {
                        Email = "Ahmed@gmail.com",
                        DisplayName = "Ahmed Nasser",
                        PhoneNumber = "01885566624",
                        UserName = "AhmedNasser"
                    };

                    var User2 = new ApplicationUser
                    {
                        Email = "Yassmine@gmail.com",
                        DisplayName = "Yassmine Ali",
                        PhoneNumber = "01885566624",
                        UserName = "YassmineAli"
                    };

                    await userManager.CreateAsync(User1, "P@ssw0rd");
                    await userManager.CreateAsync(User2, "P@ssw0rd");

                    await userManager.AddToRoleAsync(User1, "Admin");
                    await userManager.AddToRoleAsync(User2, "SuperAdmin");


                }
                await identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
    

        }
    }

}
    