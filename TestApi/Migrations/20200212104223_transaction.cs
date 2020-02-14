using Microsoft.EntityFrameworkCore.Migrations;

namespace TestApi.Migrations
{
    public partial class transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "TransactionSet");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "TransactionSet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionSet_UserId1",
                table: "TransactionSet",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionSet_AspNetUsers_UserId1",
                table: "TransactionSet",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionSet_AspNetUsers_UserId1",
                table: "TransactionSet");

            migrationBuilder.DropIndex(
                name: "IX_TransactionSet_UserId1",
                table: "TransactionSet");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "TransactionSet");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "TransactionSet",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
