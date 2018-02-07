using iShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Repo.EntityConfigurations
{
    public class InvoiceConfigurations: IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.Property(i => i.OrderId)
                .IsRequired();


            builder.Property(o => o.OrderId)
                .IsRequired();
        }
    }
}
