using Microsoft.EntityFrameworkCore.Migrations;

namespace Gym.Migrations
{
    public partial class AddCourses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Courses_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "3b20ebac-96d5-4bc4-a1b5-37c9f577b6c1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a27aba12-03d7-43a9-ba8d-24a20e576604", "AQAAAAEAACcQAAAAEE9pqm69Fq1l5nzU67KA+pvTw0TiRTMGMbpRUZZMGcghQ33Qk+ciPONdVeQmDS/wxQ==", "388cd2f2-71e6-467f-8556-2b2d61e0608a" });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DoctorId",
                table: "Courses",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5e174e-3b0e-446f-86af-483d56fd7210",
                column: "ConcurrencyStamp",
                value: "7b34ebfd-5fb6-4072-b545-81f94bd2edde");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9064fec7-9825-4fb2-8444-680e4e780e8b", "AQAAAAEAACcQAAAAEHaY/uCHQ/4QAJZV0gw5U8lbORBm4VTBg9Cp6h6++MXsPF6Pmw7Ihsc2hTfn05KTBw==", "1725d8a3-2fe7-48f8-9c64-404da4fc57a9" });
        }
    }
}
