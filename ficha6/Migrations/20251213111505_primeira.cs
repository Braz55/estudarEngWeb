using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ficha6.Migrations
{
    /// <inheritdoc />
    public partial class primeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Class__3213E83F76ACC900", x => x.id);
                });
            */
            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    class_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__student__FD291E40467E3FFA", x => x.number);
                    table.ForeignKey(
                        name: "FK__student__class_i__267ABA7A",
                        column: x => x.class_id,
                        principalTable: "Class",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_student_class_id",
                table: "student",
                column: "class_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "Class");
            */
        }
    }
}
