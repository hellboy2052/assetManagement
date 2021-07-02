﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RookieOnlineAssetManagement.Data;

namespace RookieOnlineAssetManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210513035518_SpecificationLengthModifed_IncrementIdColumnRemoved_AssetTable")]
    partial class SpecificationLengthModifed_IncrementIdColumnRemoved_AssetTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "c581eb54-d110-4177-b851-7a616d52abc8",
                            ConcurrencyStamp = "7ee67f3e-b1ac-4661-847d-eef3c85e2775",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                            ConcurrencyStamp = "22185751-c483-4a25-851e-6d14be07c8c7",
                            Name = "Staff",
                            NormalizedName = "STAFF"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                            RoleId = "c581eb54-d110-4177-b851-7a616d52abc8"
                        },
                        new
                        {
                            UserId = "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                            RoleId = "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39"
                        },
                        new
                        {
                            UserId = "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                            RoleId = "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("DoB");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FullName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

                    b.Property<bool>("Gender")
                        .HasColumnType("bit");

                    b.Property<int>("IncrementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDefaultPassword")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDisabled")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("JoinedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffCode")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(6)
                        .HasColumnType("char(6)")
                        .HasComputedColumnSql("N'SD'+ RIGHT('0000'+CAST(IncrementId AS VARCHAR(4)),4)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "5f6f60e1-433b-4c02-8af2-46fb29be8568",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "45344b28-0044-497d-a529-d638eb2cc253",
                            DateOfBirth = new DateTime(1999, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailConfirmed = false,
                            FirstName = "An",
                            Gender = true,
                            IncrementId = 1,
                            IsDefaultPassword = false,
                            IsDisabled = false,
                            JoinedDate = new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Nguyen Thuy",
                            Location = "HCM",
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEIPFYzxFuTQholxmfyiB9x9uWj3KyRrUQyDO+Eo1w1ZJkIgzbzf/fd6OjwpQyUAyaw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2150b155-6c4d-459b-b4bd-151108768ddc",
                            StaffCode = "SD0001",
                            TwoFactorEnabled = false,
                            Type = "Staff",
                            UserName = "annt"
                        },
                        new
                        {
                            Id = "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "8c5e7fa2-0acc-470f-9f9e-18ae522ad8dd",
                            DateOfBirth = new DateTime(1993, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailConfirmed = false,
                            FirstName = "An",
                            Gender = false,
                            IncrementId = 2,
                            IsDefaultPassword = false,
                            IsDisabled = false,
                            JoinedDate = new DateTime(2019, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Tran Van",
                            Location = "HCM",
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAEA+CvA/GEIS2OpSEHg/RTo7xwx7TnFq2ADWv7/EMvU/AyMbbheGF9DtDWXDGRIehNQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "31e81dc6-05ee-4459-ba0b-66749c129d3c",
                            StaffCode = "SD0002",
                            TwoFactorEnabled = false,
                            Type = "Admin",
                            UserName = "antv"
                        },
                        new
                        {
                            Id = "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "05f1a246-ec43-4bda-b2cb-ea3c229e870a",
                            DateOfBirth = new DateTime(1996, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailConfirmed = false,
                            FirstName = "Binh",
                            Gender = true,
                            IncrementId = 3,
                            IsDefaultPassword = false,
                            IsDisabled = true,
                            JoinedDate = new DateTime(2018, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Nguyen Van",
                            Location = "HN",
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAEAACcQAAAAELFDyxMBXxwmeTPdrs4wzUgVU5nmg3rG3j4yNPtb6j9KkyrJye5Udngi9047HWNwWg==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "637d85c6-57c9-4bcc-96af-1d436d4dde53",
                            StaffCode = "SD0003",
                            TwoFactorEnabled = false,
                            Type = "Admin",
                            UserName = "binhnv1"
                        });
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Asset", b =>
                {
                    b.Property<string>("AssestCode")
                        .HasMaxLength(8)
                        .HasColumnType("char(8)");

                    b.Property<string>("AssestName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("InstallDate")
                        .HasColumnType("date");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssestCode");

                    b.HasIndex("CategoryId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Assignment", b =>
                {
                    b.Property<string>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssetCode")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("char(8)");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("AssignmentId");

                    b.HasIndex("AdminId");

                    b.HasIndex("AssetCode")
                        .IsUnique();

                    b.HasIndex("StaffId");

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CategoryId");

                    b.HasIndex("CategoryName")
                        .IsUnique()
                        .HasFilter("[CategoryName] IS NOT NULL");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Returning", b =>
                {
                    b.Property<string>("ReturnId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(8)");

                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssetCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("AssignedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ReturnedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StaffId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("ReturnId");

                    b.HasIndex("AdminId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("StaffId");

                    b.ToTable("Returning");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Asset", b =>
                {
                    b.HasOne("RookieOnlineAssetManagement.Entities.Category", "Category")
                        .WithMany("Assets")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Assignment", b =>
                {
                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("RookieOnlineAssetManagement.Entities.Asset", "Asset")
                        .WithOne("Assignment")
                        .HasForeignKey("RookieOnlineAssetManagement.Entities.Assignment", "AssetCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");

                    b.Navigation("Admin");

                    b.Navigation("Asset");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Returning", b =>
                {
                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.HasOne("RookieOnlineAssetManagement.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("RookieOnlineAssetManagement.Entities.Asset", "Asset")
                        .WithOne("Returning")
                        .HasForeignKey("RookieOnlineAssetManagement.Entities.Returning", "ReturnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RookieOnlineAssetManagement.Entities.ApplicationUser", "Staff")
                        .WithMany()
                        .HasForeignKey("StaffId");

                    b.Navigation("Admin");

                    b.Navigation("Asset");

                    b.Navigation("Category");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Asset", b =>
                {
                    b.Navigation("Assignment");

                    b.Navigation("Returning");
                });

            modelBuilder.Entity("RookieOnlineAssetManagement.Entities.Category", b =>
                {
                    b.Navigation("Assets");
                });
#pragma warning restore 612, 618
        }
    }
}
