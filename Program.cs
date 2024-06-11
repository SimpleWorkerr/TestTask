using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace TestTask
{
    public class Program
    {

        public static void Main(string[] args)
        {
            ApplicationContext dbContext = new ApplicationContext();


            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.MapGet("/getNumbers", async (HttpContext context) =>
            {
                await context.Response.WriteAsJsonAsync(from array in dbContext.Arrays select array.Id);
            });

            app.MapGet("/array", async (HttpContext context) =>
            {
                var isParsed = int.TryParse(context.Request.Query["id"], out int id);
                if (isParsed)
                {
                    ArrayDb result = dbContext.Arrays.Include(array => array.Items).First(a => a.Id == id);

                    if (result != null)
                        await context.Response.WriteAsJsonAsync(from i in result.Items select i.Value);
                }

            });

            app.MapPost("/sort", async (HttpContext context) =>
            {
                int[]? result = await context.Request.ReadFromJsonAsync<int[]>();

                if (result != null)
                {
                    var sortedMass = BubbleSort(result);

                    ArrayDb resArray = new ArrayDb();
                    foreach (var value in sortedMass)
                        resArray.Items.Add(new Item() { Value = value });

                    dbContext.Arrays.Add(resArray);

                    await dbContext.SaveChangesAsync();
                    await context.Response.WriteAsJsonAsync(sortedMass);
                }
            });

            app.Run();
        }

        private static int[] BubbleSort(int[] unSortMass)
        {
            var len = unSortMass.Length;
            for (var i = 1; i < len; i++)
                for (var j = 0; j < len - i; j++)
                    if (unSortMass[j] > unSortMass[j + 1])
                        Swap(ref unSortMass[j], ref unSortMass[j + 1]);

            return unSortMass;
        }
        static void Swap(ref int e1, ref int e2)
        {
            var temp = e1;
            e1 = e2;
            e2 = temp;
        }
    }
}
