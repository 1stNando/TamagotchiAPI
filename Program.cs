using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TamagotchiAPI.Models;
using TamagotchiAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TamagotchiAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Utilities.CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                var canContinue = await Utilities.WaitForMigrations(host, context);

                if (!canContinue)
                {
                    return;
                }
            }

            // var task = host.RunAsync();

            // Utilities.Notify("TamagotchiAPI Running!");

            // WebHostExtensions.WaitForShutdown(host);

            //////////////////////Beginning of console app set up 
            var context2 = new DatabaseContext();

            var keepGoing = true;

            while (keepGoing)
            {
                Console.Write("(L)ist pets. (C)reate pet. (D)elete pet. (U)pdate pet. (Q)uit: ");
                var option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "Q":
                        keepGoing = false;
                        break;

                    case "L":
                        var allPets = context2.Pets.Count();

                        Console.WriteLine(allPets);
                        break;
                }
            }

            var task = host.RunAsync();

            Utilities.Notify("TamagotchiAPI Running!");

            WebHostExtensions.WaitForShutdown(host);
        }
    }
}
