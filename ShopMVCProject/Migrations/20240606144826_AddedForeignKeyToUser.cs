using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopMVCProject.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeyToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIdInt",
                table: "ShoppingCarts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserIdInt",
                value: null);

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserIdInt",
                value: null);

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserIdInt",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserIdInt",
                table: "ShoppingCarts",
                column: "UserIdInt");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserIdInt",
                table: "ShoppingCarts",
                column: "UserIdInt",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_UserIdInt",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_UserIdInt",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "UserIdInt",
                table: "ShoppingCarts");
        }
    }
}
