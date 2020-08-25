﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eMTE.Temperature.DataAccess;

namespace eMTE.Temperature.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200825013206_Initialize")]
    partial class Initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("eMTE.Temperature.Domain.DayMeasure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Intime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("NotedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("OutTime")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.HasIndex("UserId", "NotedDate")
                        .IsUnique();

                    b.ToTable("DayMeasures");
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.HealthMeasure", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("Cough")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("DayMeasureId")
                        .HasColumnType("char(36)");

                    b.Property<string>("HeatRate")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ImageWithPPE")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("OxygenSaturation")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("RunnyNose")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("ShortnessBreath")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SlotNumber")
                        .HasColumnType("int");

                    b.Property<bool>("Sneezing")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("Temperature")
                        .HasColumnType("double");

                    b.Property<string>("TemperatureUnit")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdateDateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("DayMeasureId");

                    b.ToTable("HealthMeasures");
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.HealthMeasureConfiguration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsCoughMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsHeatRateMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsImageWithPPEMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsOxygenSaturationMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsRunnyNoseMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsShortnessBreathMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSneezingMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsTemperatureMandate")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MeasureCount")
                        .HasColumnType("int");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TemperatureUnit")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId")
                        .IsUnique();

                    b.ToTable("HealthMeasureConfigurations");
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Logo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisplayPicture")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid?>("ModifiedById")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("ModifiedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("TeamDescription")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("TeamManagerId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ModifiedById");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.HasIndex("TeamManagerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.TeamUserMap", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("TeamId", "UserId")
                        .IsUnique();

                    b.ToTable("TeamUserMaps");
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("AlreadyInfected")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Code")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DisplayPicture")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("InfectedFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("InfectedTo")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsOrganizationAdmin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nationality")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleDescription")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("OrganizationId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.DayMeasure", b =>
                {
                    b.HasOne("eMTE.Temperature.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMTE.Temperature.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.HealthMeasure", b =>
                {
                    b.HasOne("eMTE.Temperature.Domain.DayMeasure", "DayMeasure")
                        .WithMany()
                        .HasForeignKey("DayMeasureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.HealthMeasureConfiguration", b =>
                {
                    b.HasOne("eMTE.Temperature.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.Team", b =>
                {
                    b.HasOne("eMTE.Temperature.Domain.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMTE.Temperature.Domain.User", "ModifiedBy")
                        .WithMany()
                        .HasForeignKey("ModifiedById");

                    b.HasOne("eMTE.Temperature.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMTE.Temperature.Domain.User", "TeamManager")
                        .WithMany()
                        .HasForeignKey("TeamManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.TeamUserMap", b =>
                {
                    b.HasOne("eMTE.Temperature.Domain.Team", "Team")
                        .WithMany()
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eMTE.Temperature.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eMTE.Temperature.Domain.User", b =>
                {
                    b.HasOne("eMTE.Temperature.Domain.Organization", "Organization")
                        .WithMany()
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}