using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduCare.User.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersContactInformations",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContactType = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsPrimary = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersContactInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersContactInformations_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "User",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersContactInformations_Address_ContactType",
                schema: "User",
                table: "UsersContactInformations",
                columns: new[] { "Address", "ContactType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersContactInformations_Address_ContactType_IsPrimary",
                schema: "User",
                table: "UsersContactInformations",
                columns: new[] { "Address", "ContactType", "IsPrimary" });

            migrationBuilder.CreateIndex(
                name: "IX_UsersContactInformations_UserId",
                schema: "User",
                table: "UsersContactInformations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersContactInformations",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "User");
        }
    }
}
