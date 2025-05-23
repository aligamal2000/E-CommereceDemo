﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Data;

#nullable disable

namespace Persistence.Data.Migrations
{
    [DbContext(typeof(StoreDBContext))]
    partial class StoreDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Orders.DeliveryMethod", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("DeliveryTime")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.HasKey("id");

                    b.ToTable("DeliveryMethods", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Orders.Order", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DeliveryMethodId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("OrderDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("DeliveryMethodId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Orders.OrderItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<Guid?>("Orderid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Orderid");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Products.DelvieryMethod", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Domain.Models.Products.Product", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PictureUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10,5)");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("BrandId");

                    b.HasIndex("TypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Models.Products.ProductType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("Domain.Models.Orders.Order", b =>
                {
                    b.HasOne("Domain.Models.Orders.DeliveryMethod", "DeliveryMethod")
                        .WithMany()
                        .HasForeignKey("DeliveryMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Models.Orders.OrderAddress", "ShipToAddress", b1 =>
                        {
                            b1.Property<Guid>("Orderid")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("Orderid");

                            b1.ToTable("Orders");

                            b1.WithOwner()
                                .HasForeignKey("Orderid");
                        });

                    b.Navigation("DeliveryMethod");

                    b.Navigation("ShipToAddress")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Orders.OrderItem", b =>
                {
                    b.HasOne("Domain.Models.Orders.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("Orderid");

                    b.OwnsOne("Domain.Models.Orders.ProductItemOrdered", "Product", b1 =>
                        {
                            b1.Property<int>("OrderItemid")
                                .HasColumnType("int");

                            b1.Property<string>("PictureUrl")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("ProductName")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("id")
                                .HasColumnType("int");

                            b1.HasKey("OrderItemid");

                            b1.ToTable("OrderItems");

                            b1.WithOwner()
                                .HasForeignKey("OrderItemid");
                        });

                    b.Navigation("Product")
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Products.Product", b =>
                {
                    b.HasOne("Domain.Models.Products.DelvieryMethod", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Products.ProductType", "Type")
                        .WithMany("Products")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.Models.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Domain.Models.Products.DelvieryMethod", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Models.Products.ProductType", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
