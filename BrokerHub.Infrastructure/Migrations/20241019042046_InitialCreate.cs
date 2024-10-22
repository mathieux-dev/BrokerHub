using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrokerHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Imoveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Endereco_Rua = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Endereco_Numero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Endereco_Cidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Endereco_Estado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Endereco_CEP = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Endereco_Complemento = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Quartos = table.Column<int>(type: "int", nullable: true),
                    Banheiros = table.Column<int>(type: "int", nullable: true),
                    VagasGaragem = table.Column<int>(type: "int", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imoveis", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Imoveis");
        }
    }
}
