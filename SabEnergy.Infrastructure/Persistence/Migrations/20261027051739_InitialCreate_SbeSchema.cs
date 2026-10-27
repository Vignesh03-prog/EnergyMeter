using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_SbeSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sbe");

            migrationBuilder.CreateTable(
                name: "tb_sbe_gateway_makes",
                schema: "sbe",
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

            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_makes",
                schema: "sbe",
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
                name: "tb_sbe_parameter_masters",
                schema: "sbe",
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
                name: "tb_sbe_gateway_models",
                schema: "sbe",
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
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_gateway_makes",
                        principalColumn: "gateway_make_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_models",
                schema: "sbe",
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
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_meter_makes",
                        principalColumn: "metermake_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_gateways",
                schema: "sbe",
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
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_gateway_makes",
                        principalColumn: "gateway_make_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_gateways_tb_sbe_gateway_models_gateway_model_id",
                        column: x => x.gateway_model_id,
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_gateway_models",
                        principalColumn: "gateway_model_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_profiles",
                schema: "sbe",
                columns: table => new
                {
                    meter_profile_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    profile_version = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    make_id = table.Column<int>(type: "integer", nullable: false),
                    model_id = table.Column<int>(type: "integer", nullable: false),
                    address_offset = table.Column<int>(type: "integer", nullable: false),
                    holding_register = table.Column<int>(type: "integer", nullable: false),
                    byte_order = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_meter_profiles", x => x.meter_profile_id);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meter_profiles_tb_sbe_meter_makes_make_id",
                        column: x => x.make_id,
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_meter_makes",
                        principalColumn: "metermake_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meter_profiles_tb_sbe_meter_models_model_id",
                        column: x => x.model_id,
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_meter_models",
                        principalColumn: "meter_model_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_meters",
                schema: "sbe",
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
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_meter_makes",
                        principalColumn: "metermake_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meters_tb_sbe_meter_models_meter_model_id",
                        column: x => x.meter_model_id,
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_meter_models",
                        principalColumn: "meter_model_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_parameters",
                schema: "sbe",
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
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_meter_profiles",
                        principalColumn: "meter_profile_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meter_parameters_tb_sbe_parameter_masters_parameter_~",
                        column: x => x.parameter_master_id,
                        principalSchema: "sbe",
                        principalTable: "tb_sbe_parameter_masters",
                        principalColumn: "parameter_master_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateway_makes_gateway_make_name",
                schema: "sbe",
                table: "tb_sbe_gateway_makes",
                column: "gateway_make_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateway_models_gateway_make_id",
                schema: "sbe",
                table: "tb_sbe_gateway_models",
                column: "gateway_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateway_models_gateway_model_name",
                schema: "sbe",
                table: "tb_sbe_gateway_models",
                column: "gateway_model_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_device_name",
                schema: "sbe",
                table: "tb_sbe_gateways",
                column: "device_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_gateway_make_id",
                schema: "sbe",
                table: "tb_sbe_gateways",
                column: "gateway_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_gateway_model_id",
                schema: "sbe",
                table: "tb_sbe_gateways",
                column: "gateway_model_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_gateways_serial_no",
                schema: "sbe",
                table: "tb_sbe_gateways",
                column: "serial_no");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_makes_metermake_name",
                schema: "sbe",
                table: "tb_sbe_meter_makes",
                column: "metermake_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_models_meter_make_id",
                schema: "sbe",
                table: "tb_sbe_meter_models",
                column: "meter_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_models_meter_model_name",
                schema: "sbe",
                table: "tb_sbe_meter_models",
                column: "meter_model_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_parameters_meter_profile_id",
                schema: "sbe",
                table: "tb_sbe_meter_parameters",
                column: "meter_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_parameters_parameter_master_id",
                schema: "sbe",
                table: "tb_sbe_meter_parameters",
                column: "parameter_master_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_profiles_make_id",
                schema: "sbe",
                table: "tb_sbe_meter_profiles",
                column: "make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_profiles_model_id",
                schema: "sbe",
                table: "tb_sbe_meter_profiles",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_profiles_profile_version",
                schema: "sbe",
                table: "tb_sbe_meter_profiles",
                column: "profile_version");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_custom_name",
                schema: "sbe",
                table: "tb_sbe_meters",
                column: "custom_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_meter_make_id",
                schema: "sbe",
                table: "tb_sbe_meters",
                column: "meter_make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_meter_model_id",
                schema: "sbe",
                table: "tb_sbe_meters",
                column: "meter_model_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meters_serial_no",
                schema: "sbe",
                table: "tb_sbe_meters",
                column: "serial_no");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_parameter_masters_category_id",
                schema: "sbe",
                table: "tb_sbe_parameter_masters",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_parameter_masters_key",
                schema: "sbe",
                table: "tb_sbe_parameter_masters",
                column: "key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_gateways",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_meter_parameters",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_meters",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_gateway_models",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_meter_profiles",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_parameter_masters",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_gateway_makes",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_meter_models",
                schema: "sbe");

            migrationBuilder.DropTable(
                name: "tb_sbe_meter_makes",
                schema: "sbe");
        }
    }
}
