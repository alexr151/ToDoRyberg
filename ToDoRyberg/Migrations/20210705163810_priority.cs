using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoRyberg.Migrations
{
    public partial class priority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PriorityId",
                table: "ToDos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    PriorityId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.PriorityId);
                });

            migrationBuilder.InsertData(
                table: "Priorities",
                columns: new[] { "PriorityId", "Name" },
                values: new object[,]
                {
                    { "urgent", "Urgent" },
                    { "high", "High" },
                    { "moderate", "Moderate" },
                    { "low", "Low" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { "inprogress", "In Progress" },
                    { "qa", "Quality Assurance" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDos_PriorityId",
                table: "ToDos",
                column: "PriorityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Priorities_PriorityId",
                table: "ToDos",
                column: "PriorityId",
                principalTable: "Priorities",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Priorities_PriorityId",
                table: "ToDos");

            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_PriorityId",
                table: "ToDos");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: "inprogress");

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: "qa");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "ToDos");
        }
    }
}
