﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using primeraApi.Modelos.Datos;

#nullable disable

namespace primeraApi.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20231031010035_AgregarNumberoVillas")]
    partial class AgregarNumberoVillas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("primeraApi.Modelos.NumeroVilla", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<string>("DetalleEspecial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("NumeroVillas");
                });

            modelBuilder.Entity("primeraApi.Modelos.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ActualizacionFecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Amenidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagenUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("MetrosCuadrados")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("int");

                    b.Property<double>("Tarifa")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActualizacionFecha = new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9709),
                            Amenidad = "",
                            Detalle = "detalle de la villa",
                            FechaCreacion = new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9696),
                            ImagenUrl = "",
                            MetrosCuadrados = 25.0,
                            Nombre = "Villa San alejandro",
                            Ocupantes = 54,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            ActualizacionFecha = new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9719),
                            Amenidad = "",
                            Detalle = "Mira esto",
                            FechaCreacion = new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9718),
                            ImagenUrl = "",
                            MetrosCuadrados = 2534.0,
                            Nombre = "No se que onda",
                            Ocupantes = 544,
                            Tarifa = 200543.0
                        });
                });

            modelBuilder.Entity("primeraApi.Modelos.NumeroVilla", b =>
                {
                    b.HasOne("primeraApi.Modelos.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
