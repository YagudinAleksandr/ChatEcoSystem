using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatEcoSystem.Notification.Logic.Data.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnreadMessages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(nullable: false),
                    ReceiverId = table.Column<string>(nullable: false),
                    ReceiverEmail = table.Column<string>(nullable: false),
                    SenderId = table.Column<string>(nullable: true),
                    SenderName = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    SentTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnreadMessages", x => x.MessageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnreadMessages_ReceiverId",
                table: "UnreadMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_UnreadMessages_SentTime",
                table: "UnreadMessages",
                column: "SentTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnreadMessages");
        }
    }
}
