using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strEntityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    iEntityId = table.Column<int>(type: "int", nullable: true),
                    strAction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    iPerformedBy = table.Column<int>(type: "int", nullable: true),
                    strDataBefore = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    strDataAfter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dtTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.iId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strFirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    strLastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    strEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    strPasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    iAddedBy = table.Column<int>(type: "int", nullable: true),
                    dtCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    iModifiedBy = table.Column<int>(type: "int", nullable: true),
                    dtModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.iId);
                    table.ForeignKey(
                        name: "FK_Users_Users_iAddedBy",
                        column: x => x.iAddedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_iModifiedBy",
                        column: x => x.iModifiedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strSize = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    iStockOnHand = table.Column<int>(type: "int", nullable: false),
                    iIncomingStock = table.Column<int>(type: "int", nullable: false),
                    iAddedBy = table.Column<int>(type: "int", nullable: true),
                    dtCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    iModifiedBy = table.Column<int>(type: "int", nullable: true),
                    dtModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.iId);
                    table.ForeignKey(
                        name: "FK_ProductSizes_Users_iAddedBy",
                        column: x => x.iAddedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductSizes_Users_iModifiedBy",
                        column: x => x.iModifiedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    strDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    iAddedBy = table.Column<int>(type: "int", nullable: true),
                    dtCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    iModifiedBy = table.Column<int>(type: "int", nullable: true),
                    dtModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.iId);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Users_iAddedBy",
                        column: x => x.iAddedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductTypes_Users_iModifiedBy",
                        column: x => x.iModifiedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    iId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    strDescription = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    strColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    dblprice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductSizeId = table.Column<int>(type: "int", nullable: true),
                    iAddedBy = table.Column<int>(type: "int", nullable: true),
                    dtCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    iModifiedBy = table.Column<int>(type: "int", nullable: true),
                    dtModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.iId);
                    table.ForeignKey(
                        name: "FK_Products_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductTypes",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_iAddedBy",
                        column: x => x.iAddedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Users_iModifiedBy",
                        column: x => x.iModifiedBy,
                        principalTable: "Users",
                        principalColumn: "iId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_iAddedBy",
                table: "Products",
                column: "iAddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_iModifiedBy",
                table: "Products",
                column: "iModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductSizeId",
                table: "Products",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_iAddedBy",
                table: "ProductSizes",
                column: "iAddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_iModifiedBy",
                table: "ProductSizes",
                column: "iModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_iAddedBy",
                table: "ProductTypes",
                column: "iAddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTypes_iModifiedBy",
                table: "ProductTypes",
                column: "iModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_iAddedBy",
                table: "Users",
                column: "iAddedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_iModifiedBy",
                table: "Users",
                column: "iModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Users_strEmail",
                table: "Users",
                column: "strEmail",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
