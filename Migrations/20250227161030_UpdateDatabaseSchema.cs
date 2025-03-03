using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace G5_MovieTicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cinemas",
                columns: table => new
                {
                    CinemaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinemaName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cinemas", x => x.CinemaId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Rating = table.Column<float>(type: "real", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "SeatTypes",
                columns: table => new
                {
                    SeatTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BasePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatTypes", x => x.SeatTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Fullname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    ScreenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CinemaId = table.Column<int>(type: "int", nullable: false),
                    ScreenName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.ScreenId);
                    table.ForeignKey(
                        name: "FK_Screens_Cinemas_CinemaId",
                        column: x => x.CinemaId,
                        principalTable: "Cinemas",
                        principalColumn: "CinemaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OrderTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenSeats",
                columns: table => new
                {
                    ScreenSeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    SeatLabel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SeatTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenSeats", x => x.ScreenSeatId);
                    table.ForeignKey(
                        name: "FK_ScreenSeats_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "ScreenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenSeats_SeatTypes_SeatTypeId",
                        column: x => x.SeatTypeId,
                        principalTable: "SeatTypes",
                        principalColumn: "SeatTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentGateway = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TransactionTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    GatewayResponse = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLogs", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_TransactionLogs_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ScreenSeatId = table.Column<int>(type: "int", nullable: false),
                    PriceCharged = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_ScreenSeats_ScreenSeatId",
                        column: x => x.ScreenSeatId,
                        principalTable: "ScreenSeats",
                        principalColumn: "ScreenSeatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeatLocks",
                columns: table => new
                {
                    SeatLockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ScreenSeatId = table.Column<int>(type: "int", nullable: false),
                    LockStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LockExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LockStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatLocks", x => x.SeatLockId);
                    table.ForeignKey(
                        name: "FK_SeatLocks_ScreenSeats_ScreenSeatId",
                        column: x => x.ScreenSeatId,
                        principalTable: "ScreenSeats",
                        principalColumn: "ScreenSeatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatLocks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Showtimes",
                columns: table => new
                {
                    ShowtimeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ScreenSeatId = table.Column<int>(type: "int", nullable: true),
                    ShowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShowTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ExperienceType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Showtimes", x => x.ShowtimeId);
                    table.ForeignKey(
                        name: "FK_Showtimes_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "MovieId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Showtimes_ScreenSeats_ScreenSeatId",
                        column: x => x.ScreenSeatId,
                        principalTable: "ScreenSeats",
                        principalColumn: "ScreenSeatId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderItemId = table.Column<int>(type: "int", nullable: false),
                    UniqueCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IssueTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketStatus = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ScannedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketId);
                    table.ForeignKey(
                        name: "FK_Tickets_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "OrderItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketScanLogs",
                columns: table => new
                {
                    ScanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    ScanTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScannedBy = table.Column<int>(type: "int", nullable: false),
                    ScanResult = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketScanLogs", x => x.ScanId);
                    table.ForeignKey(
                        name: "FK_TicketScanLogs_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketScanLogs_Users_ScannedBy",
                        column: x => x.ScannedBy,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ScreenSeatId",
                table: "OrderItems",
                column: "ScreenSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Screens_CinemaId",
                table: "Screens",
                column: "CinemaId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSeats_ScreenId",
                table: "ScreenSeats",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenSeats_SeatTypeId",
                table: "ScreenSeats",
                column: "SeatTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatLocks_ScreenSeatId",
                table: "SeatLocks",
                column: "ScreenSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatLocks_UserId",
                table: "SeatLocks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_MovieId",
                table: "Showtimes",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_Showtimes_ScreenSeatId",
                table: "Showtimes",
                column: "ScreenSeatId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_OrderItemId",
                table: "Tickets",
                column: "OrderItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TicketScanLogs_ScannedBy",
                table: "TicketScanLogs",
                column: "ScannedBy");

            migrationBuilder.CreateIndex(
                name: "IX_TicketScanLogs_TicketId",
                table: "TicketScanLogs",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_OrderId",
                table: "TransactionLogs",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatLocks");

            migrationBuilder.DropTable(
                name: "Showtimes");

            migrationBuilder.DropTable(
                name: "TicketScanLogs");

            migrationBuilder.DropTable(
                name: "TransactionLogs");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ScreenSeats");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "SeatTypes");

            migrationBuilder.DropTable(
                name: "Cinemas");
        }
    }
}
