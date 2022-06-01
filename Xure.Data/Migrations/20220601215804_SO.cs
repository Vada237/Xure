using Microsoft.EntityFrameworkCore.Migrations;

namespace Xure.Data.Migrations
{
    public partial class SO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerOrder_Delivery_DeliveryId",
                table: "SellerOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerOrder_Orders_OrderId",
                table: "SellerOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerOrder",
                table: "SellerOrder");

            migrationBuilder.RenameTable(
                name: "SellerOrder",
                newName: "SellerOrders");

            migrationBuilder.RenameIndex(
                name: "IX_SellerOrder_OrderId",
                table: "SellerOrders",
                newName: "IX_SellerOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerOrder_DeliveryId",
                table: "SellerOrders",
                newName: "IX_SellerOrders_DeliveryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerOrders",
                table: "SellerOrders",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ab2bff8-07be-441b-82d6-0d91174ad815",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "6ea7e841-c4a9-40c5-b1f0-131832ed6f29", "97e12bfa-6164-44f2-9eff-ac6d6c87d2f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "221a163b-8960-42f9-a19e-023493311599",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "dcdab130-c5d3-4924-a42f-6419ac4994ff", "1015f6f4-0e90-4669-bc76-d53b1c274a1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a522bd3-d3d6-4f6c-bd0e-f00e38a89b86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "a2972523-5af6-4b90-9d02-8abac509d10c", "a0511c81-960a-4ec0-a0d9-74ba025a3afc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c49e0b3a-bebc-47d3-b65d-e2da531830ae",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5a708f24-8818-45fb-9d72-4de524fe5f43", "7f8b8b1b-7426-4b3d-81f1-e1ebb8aca56f" });

            migrationBuilder.AddForeignKey(
                name: "FK_SellerOrders_Delivery_DeliveryId",
                table: "SellerOrders",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerOrders_Orders_OrderId",
                table: "SellerOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SellerOrders_Delivery_DeliveryId",
                table: "SellerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SellerOrders_Orders_OrderId",
                table: "SellerOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SellerOrders",
                table: "SellerOrders");

            migrationBuilder.RenameTable(
                name: "SellerOrders",
                newName: "SellerOrder");

            migrationBuilder.RenameIndex(
                name: "IX_SellerOrders_OrderId",
                table: "SellerOrder",
                newName: "IX_SellerOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_SellerOrders_DeliveryId",
                table: "SellerOrder",
                newName: "IX_SellerOrder_DeliveryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SellerOrder",
                table: "SellerOrder",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ab2bff8-07be-441b-82d6-0d91174ad815",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b66b31eb-3b99-42be-985d-21b08b81518a", "452dbee1-298c-4ae5-a946-b6dd73fd0223" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "221a163b-8960-42f9-a19e-023493311599",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "462a9574-82a7-466e-bbab-d1144d4cc219", "15b13a36-6c09-434d-980e-bdddcbf2858b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a522bd3-d3d6-4f6c-bd0e-f00e38a89b86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3a6f89c0-d02c-4544-8db0-e46bfb0c2d78", "951da589-7ee9-4fb0-ad40-a22c06c0a08c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c49e0b3a-bebc-47d3-b65d-e2da531830ae",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "f4736cc6-40d5-4fa4-b3da-2687c1798bdb", "383a1ca8-b2bc-4c52-8ab4-63eceff04663" });

            migrationBuilder.AddForeignKey(
                name: "FK_SellerOrder_Delivery_DeliveryId",
                table: "SellerOrder",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SellerOrder_Orders_OrderId",
                table: "SellerOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
