using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data.Configurations
{
    public class ReturningConfiguration : IEntityTypeConfiguration<Returning>
    {
        public void Configure (EntityTypeBuilder<Returning> builder)
        {
            builder.HasKey(x => x.ReturnId);
            builder.Property(x => x.ReturnId).ValueGeneratedOnAdd();
            builder.Property(x => x.ReturnedDate).HasColumnType("date");
        }
    }
}
