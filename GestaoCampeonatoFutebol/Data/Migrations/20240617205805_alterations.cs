using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoCampeonatoFutebol.Data.Migrations
{
    /// <inheritdoc />
    public partial class alterations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipas_Jogos_JogoId",
                table: "Equipas");

            migrationBuilder.DropIndex(
                name: "IX_Equipas_JogoId",
                table: "Equipas");

            migrationBuilder.DropColumn(
                name: "JogoId",
                table: "Equipas");

            migrationBuilder.AddColumn<int>(
                name: "EquipaOneId",
                table: "Jogos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipaTwoId",
                table: "Jogos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Resultado",
                table: "Jogos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimeiroNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimoNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perfis_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_EquipaOneId",
                table: "Jogos",
                column: "EquipaOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_EquipaTwoId",
                table: "Jogos",
                column: "EquipaTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Perfis_UserId",
                table: "Perfis",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Equipas_EquipaOneId",
                table: "Jogos",
                column: "EquipaOneId",
                principalTable: "Equipas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Equipas_EquipaTwoId",
                table: "Jogos",
                column: "EquipaTwoId",
                principalTable: "Equipas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Equipas_EquipaOneId",
                table: "Jogos");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Equipas_EquipaTwoId",
                table: "Jogos");

            migrationBuilder.DropTable(
                name: "Perfis");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_EquipaOneId",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_EquipaTwoId",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "EquipaOneId",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "EquipaTwoId",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "Resultado",
                table: "Jogos");

            migrationBuilder.AddColumn<int>(
                name: "JogoId",
                table: "Equipas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Equipas_JogoId",
                table: "Equipas",
                column: "JogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipas_Jogos_JogoId",
                table: "Equipas",
                column: "JogoId",
                principalTable: "Jogos",
                principalColumn: "Id");
        }
    }
}
