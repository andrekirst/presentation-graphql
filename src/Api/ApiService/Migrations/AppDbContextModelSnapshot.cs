﻿// <auto-generated />
using System;
using ApiService.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ApiService.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ApiService.Models.Persistance.AddressEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("City")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Number")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Street")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.DayStatisticsAggregateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Average")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<int>("Maximum")
                        .HasColumnType("integer");

                    b.Property<int>("Minimum")
                        .HasColumnType("integer");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("DayStatisticsAggregates");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.DayStatisticsValuesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("DayStatisticsValues");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.HourStatisticsAggregateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Average")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<int>("Hour")
                        .HasColumnType("integer");

                    b.Property<int>("Maximum")
                        .HasColumnType("integer");

                    b.Property<int>("Minimum")
                        .HasColumnType("integer");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("HourStatisticsAggregates");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.HourStatisticsValuesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<int>("Hour")
                        .HasColumnType("integer");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("HourStatisticsValues");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.MonthStatisticsAggregateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Average")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Maximum")
                        .HasColumnType("integer");

                    b.Property<int>("Minimum")
                        .HasColumnType("integer");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("MonthStatisticsAggregates");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.MonthStatisticsValuesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("MonthStatisticsValues");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.OperatorEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Website")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("Operators");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ParkAreaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<int?>("Free")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastUpdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("OperatorId")
                        .HasColumnType("integer");

                    b.Property<int?>("ParkingStateId")
                        .HasColumnType("integer");

                    b.Property<int>("RegionId")
                        .HasColumnType("integer");

                    b.Property<int?>("Total")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OperatorId");

                    b.HasIndex("ParkingStateId");

                    b.HasIndex("RegionId");

                    b.ToTable("ParkAreas");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ParkingSlotHistoryEntity", b =>
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

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int?>("Total")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("ParkingSlotHistories");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ParkingStateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("ParkingStates");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.RegionEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ServiceTimeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeOnly?>("Closing")
                        .HasColumnType("time without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsAllDayOpen")
                        .HasColumnType("boolean");

                    b.Property<TimeOnly?>("Opening")
                        .HasColumnType("time without time zone");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId")
                        .IsUnique();

                    b.ToTable("ServiceTimes");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.YearStatisticsAggregateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Average")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Maximum")
                        .HasColumnType("integer");

                    b.Property<int>("Minimum")
                        .HasColumnType("integer");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("YearStatisticsAggregates");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.YearStatisticsValuesEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ParkAreaId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParkAreaId");

                    b.ToTable("YearStatisticsValues");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.AddressEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithOne("Address")
                        .HasForeignKey("ApiService.Models.Persistance.AddressEntity", "ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("ApiService.Models.Persistance.Location", "Location", b1 =>
                        {
                            b1.Property<int>("AddressEntityId")
                                .HasColumnType("integer");

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision");

                            b1.Property<double>("Longitute")
                                .HasColumnType("double precision");

                            b1.HasKey("AddressEntityId");

                            b1.ToTable("Addresses");

                            b1.WithOwner()
                                .HasForeignKey("AddressEntityId");
                        });

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.DayStatisticsAggregateEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("DayStatisticsAggregates")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.DayStatisticsValuesEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("DayStatisticsValues")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.HourStatisticsAggregateEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("HourStatisticsAggregates")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.HourStatisticsValuesEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("HourStatisticsValues")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.MonthStatisticsAggregateEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("MonthStatisticsAggregates")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.MonthStatisticsValuesEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("MonthStatisticsValues")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ParkAreaEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.OperatorEntity", "Operator")
                        .WithMany("ParkAreas")
                        .HasForeignKey("OperatorId");

                    b.HasOne("ApiService.Models.Persistance.ParkingStateEntity", "ParkingState")
                        .WithMany("ParkAreas")
                        .HasForeignKey("ParkingStateId");

                    b.HasOne("ApiService.Models.Persistance.RegionEntity", "Region")
                        .WithMany("ParkAreas")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operator");

                    b.Navigation("ParkingState");

                    b.Navigation("Region");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ParkingSlotHistoryEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("ParkingSlotHistories")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ServiceTimeEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithOne("ServiceTime")
                        .HasForeignKey("ApiService.Models.Persistance.ServiceTimeEntity", "ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.YearStatisticsAggregateEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("YearStatisticsAggregates")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.YearStatisticsValuesEntity", b =>
                {
                    b.HasOne("ApiService.Models.Persistance.ParkAreaEntity", "ParkArea")
                        .WithMany("YearStatisticsValues")
                        .HasForeignKey("ParkAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParkArea");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.OperatorEntity", b =>
                {
                    b.Navigation("ParkAreas");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ParkAreaEntity", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("DayStatisticsAggregates");

                    b.Navigation("DayStatisticsValues");

                    b.Navigation("HourStatisticsAggregates");

                    b.Navigation("HourStatisticsValues");

                    b.Navigation("MonthStatisticsAggregates");

                    b.Navigation("MonthStatisticsValues");

                    b.Navigation("ParkingSlotHistories");

                    b.Navigation("ServiceTime");

                    b.Navigation("YearStatisticsAggregates");

                    b.Navigation("YearStatisticsValues");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.ParkingStateEntity", b =>
                {
                    b.Navigation("ParkAreas");
                });

            modelBuilder.Entity("ApiService.Models.Persistance.RegionEntity", b =>
                {
                    b.Navigation("ParkAreas");
                });
#pragma warning restore 612, 618
        }
    }
}
