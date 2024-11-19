﻿// <auto-generated />
using System;
using Library_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library_Management_System.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20241015145348_db4")]
    partial class db4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Library_Management_System.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Library_Management_System.Models.Checkout", b =>
                {
                    b.Property<int>("CheckoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CheckoutId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CheckoutDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MemberId")
                        .HasColumnType("int");

                    b.HasKey("CheckoutId");

                    b.HasIndex("BookId");

                    b.HasIndex("MemberId");

                    b.ToTable("Checkouts");
                });

            modelBuilder.Entity("Library_Management_System.Models.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MemberId"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MembershipDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberId");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Library_Management_System.Models.Penalty", b =>
                {
                    b.Property<int>("PenaltyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PenaltyId"));

                    b.Property<int>("CheckoutId")
                        .HasColumnType("int");

                    b.Property<int>("DaysOverdue")
                        .HasColumnType("int");

                    b.Property<decimal>("PenaltyAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("PenaltyId");

                    b.HasIndex("CheckoutId");

                    b.ToTable("Penalties");
                });

            modelBuilder.Entity("Library_Management_System.Models.Checkout", b =>
                {
                    b.HasOne("Library_Management_System.Models.Book", "Book")
                        .WithMany("Checkouts")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library_Management_System.Models.Member", "Member")
                        .WithMany("Checkouts")
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Member");
                });

            modelBuilder.Entity("Library_Management_System.Models.Penalty", b =>
                {
                    b.HasOne("Library_Management_System.Models.Checkout", "Checkout")
                        .WithMany("Penalties")
                        .HasForeignKey("CheckoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Checkout");
                });

            modelBuilder.Entity("Library_Management_System.Models.Book", b =>
                {
                    b.Navigation("Checkouts");
                });

            modelBuilder.Entity("Library_Management_System.Models.Checkout", b =>
                {
                    b.Navigation("Penalties");
                });

            modelBuilder.Entity("Library_Management_System.Models.Member", b =>
                {
                    b.Navigation("Checkouts");
                });
#pragma warning restore 612, 618
        }
    }
}
