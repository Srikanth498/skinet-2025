using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    //This method is used to configure the entity of type Product
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        //This line of code applies the configurations from the ProductConfiguration class to the model builder.   
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        //This line of code sets the Name property of the Product entity as required.If we require we can set other properties like HasLength as required as well.
        builder.Property(x => x.Name).IsRequired();
    }
}
