using Microsoft.EntityFrameworkCore.Migrations;

namespace Gym.Migrations
{
    public partial class imagesgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Images",
                newName: "photo");

            migrationBuilder.CreateTable(
                name: "GroupDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupDTO", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupDTO");

            migrationBuilder.RenameColumn(
                name: "photo",
                table: "Images",
                newName: "Image");
        }
    }
}
