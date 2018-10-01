using Microsoft.EntityFrameworkCore.Migrations;

namespace Exs.Infra.Data.Migrations
{
    public partial class ignorecascadefilmelocacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CascadeMode",
                table: "LocacaoFilme");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CascadeMode",
                table: "LocacaoFilme",
                nullable: false,
                defaultValue: 0);
        }
    }
}
