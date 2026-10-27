using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_ParameterMaster_And_MeterParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_sbe_parameter_masters",
                columns: table => new
                {
                    parameter_master_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    display_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    unit = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_parameter_masters", x => x.parameter_master_id);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_parameters",
                columns: table => new
                {
                    meter_parameter_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    meter_profile_id = table.Column<int>(type: "integer", nullable: false),
                    parameter_master_id = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<int>(type: "integer", nullable: false),
                    data_type = table.Column<int>(type: "integer", nullable: false),
                    multiplier = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_meter_parameters", x => x.meter_parameter_id);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meter_parameters_tb_sbe_meter_profiles_meter_profile~",
                        column: x => x.meter_profile_id,
                        principalTable: "tb_sbe_meter_profiles",
                        principalColumn: "meter_profile_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meter_parameters_tb_sbe_parameter_masters_parameter_~",
                        column: x => x.parameter_master_id,
                        principalTable: "tb_sbe_parameter_masters",
                        principalColumn: "parameter_master_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_parameters_meter_profile_id",
                table: "tb_sbe_meter_parameters",
                column: "meter_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_parameters_parameter_master_id",
                table: "tb_sbe_meter_parameters",
                column: "parameter_master_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_parameter_masters_category_id",
                table: "tb_sbe_parameter_masters",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_parameter_masters_key",
                table: "tb_sbe_parameter_masters",
                column: "key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_meter_parameters");

            migrationBuilder.DropTable(
                name: "tb_sbe_parameter_masters");
        }
    }
}
