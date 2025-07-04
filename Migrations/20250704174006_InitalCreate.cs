using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeagueOfLegendsCharachters.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Charachters",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolePosition = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    BlueEssence = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charachters", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Damage = table.Column<int>(type: "int", nullable: false),
                    Armor = table.Column<int>(type: "int", nullable: false),
                    MagicResist = table.Column<int>(type: "int", nullable: false),
                    MovementSpeed = table.Column<int>(type: "int", nullable: false),
                    AttackRange = table.Column<int>(type: "int", nullable: false),
                    CharachterName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_Statuses_Charachters_CharachterName",
                        column: x => x.CharachterName,
                        principalTable: "Charachters",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_CharachterName",
                table: "Statuses",
                column: "CharachterName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "Charachters");
        }
    }
}
