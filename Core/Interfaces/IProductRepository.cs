using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);//This method will return a list of products.
    Task<Product?> GetProductByIdAsync(int id);//This method will return a product by its id.
    Task<IReadOnlyList<string>> GetBandsAsync();//This method will return a list of product bands.
    Task<IReadOnlyList<string>> GetTypesAsync();//This method will return a list of product types.
    void AddProduct(Product product);//This method will add a product.
    void updateProduct(Product product);//This method will update a product.
    void DeleteProduct(Product product);//This method will delete a product.
    bool productExists(int id);//This method will check if a product exists.
    Task<bool> SavechangesAsync();//This method will save changes to the database.
}
