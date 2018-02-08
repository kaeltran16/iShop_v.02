using iShop.Data.Entities;
using iShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Repo.EntityConfigurations
{
    public class InventoryConfigurations: IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(i => new {i.ProductId, i.SupplierId});

            builder.Property(i => i.ProductId)
                .IsRequired();

            builder.Property(i => i.SupplierId)
                .IsRequired();

            builder.Property(i => i.Stock)
                .IsRequired();

            builder
                .HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            builder
                .HasOne(i => i.Supplier)
                .WithMany(p => p.Inventories)
                .HasForeignKey(i => i.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
