using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure (EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(cate => cate.CategoryId);
            builder.HasIndex(cate =>cate.CategoryName).IsUnique();
            builder.Property(x => x.CategoryId).ValueGeneratedOnAdd();
        }
    }
}
