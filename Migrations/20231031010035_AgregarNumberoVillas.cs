using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace primeraApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumberoVillas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetalleEspecial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualizacionFecha", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9709), new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9696) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualizacionFecha", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9719), new DateTime(2023, 10, 30, 19, 0, 34, 736, DateTimeKind.Local).AddTicks(9718) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualizacionFecha", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6167), new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6150) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualizacionFecha", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6178), new DateTime(2023, 10, 23, 17, 43, 54, 201, DateTimeKind.Local).AddTicks(6178) });
        }
    }
}
