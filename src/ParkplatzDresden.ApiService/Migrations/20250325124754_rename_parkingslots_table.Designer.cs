﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ParkplatzDresden.ApiService.Database;

#nullable disable

namespace ParkplatzDresden.ApiService.Migrations
{
    [DbContext(typeof(ParkplatzDbContext))]
    [Migration("20250325124754_rename_parkingslots_table")]
    partial class rename_parkingslots_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ParkplatzDresden.ApiService.Models.Database.ParkAreaEntity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("ParkAreas");
                });

            modelBuilder.Entity("ParkplatzDresden.ApiService.Models.Database.ParkingSlotsEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("Free")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaEntityId")
                        .HasColumnType("integer");

                    b.Property<int?>("Total")
                        .HasColumnType("integer");

                    b.Property<int?>("Used")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaEntityId")
                        .IsUnique();

                    b.ToTable("ParkingSlots", (string)null);
                });

            modelBuilder.Entity("ParkplatzDresden.ApiService.Models.Database.ParkingSlotsEntity", b =>
                {
                    b.HasOne("ParkplatzDresden.ApiService.Models.Database.ParkAreaEntity", "ParkArea")
                        .WithOne("ParkingSlot")
                        .HasForeignKey("ParkplatzDresden.ApiService.Models.Database.ParkingSlotsEntity", "ParkAreaEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ParkplatzDresden.ApiService.Models.Database.ParkAreaEntity", b =>
                {
                    b.Navigation("ParkingSlot");
                });
#pragma warning restore 612, 618
        }
    }
}
