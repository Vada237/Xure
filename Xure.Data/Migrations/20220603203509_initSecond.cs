using Microsoft.EntityFrameworkCore.Migrations;

namespace Xure.Data.Migrations
{
    public partial class initSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ReceptionPoints",
                columns: new[] { "id", "Address", "OpenTime", "CloseTime"},
                values: new object[,]
                {
                    { 1, "Москва, ул.Лестева, д.9", "7:30:30", "20:30:00"},
                    { 2, "Воронеж, ул.3 Интернационала, д.35" , "8:30:30", "21:30:00"},
                    { 3, "Ростов-на-Дону, пер. Журавлева, д.127" , "8:30:30", "21:30:00"},
                    { 4, "Ставрополь, ул. Ломоносова, д.30" , "7:30:30", "20:30:00"}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ReceptionPoints",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReceptionPoints",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ReceptionPoints",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReceptionPoints",
                keyColumn: "id",
                keyValue: 4);
        }
    }
}
