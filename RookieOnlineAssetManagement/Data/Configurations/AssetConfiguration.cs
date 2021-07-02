using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure (EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(ast => ast.AssestCode);
            builder.Property(x => x.AssestCode).HasColumnType("char").HasMaxLength(8).IsRequired();
            builder.Property(x => x.Specification).IsRequired();
            builder.Property(x => x.AssestName).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Location).HasMaxLength(50).IsRequired();
            builder.Property(x => x.InstallDate).HasColumnType("date").IsRequired();

            builder.HasOne(ast => ast.Category)
                   .WithMany(cate => cate.Assets).OnDelete(DeleteBehavior.SetNull)
                   .HasForeignKey(cateFk => cateFk.CategoryId);
        }
    }
}
