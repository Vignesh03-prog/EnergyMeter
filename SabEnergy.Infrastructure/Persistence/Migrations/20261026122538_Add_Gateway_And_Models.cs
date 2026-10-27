using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Gateway_And_Models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_sbe_gateways",
                columns: table => new
                {
                    gateway_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    device_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    serial_no = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    gateway_make_id = table.Column<int>(type: "integer", nullable: false),
                    gateway_model_id = table.Column<int>(type: "integer", nullable: true),
                    ip_address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    mac_address = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    channel_count = table.Column<int>(type: "integer", nullable: false),
                    firmware_version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    keepalive_time = table.Column<int>(type: "integer", nullable: false),
                    wifi_ssid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    wifi_port = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_gateways", x => x.gateway_id);
                    table.ForeignKey(
                        name: "FK_tb_sbe_gateways_tb_sbe_gateway_makes_gateway_make_id",
                        column: x => x.gateway_make_id,
                        principalTable: "tb_sbe_gateway_makes",
                        principalColumn: "gateway_make_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_gateways_tb_sbe_gateway_models_gateway_model_id",
                        column: x => x.gateway_model_id,
                        principalTable: "tb_sbe_gateway_models",
                        principalColumn: "gateway_model_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_device_name",
                table: "tb_sbe_gateways",
                column: "device_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_gateway_make_id",
                table: "tb_sbe_gateways",
                column: "gateway_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_gateway_model_id",
                table: "tb_sbe_gateways",
                column: "gateway_model_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_serial_no",
                table: "tb_sbe_gateways",
                column: "serial_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_gateways");
        }
    }
}
