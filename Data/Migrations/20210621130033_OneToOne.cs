using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class OneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workshifts_Merchendisers_MerchendiserId",
                table: "Workshifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshifts_Shops_ShopId",
                table: "Workshifts");

            migrationBuilder.DropIndex(
                name: "IX_Workshifts_MerchendiserId",
                table: "Workshifts");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Workshifts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentShiftId",
                table: "Merchendisers",
                type: "int",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Workshifts_Shops_ShopId",
                table: "Workshifts",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchendisers_Workshifts_CurrentShiftId",
                table: "Merchendisers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workshifts_Shops_ShopId",
                table: "Workshifts");

            migrationBuilder.DropIndex(
                name: "IX_Merchendisers_CurrentShiftId",
                table: "Merchendisers");

            migrationBuilder.DropColumn(
                name: "CurrentShiftId",
                table: "Merchendisers");

            migrationBuilder.AlterColumn<int>(
                name: "ShopId",
                table: "Workshifts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Workshifts_Shops_ShopId",
                table: "Workshifts",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
