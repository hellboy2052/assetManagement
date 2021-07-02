using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RookieOnlineAssetManagement.Entities;
using System;

namespace RookieOnlineAssetManagement.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure (EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.IncrementId).UseIdentityColumn().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(x => x.StaffCode).HasColumnType("char").HasMaxLength(6).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Location).HasMaxLength(50).IsRequired();
            builder.Property(x => x.DateOfBirth).HasColumnType("date").HasColumnName("DoB");
            builder.Property(x => x.JoinedDate).HasColumnType("date").HasDefaultValueSql("getdate()");
            builder.Property(x => x.IsDisabled).HasDefaultValue(false);
            builder.Property(x => x.IsDefaultPassword).HasDefaultValue(true);
            builder.Property(x => x.FullName).HasMaxLength(100);
            builder.Property(x => x.Type).HasConversion<string>();
            builder.Property(x => x.Location).HasConversion<string>();

            builder.Property(u => u.FullName)
                    .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            builder.Property(x => x.StaffCode)
                    .HasComputedColumnSql("N'SD'+ RIGHT('0000'+CAST(IncrementId AS VARCHAR(4)),4)");
        }
    }
}
