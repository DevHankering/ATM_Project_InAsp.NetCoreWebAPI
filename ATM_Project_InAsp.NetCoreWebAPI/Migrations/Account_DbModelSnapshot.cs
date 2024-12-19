﻿// <auto-generated />
using System;
using ATM_Project_InAsp.NetCoreWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ATM_Project_InAsp.NetCoreWebAPI.Migrations
{
    [DbContext(typeof(Account_Db))]
    partial class Account_DbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ATM_Project_InAsp.NetCoreWebAPI.Models.DocumentDomainModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AdhaarCardUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PanCardUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("ATM_Project_InAsp.NetCoreWebAPI.Models.UserDomainModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAtLocalTimeZone")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("CreatedAtUniversalTimeZone")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DocumentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<long?>("PhoneNumber")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DocumentId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ATM_Project_InAsp.NetCoreWebAPI.Models.UserDomainModel", b =>
                {
                    b.HasOne("ATM_Project_InAsp.NetCoreWebAPI.Models.DocumentDomainModel", "Documents")
                        .WithOne()
                        .HasForeignKey("ATM_Project_InAsp.NetCoreWebAPI.Models.UserDomainModel", "DocumentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Documents");
                });
#pragma warning restore 612, 618
        }
    }
}
