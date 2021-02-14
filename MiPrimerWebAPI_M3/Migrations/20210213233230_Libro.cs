using Microsoft.EntityFrameworkCore.Migrations;

namespace MiPrimerWebAPI_M3.Migrations
{
    public partial class Libro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cNombre",
                table: "Autores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    iIdLibro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cTitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iIdAutor = table.Column<int>(type: "int", nullable: false),
                    AutoriIdAutor = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.iIdLibro);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_AutoriIdAutor",
                        column: x => x.AutoriIdAutor,
                        principalTable: "Autores",
                        principalColumn: "iIdAutor",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_AutoriIdAutor",
                table: "Libros",
                column: "AutoriIdAutor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.AlterColumn<string>(
                name: "cNombre",
                table: "Autores",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
