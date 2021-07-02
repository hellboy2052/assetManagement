using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RookieOnlineAssetManagement.Entities;

namespace RookieOnlineAssetManagement.Data.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure (EntityTypeBuilder<Assignment> builder)
        {
            builder.HasKey(asgn => asgn.AssignmentId);
            builder.Property(x => x.AssignmentId).ValueGeneratedOnAdd();
            builder.Property(astFk => astFk.AssetCode).HasColumnType("char").HasMaxLength(8).IsRequired();
            builder.Property(astFk => astFk.AssignedDate).HasColumnType("date").IsRequired();

            builder.HasOne(x => x.Asset).WithMany(y => y.Assignments).HasForeignKey(fk => fk.AssetCode);
        }
    }
}
