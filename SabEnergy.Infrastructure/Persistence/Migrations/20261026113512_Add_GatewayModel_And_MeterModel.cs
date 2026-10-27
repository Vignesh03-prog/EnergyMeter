using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_GatewayModel_And_MeterModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_sbe_gateway_models",
                columns: table => new
                {
                    gateway_model_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gateway_model_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    gateway_make_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_gateway_models", x => x.gateway_model_id);
                    table.ForeignKey(
                        name: "FK_tb_sbe_gateway_models_tb_sbe_gateway_makes_gateway_make_id",
                        column: x => x.gateway_make_id,
                        principalTable: "tb_sbe_gateway_makes",
                        principalColumn: "gateway_make_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_models",
                columns: table => new
                {
                    meter_model_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    meter_model_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    meter_make_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_meter_models", x => x.meter_model_id);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meter_models_tb_sbe_meter_makes_meter_make_id",
                        column: x => x.meter_make_id,
                        principalTable: "tb_sbe_meter_makes",
                        principalColumn: "metermake_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateway_models_gateway_make_id",
                table: "tb_sbe_gateway_models",
                column: "gateway_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateway_models_gateway_model_name",
                table: "tb_sbe_gateway_models",
                column: "gateway_model_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_models_meter_make_id",
                table: "tb_sbe_meter_models",
                column: "meter_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_models_meter_model_name",
                table: "tb_sbe_meter_models",
                column: "meter_model_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_gateway_models");

            migrationBuilder.DropTable(
                name: "tb_sbe_meter_models");
        }
    }
}
