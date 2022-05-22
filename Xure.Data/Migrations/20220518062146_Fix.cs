using Microsoft.EntityFrameworkCore.Migrations;

namespace Xure.Data.Migrations
{
    public partial class Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Storages_StorageId",
                table: "Delivery");

            migrationBuilder.DropTable(
                name: "Storages");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_StorageId",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Delivery");

            migrationBuilder.AddColumn<int>(
                name: "ReceprtionPointId",
                table: "Delivery",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_ReceprtionPointId",
                table: "Delivery",
                column: "ReceprtionPointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_ReceptionPoints_ReceprtionPointId",
                table: "Delivery",
                column: "ReceprtionPointId",
                principalTable: "ReceptionPoints",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_ReceptionPoints_ReceprtionPointId",
                table: "Delivery");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_ReceprtionPointId",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "ReceprtionPointId",
                table: "Delivery");

            migrationBuilder.AddColumn<byte>(
                name: "StorageId",
                table: "Delivery",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    id = table.Column<byte>(type: "tinyint", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_StorageId",
                table: "Delivery",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Storages_StorageId",
                table: "Delivery",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
