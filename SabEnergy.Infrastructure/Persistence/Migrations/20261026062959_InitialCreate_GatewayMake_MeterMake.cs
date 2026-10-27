using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_GatewayMake_MeterMake : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_makes",
                columns: table => new
                {
                    metermake_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    metermake_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_meter_makes", x => x.metermake_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_gateway_makes",
                columns: table => new
                {
                    gateway_make_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    gateway_make_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_gateway_makes", x => x.gateway_make_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_makes_metermake_name",
                table: "tb_sbe_meter_makes",
                column: "metermake_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateway_makes_gateway_make_name",
                table: "tb_sbe_gateway_makes",
                column: "gateway_make_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_meter_makes");

            migrationBuilder.DropTable(
                name: "tb_sbe_gateway_makes");
        }
    }
}
