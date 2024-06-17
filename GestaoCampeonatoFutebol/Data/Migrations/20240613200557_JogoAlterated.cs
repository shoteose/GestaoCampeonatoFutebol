using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoCampeonatoFutebol.Data.Migrations
{
    /// <inheritdoc />
    public partial class JogoAlterated : Migration
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
                name: "Equipa1Id",
                table: "Jogos",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Equipa2Id",
                table: "Jogos",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_Equipa1Id",
                table: "Jogos",
                column: "Equipa1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_Equipa2Id",
                table: "Jogos",
                column: "Equipa2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Equipas_Equipa1Id",
                table: "Jogos",
                column: "Equipa1Id",
                principalTable: "Equipas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_Equipas_Equipa2Id",
                table: "Jogos",
                column: "Equipa2Id",
                principalTable: "Equipas",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Equipas_Equipa1Id",
                table: "Jogos");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_Equipas_Equipa2Id",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_Equipa1Id",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_Equipa2Id",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "Equipa1Id",
                table: "Jogos");

            migrationBuilder.DropColumn(
                name: "Equipa2Id",
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
