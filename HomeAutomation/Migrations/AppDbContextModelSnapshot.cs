﻿// <auto-generated />
using System;
using HomeAutomation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HomeAutomation.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HomeAutomation.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsUp")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Equipments");

                    b.HasDiscriminator<string>("Type").HasValue("Equipment");
                });

            modelBuilder.Entity("HomeAutomation.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("When")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("PersonId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("HomeAutomation.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HomeAutomation.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateOnly>("BirthDay")
                        .HasColumnType("date");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsAcive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("NIC")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("varchar(15)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("NIC")
                        .IsUnique();

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Type").HasValue("Person");
                });

            modelBuilder.Entity("HomeAutomation.Models.AC", b =>
                {
                    b.HasBaseType("HomeAutomation.Models.Equipment");

                    b.Property<int>("Temperature")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("AC");
                });

            modelBuilder.Entity("HomeAutomation.Models.Admin", b =>
                {
                    b.HasBaseType("HomeAutomation.Person");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("HomeAutomation.Models.Door", b =>
                {
                    b.HasBaseType("HomeAutomation.Models.Equipment");

                    b.HasDiscriminator().HasValue("Door");
                });

            modelBuilder.Entity("HomeAutomation.Models.Heater", b =>
                {
                    b.HasBaseType("HomeAutomation.Models.Equipment");

                    b.Property<int>("Temperature")
                        .HasColumnType("int")
                        .HasColumnName("Heater_Temperature");

                    b.HasDiscriminator().HasValue("Heater");
                });

            modelBuilder.Entity("HomeAutomation.Models.Lamp", b =>
                {
                    b.HasBaseType("HomeAutomation.Models.Equipment");

                    b.HasDiscriminator().HasValue("Lamp");
                });

            modelBuilder.Entity("HomeAutomation.Models.TV", b =>
                {
                    b.HasBaseType("HomeAutomation.Models.Equipment");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("TV");
                });

            modelBuilder.Entity("HomeAutomation.Models.User", b =>
                {
                    b.HasBaseType("HomeAutomation.Person");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("HomeAutomation.Models.Equipment", b =>
                {
                    b.HasOne("HomeAutomation.Models.Room", "Room")
                        .WithMany("Equipments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("HomeAutomation.Models.Log", b =>
                {
                    b.HasOne("HomeAutomation.Models.Equipment", "Equipment")
                        .WithMany("Logs")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("HomeAutomation.Person", "Person")
                        .WithMany("Logs")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("HomeAutomation.Models.Equipment", b =>
                {
                    b.Navigation("Logs");
                });

            modelBuilder.Entity("HomeAutomation.Models.Room", b =>
                {
                    b.Navigation("Equipments");
                });

            modelBuilder.Entity("HomeAutomation.Person", b =>
                {
                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
}
