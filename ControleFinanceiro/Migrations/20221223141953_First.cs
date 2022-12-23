using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleFinanceiro.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(180)", maxLength: 180, nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    Tipo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 12, 23, 14, 19, 53, 692, DateTimeKind.Utc).AddTicks(3588)),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 12, 23, 14, 19, 53, 692, DateTimeKind.Utc).AddTicks(4113))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes");
        }
    }
}
