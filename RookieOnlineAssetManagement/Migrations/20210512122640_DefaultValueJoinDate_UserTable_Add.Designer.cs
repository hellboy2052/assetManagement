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
    [Migration("20210512122640_DefaultValueJoinDate_UserTable_Add")]
    partial class DefaultValueJoinDate_UserTable_Add
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
                            ConcurrencyStamp = "36792535-1fa5-4439-a201-aba36b39ae77",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "f456eb20-a4b3-4d76-b9b2-ccdbe6641f39",
                            ConcurrencyStamp = "e60e4f20-0343-45d9-9efe-9b8f09994c4b",
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
                            ConcurrencyStamp = "0a05454a-caea-4972-8c65-a04f17916334",
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
                            PasswordHash = "AQAAAAEAACcQAAAAEHpLZ9FNij02zkPuTKAtXFejX0RGENwJscBq+s8wFPs7MLtKppAXve/9E+AG0nLkxw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2ea046f0-cf25-4bc7-9464-8a0a0fe705fc",
                            StaffCode = "SD0001",
                            TwoFactorEnabled = false,
                            Type = "Admin",
                            UserName = "annt"
                        },
                        new
                        {
                            Id = "bfa869f5-7605-4e5e-a4a1-aa0b2b87743a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b37273e0-1453-414e-b054-454c73c5f7cf",
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
                            PasswordHash = "AQAAAAEAACcQAAAAEICD0iMPkkyiaWJtLGc78/V6m3c7YuvPi1THP/yFUrh4YEi0WuA4z2dduVH9HT79Gw==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "50209fcb-20ce-4146-8f2d-da477caca0ee",
                            StaffCode = "SD0002",
                            TwoFactorEnabled = false,
                            Type = "Staff",
                            UserName = "antv"
                        },
                        new
                        {
                            Id = "308a0f9c-2744-45d1-96c6-3c2f624f284c",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "73e205ac-e5a8-4c5e-bc88-482b479bd288",
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
                            PasswordHash = "AQAAAAEAACcQAAAAEH3Okf2WkOkNJ7dlntvpB6MMHtklowI23GZQiwPjdas9agAg25k/xYDi3vjnnFrsKQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "c67522f8-d71d-42f1-bfa7-63288a4247ba",
                            StaffCode = "SD0003",
                            TwoFactorEnabled = false,
                            Type = "Staff",
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
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("IncrementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("InstallDate")
                        .HasMaxLength(50)
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Specification")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("State")
                        .HasColumnType("int");

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