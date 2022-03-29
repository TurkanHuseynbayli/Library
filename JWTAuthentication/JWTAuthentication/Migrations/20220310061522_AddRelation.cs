using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAuthentication.Migrations
{
    public partial class AddRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Books_BookId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_BookId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Location");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_LocationId",
                table: "Books",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Location_LocationId",
                table: "Books",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Location_LocationId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LocationId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Location",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Location_BookId",
                table: "Location",
                column: "BookId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Books_BookId",
                table: "Location",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
