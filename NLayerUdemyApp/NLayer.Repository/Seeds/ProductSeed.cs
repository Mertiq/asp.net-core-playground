using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;

namespace NLayer.Repository.Seeds;

public class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product { Id = 1, Name = "Kalem 1", CategoryId = 1, Price = 100, Stock = 20, CreatedDate = DateTime.Now },
            new Product { Id = 2, Name = "Kalem 2", CategoryId = 1, Price = 200, Stock = 30, CreatedDate = DateTime.Now },
            new Product { Id = 3, Name = "Kalem 3", CategoryId = 1, Price = 300, Stock = 40, CreatedDate = DateTime.Now },
            new Product { Id = 4, Name = "Kitap 1", CategoryId = 2, Price = 400, Stock = 50, CreatedDate = DateTime.Now }
        );
    }
}