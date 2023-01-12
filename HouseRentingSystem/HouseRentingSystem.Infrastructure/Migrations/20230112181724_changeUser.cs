using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    public partial class changeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "112ea4e6-940b-4da4-a786-b3735b9244cd", "AQAAAAEAACcQAAAAEJt2Ka7QU32UraqRoPP9Ra680u/aZfgyxcH3/eu0qn6Py9Q3Kl1sgj0T6d/EVqsIGA==", "e179003c-67e2-4405-aa25-08b46b1dc64f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91d3b3ce-115a-428e-b838-7093d4755179", "AQAAAAEAACcQAAAAELx1KoMh2IxTWj/BoQATVzdbrxl6cak/5GrLBqjzPfdJ3jwF0D34b63I+bsF0oINhw==", "3929dfc5-06ec-4e16-9b47-60330163e79b" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d80bc8f2-00d0-4e13-b60e-a8a38c1a42d8", "AQAAAAEAACcQAAAAEF/IK8iGqPgP6KjvcUge5A2xLDPjKNA6NyLJ9MHkb0/euG/IgSF6yynJzrnfRXEvdw==", "014a853b-7ddf-44ab-b065-faa45c3d2a15" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c219f212-c27e-4fb0-b151-ba1079f5206d", "AQAAAAEAACcQAAAAEELJqMSGStkuZLRKJ0jSSh0PYTH1KnzIaaY6oC6KyCUBnsjhAFR/E9FKR0l1v7gRMw==", "9bbb4255-d53f-4b72-807b-8cb064ffe987" });
        }
    }
}
