using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DeleteShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchendisers_Workshifts_CurrentShiftId",
                table: "Merchendisers");

            migrationBuilder.DropIndex(
                name: "IX_Merchendisers_CurrentShiftId",
                table: "Merchendisers");

            migrationBuilder.CreateIndex(
                name: "IX_Workshifts_MerchendiserId",
                table: "Workshifts",
                column: "MerchendiserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Workshifts_Merchendisers_MerchendiserId",
                table: "Workshifts",
                column: "MerchendiserId",
                principalTable: "Merchendisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workshifts_Merchendisers_MerchendiserId",
                table: "Workshifts");

            migrationBuilder.DropIndex(
                name: "IX_Workshifts_MerchendiserId",
                table: "Workshifts");

            migrationBuilder.CreateIndex(
                name: "IX_Merchendisers_CurrentShiftId",
                table: "Merchendisers",
                column: "CurrentShiftId",
                unique: true,
                filter: "[CurrentShiftId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchendisers_Workshifts_CurrentShiftId",
                table: "Merchendisers",
                column: "CurrentShiftId",
                principalTable: "Workshifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
