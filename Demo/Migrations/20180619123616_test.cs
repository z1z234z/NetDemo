using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Demo.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Account = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Last_Login_Time = table.Column<DateTime>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "LoseTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoseTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Account = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Age = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    School = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Finders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Answer = table.Column<string>(nullable: true),
                    Complete = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    LoseTypeID = table.Column<int>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Finders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Finders_LoseTypes_LoseTypeID",
                        column: x => x.LoseTypeID,
                        principalTable: "LoseTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Finders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Complete = table.Column<bool>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    LoseTypeID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Owners_LoseTypes_LoseTypeID",
                        column: x => x.LoseTypeID,
                        principalTable: "LoseTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Owners_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Finders_LoseTypeID",
                table: "Finders",
                column: "LoseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Finders_UserID",
                table: "Finders",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_LoseTypeID",
                table: "Owners",
                column: "LoseTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Owners_UserID",
                table: "Owners",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Finders");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "LoseTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
