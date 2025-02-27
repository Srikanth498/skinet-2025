using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{


    public void AddProduct(Product product)
    {
        context.Products.Add(product);//add product
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);//delete product
    }

    public async Task<IReadOnlyList<string>> GetBandsAsync()
    {
        
        return await context.Products.Select(x => x.Brand)
            .Distinct().ToListAsync();//return all product brands
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);//return product by id
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();//query all products
        
        if(!string.IsNullOrWhiteSpace(brand))
        {
            query = query.Where(x => x.Brand == brand);//filter by brand
        }
        if(!string.IsNullOrWhiteSpace(type))
        {
            query = query.Where(x => x.Type == type);//filter by type
        }

        query = sort switch
        {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Name)
            };//sort by price


        return await query.ToListAsync();//return filtered products
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        
        return await context.Products.Select(x => x.Type)
            .Distinct()
            .ToListAsync();//return all product types
    }

    public bool productExists(int id)
    {
        return context.Products.Any(x => x.Id == id);//if product exists return true else false
    }

    public async Task<bool> SavechangesAsync()
    {
        return await context.SaveChangesAsync() > 0;//if changes are saved return true else false 
    }

    public void updateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;//update product
    }
}
