using iShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iShop.Repo.EntityConfigurations
{
    public class ImageConfigurations: IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.Property(i => i.ProductId).IsRequired();
            builder.Property(i => i.FileName).IsRequired();
        }
    }
}
