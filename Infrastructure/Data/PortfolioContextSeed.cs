using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class PortfolioContextSeed
    {
        public static async Task SeedAsync(PortfolioContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.Categories.Any())
                {
                    var categoriesData = File.ReadAllText("../Infrastructure/Data/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                    foreach (var item in categories)
                    {
                        context.Categories.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Modalities.Any())
                {
                    var modalitiesData = File.ReadAllText("../Infrastructure/Data/SeedData/modalities.json");
                    var modalities = JsonSerializer.Deserialize<List<Modality>>(modalitiesData);

                    foreach (var item in modalities)
                    {
                        context.Modalities.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Segments.Any())
                {
                    var segmentsData = File.ReadAllText("../Infrastructure/Data/SeedData/segments.json");
                    var segments = JsonSerializer.Deserialize<List<Segment>>(segmentsData);

                    foreach (var item in segments)
                    {
                        context.Segments.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.TypesOfStock.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/typesofstock.json");
                    var types = JsonSerializer.Deserialize<List<TypeOfStock>>(typesData);

                    foreach (var item in types)
                    {
                        context.TypesOfStock.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Stocks.Any())
                {
                    var stocksData = File.ReadAllText("../Infrastructure/Data/SeedData/stocks.json");
                    var stocks = JsonSerializer.Deserialize<List<Stock>>(stocksData);

                    foreach (var item in stocks)
                    {
                        context.Stocks.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Surtaxes.Any())
                {
                    var surtaxData = File.ReadAllText("../Infrastructure/Data/SeedData/surtaxes.json");
                    var surtaxes = JsonSerializer.Deserialize<List<Surtax>>(surtaxData);

                    foreach (var item in surtaxes)
                    {
                        context.Surtaxes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<PortfolioContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
