using Microsoft.EntityFrameworkCore.Migrations;
using System.Globalization;

#nullable disable

namespace FCG.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class segundamigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_TB_USUARIO_DSC_EMAIL",
                table: "TB_USUARIO",
                column: "DSC_EMAIL",
                unique: true);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_USUARIO_DSC_EMAIL",
                table: "TB_USUARIO");
        }
    }
}
