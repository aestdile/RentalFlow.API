using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalFlow.API.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GuestAndHomeRelationshipDeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Homes_HomeId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_HomeId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "HomeId",
                table: "Guests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "HomeId",
                table: "Guests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_HomeId",
                table: "Guests",
                column: "HomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Homes_HomeId",
                table: "Guests",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
