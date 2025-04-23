using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainComponentManagement.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UniqueNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CanAssignQuantity = table.Column<bool>(type: "boolean", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainComponents", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TrainComponents",
                columns: new[] { "Id", "CanAssignQuantity", "Name", "Quantity", "UniqueNumber" },
                values: new object[,]
                {
                    { 1, false, "Engine", null, "ENG123" },
                    { 2, false, "Passenger Car", null, "PAS456" },
                    { 3, false, "Freight Car", null, "FRT789" },
                    { 4, true, "Wheel", 0, "WHL101" },
                    { 5, true, "Seat", 0, "STS234" },
                    { 6, true, "Window", 0, "WIN567" },
                    { 7, true, "Door", 0, "DR123" },
                    { 8, true, "Control Panel", 0, "CTL987" },
                    { 9, true, "Light", 0, "LGT456" },
                    { 10, true, "Brake", 0, "BRK789" },
                    { 11, true, "Bolt", 0, "BLT321" },
                    { 12, true, "Nut", 0, "NUT654" },
                    { 13, false, "Engine Hood", null, "EH789" },
                    { 14, false, "Axle", null, "AX456" },
                    { 15, false, "Piston", null, "PST789" },
                    { 16, true, "Handrail", 0, "HND234" },
                    { 17, true, "Step", 0, "STP567" },
                    { 18, false, "Roof", null, "RF123" },
                    { 19, false, "Air Conditioner", null, "AC789" },
                    { 20, false, "Flooring", null, "FLR456" },
                    { 21, true, "Mirror", 0, "MRR789" },
                    { 22, false, "Horn", null, "HRN321" },
                    { 23, false, "Coupler", null, "CPL654" },
                    { 24, true, "Hinge", 0, "HNG987" },
                    { 25, true, "Ladder", 0, "LDR456" },
                    { 26, false, "Paint", null, "PNT789" },
                    { 27, true, "Decal", 0, "DCL321" },
                    { 28, true, "Gauge", 0, "GGS654" },
                    { 29, false, "Battery", null, "BTR987" },
                    { 30, false, "Radiator", null, "RDR456" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainComponents_UniqueNumber",
                table: "TrainComponents",
                column: "UniqueNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainComponents");
        }
    }
}
