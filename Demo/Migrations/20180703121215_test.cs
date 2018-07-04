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
                    Age = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    School = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
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
                    LastReplyTime = table.Column<DateTime>(nullable: false),
                    LoseTypeID = table.Column<int>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: true),
                    hidden = table.Column<bool>(nullable: false)
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
                    LastReplyTime = table.Column<DateTime>(nullable: false),
                    LoseTypeID = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: true),
                    hidden = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PrivateMessages",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ReceiverID = table.Column<int>(nullable: true),
                    SenderID = table.Column<int>(nullable: true),
                    content = table.Column<string>(nullable: true),
                    source = table.Column<string>(nullable: true),
                    time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateMessages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PrivateMessages_Users_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PrivateMessages_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Replies",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ReplyContent = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    ownerID = table.Column<int>(nullable: true),
                    time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Replies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Replies_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Replies_Owners_ownerID",
                        column: x => x.ownerID,
                        principalTable: "Owners",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReplyComments",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    ReplierID = table.Column<int>(nullable: true),
                    ReplyContent = table.Column<string>(nullable: true),
                    UserID = table.Column<int>(nullable: true),
                    time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReplyComments_Replies_ReplierID",
                        column: x => x.ReplierID,
                        principalTable: "Replies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyComments_Users_UserID",
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

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessages_ReceiverID",
                table: "PrivateMessages",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_PrivateMessages_SenderID",
                table: "PrivateMessages",
                column: "SenderID");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_UserID",
                table: "Replies",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ownerID",
                table: "Replies",
                column: "ownerID");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyComments_ReplierID",
                table: "ReplyComments",
                column: "ReplierID");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyComments_UserID",
                table: "ReplyComments",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Finders");

            migrationBuilder.DropTable(
                name: "PrivateMessages");

            migrationBuilder.DropTable(
                name: "ReplyComments");

            migrationBuilder.DropTable(
                name: "Replies");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "LoseTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
