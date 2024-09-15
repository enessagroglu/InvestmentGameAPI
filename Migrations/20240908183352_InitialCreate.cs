using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestmentGameAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentStockPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalShares = table.Column<int>(type: "int", nullable: false),
                    MarketCap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InGameItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InGameItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserCompanyOwnerships",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    NumberOfShares = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCompanyOwnerships", x => new { x.UserId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_UserCompanyOwnerships_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCompanyOwnerships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserItemOwnerships",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserItemOwnerships", x => new { x.UserId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_UserItemOwnerships_InGameItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "InGameItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserItemOwnerships_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyOwnerships_CompanyId",
                table: "UserCompanyOwnerships",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCompanyOwnerships_UserId",
                table: "UserCompanyOwnerships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItemOwnerships_ItemId",
                table: "UserItemOwnerships",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserItemOwnerships_UserId",
                table: "UserItemOwnerships",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCompanyOwnerships");

            migrationBuilder.DropTable(
                name: "UserItemOwnerships");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "InGameItems");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
