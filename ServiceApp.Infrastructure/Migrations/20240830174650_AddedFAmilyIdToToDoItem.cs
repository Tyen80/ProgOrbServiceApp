using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFAmilyIdToToDoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FamilyId",
                table: "ToDoItems",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyId",
                table: "ToDoItems");
        }
    }
}
