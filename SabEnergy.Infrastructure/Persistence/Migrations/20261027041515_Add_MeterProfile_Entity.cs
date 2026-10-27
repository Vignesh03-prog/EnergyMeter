using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_MeterProfile_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_sbe_meter_profiles",
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
                        principalTable: "tb_sbe_meter_makes",
                        principalColumn: "metermake_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_sbe_meter_profiles_tb_sbe_meter_models_model_id",
                        column: x => x.model_id,
                        principalTable: "tb_sbe_meter_models",
                        principalColumn: "meter_model_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_profiles_make_id",
                table: "tb_sbe_meter_profiles",
                column: "make_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_profiles_model_id",
                table: "tb_sbe_meter_profiles",
                column: "model_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_meter_profiles_profile_version",
                table: "tb_sbe_meter_profiles",
                column: "profile_version");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_meter_profiles");
        }
    }
}
