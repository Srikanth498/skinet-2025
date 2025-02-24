using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        if(!context.Products.Any()){//if there are no products in the database
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");//read the json file
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);//deserialize the json file to a list of products
            if(products == null) return;//if there are no products return
            context.Products.AddRange(products);//add the products to the database
            await context.SaveChangesAsync();//save changes
        }
    }
}
