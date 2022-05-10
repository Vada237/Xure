using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Xure.Data.Migrations
{
    public partial class AddDeliverySellerOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Storages_StorageId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StorageId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StorageId = table.Column<byte>(type: "tinyint", nullable: false),
                    DepartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SellerOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    DeliveryId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellerOrder_Delivery_DeliveryId",
                        column: x => x.DeliveryId,
                        principalTable: "Delivery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellerOrder_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_StorageId",
                table: "Delivery",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerOrder_DeliveryId",
                table: "SellerOrder",
                column: "DeliveryId");

            migrationBuilder.CreateIndex(
                name: "IX_SellerOrder_OrderId",
                table: "SellerOrder",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellerOrder");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.AddColumn<byte>(
                name: "StorageId",
                table: "Orders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StorageId",
                table: "Orders",
                column: "StorageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Storages_StorageId",
                table: "Orders",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
