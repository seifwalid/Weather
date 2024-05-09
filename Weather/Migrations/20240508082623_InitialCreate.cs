using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Weather.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissibleLimits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxTemperature = table.Column<double>(type: "float", nullable: false),
                    MaxHumidity = table.Column<double>(type: "float", nullable: false),
                    MaxTVOC = table.Column<double>(type: "float", nullable: false),
                    MaxCO2 = table.Column<double>(type: "float", nullable: false),
                    MaxPM1 = table.Column<double>(type: "float", nullable: false),
                    MaxPM2_5 = table.Column<double>(type: "float", nullable: false),
                    MaxPM10 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissibleLimits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPermissibleLimitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_PermissibleLimits_UserPermissibleLimitsId",
                        column: x => x.UserPermissibleLimitsId,
                        principalTable: "PermissibleLimits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPermissibleLimitsId",
                table: "Users",
                column: "UserPermissibleLimitsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PermissibleLimits");
        }
    }
}
