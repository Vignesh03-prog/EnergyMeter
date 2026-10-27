using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Meter_Gateway_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_sbe_meters",
                columns: table => new
                {
                    meter_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    serial_no = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    custom_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    channel_id = table.Column<int>(type: "integer", nullable: false),
                    slave_id = table.Column<int>(type: "integer", nullable: false),
                    device_profile_id = table.Column<int>(type: "integer", nullable: false),
                    meter_make_id = table.Column<int>(type: "integer", nullable: false),
                    meter_model_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    baud_rate = table.Column<int>(type: "integer", nullable: false),
                    parity_bit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    stop_bit = table.Column<int>(type: "integer", nullable: false),
                    data_bit = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_meters", x => x.meter_id);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meters_tb_sbe_meter_makes_meter_make_id",
                        column: x => x.meter_make_id,
                        principalTable: "tb_sbe_meter_makes",
                        principalColumn: "metermake_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meters_tb_sbe_meter_models_meter_model_id",
                        column: x => x.meter_model_id,
                        principalTable: "tb_sbe_meter_models",
                        principalColumn: "meter_model_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_custom_name",
                table: "tb_sbe_meters",
                column: "custom_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_meter_make_id",
                table: "tb_sbe_meters",
                column: "meter_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_meter_model_id",
                table: "tb_sbe_meters",
                column: "meter_model_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_serial_no",
                table: "tb_sbe_meters",
                column: "serial_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_meters");
        }
    }
}
