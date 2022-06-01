using Microsoft.EntityFrameworkCore.Migrations;

namespace Xure.Data.Migrations
{
    public partial class FixImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ab2bff8-07be-441b-82d6-0d91174ad815",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "cf17c98e-c90d-4238-8f28-8f214b7e2c78", "a60693d8-96d8-40c3-a859-9922e400637e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "221a163b-8960-42f9-a19e-023493311599",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ab076ea9-96c2-4cd8-8eed-2235fcdbf64e", "e209ede8-ff25-4a61-9e75-ea52c3770a47" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6a522bd3-d3d6-4f6c-bd0e-f00e38a89b86",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9c457a45-14a4-4a53-aaea-930c1a943f9f", "0bdca534-8b32-4207-897f-b645bc33507f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c49e0b3a-bebc-47d3-b65d-e2da531830ae",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5430cf26-1060-437c-81dd-0fa8f9b3276c", "560e9185-2663-40d3-9a2e-077dd0e95180" });
        }
    }
}
