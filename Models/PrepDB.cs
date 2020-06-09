using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace BalkanaAPI.Models
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<HutsContext>());
            }
        }

        public static void SeedData(HutsContext context) 
        {
            System.Console.WriteLine("APPLYING MIGRATIONS...........");
            context.Database.Migrate();
#pragma warning disable EF1001 // Internal EF Core API usage.
            if (!context.HutItems.Any())
#pragma warning restore EF1001 // Internal EF Core API usage.
            {
              System.Console.WriteLine("Adding data seeding...........");
              context.HutItems.AddRange(
                  new Hut() { Name="Kumata" },
                  new Hut() { Name = "Pumata" },
                  new Hut() { Name = "Tumata" },
                  new Hut() { Name = "Shumata" },
                  new Hut() { Name = "Lumata" }
                  );
              context.SaveChanges();
            }
            else
            {
                System.Console.WriteLine("Have data not seeding...........");
            }
        }
    }
}
