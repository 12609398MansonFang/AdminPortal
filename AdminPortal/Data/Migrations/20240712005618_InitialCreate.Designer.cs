﻿// <auto-generated />
using AdminPortal.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AdminPortal.Data.Migrations
{
    [DbContext(typeof(AdminPortalContext))]
    [Migration("20240712005618_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("AdminPortal.Entities.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Claims");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Read Contacts Claim"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Write Contacts Claim"
                        });
                });

            modelBuilder.Entity("AdminPortal.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Claims")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("TenantId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Claims = "[1]",
                            Name = "Contacts Read Role",
                            TenantId = 1
                        },
                        new
                        {
                            Id = 2,
                            Claims = "[1,2]",
                            Name = "Contacts Admin Role",
                            TenantId = 1
                        });
                });

            modelBuilder.Entity("AdminPortal.Entities.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Google"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Deloitte"
                        });
                });

            modelBuilder.Entity("AdminPortal.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tenants")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserGroups")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Manson",
                            Tenants = "[1,2]",
                            UserGroups = "[1]"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ashwin",
                            Tenants = "[1]",
                            UserGroups = "[2]"
                        });
                });

            modelBuilder.Entity("AdminPortal.Entities.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Roles")
                        .HasColumnType("TEXT");

                    b.Property<int>("TenantId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("UserGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Contacts Read Role",
                            Roles = "[1]",
                            TenantId = 1
                        },
                        new
                        {
                            Id = 2,
                            Name = "Contacts Admin Role",
                            Roles = "[2]",
                            TenantId = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
