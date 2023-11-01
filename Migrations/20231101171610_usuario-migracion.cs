using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace primeraApi.Migrations
{
    /// <inheritdoc />
    public partial class usuariomigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActualizacionFecha", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 11, 1, 11, 16, 9, 937, DateTimeKind.Local).AddTicks(5205), new DateTime(2023, 11, 1, 11, 16, 9, 937, DateTimeKind.Local).AddTicks(5191) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActualizacionFecha", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 11, 1, 11, 16, 9, 937, DateTimeKind.Local).AddTicks(5215), new DateTime(2023, 11, 1, 11, 16, 9, 937, DateTimeKind.Local).AddTicks(5214) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

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
        }
    }
}
