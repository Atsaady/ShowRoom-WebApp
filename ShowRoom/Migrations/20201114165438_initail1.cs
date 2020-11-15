using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShowRoom.Migrations
{
    public partial class initail1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartBeforeOrder");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartBeforeOrder",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClothingsId = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    tran = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartBeforeOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartBeforeOrder_Clothings_ClothingsId",
                        column: x => x.ClothingsId,
                        principalTable: "Clothings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartBeforeOrder_ClothingsId",
                table: "CartBeforeOrder",
                column: "ClothingsId");
        }
    }
}
