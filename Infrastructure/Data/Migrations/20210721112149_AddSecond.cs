using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ModalityId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SegmentId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Segment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_CategoryId",
                table: "Stocks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ModalityId",
                table: "Stocks",
                column: "ModalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_SegmentId",
                table: "Stocks",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_TypeId",
                table: "Stocks",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Category_CategoryId",
                table: "Stocks",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Modality_ModalityId",
                table: "Stocks",
                column: "ModalityId",
                principalTable: "Modality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Segment_SegmentId",
                table: "Stocks",
                column: "SegmentId",
                principalTable: "Segment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Type_TypeId",
                table: "Stocks",
                column: "TypeId",
                principalTable: "Type",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Category_CategoryId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Modality_ModalityId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Segment_SegmentId",
                table: "Stocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Type_TypeId",
                table: "Stocks");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Modality");

            migrationBuilder.DropTable(
                name: "Segment");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_CategoryId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ModalityId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_SegmentId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_TypeId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ModalityId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "SegmentId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Stocks");
        }
    }
}
