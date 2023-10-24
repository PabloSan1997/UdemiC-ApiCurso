using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace primeraApi.Migrations
{
    /// <inheritdoc />
    public partial class agregardatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "ActualizacionFecha", "Amenidad", "Detalle", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6167), "", "detalle de la villa", new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6150), "", 25.0, "Villa San alejandro", 54, 200.0 },
                    { 2, new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6178), "", "Mira esto", new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6178), "", 2534.0, "No se que onda", 544, 200543.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
