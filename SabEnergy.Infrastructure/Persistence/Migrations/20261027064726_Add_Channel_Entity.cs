using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SabEnergy.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Channel_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_sbe_channels",
                columns: table => new
                {
                    channel_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    channel_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    gateway_id = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    updated_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_sbe_channels", x => x.channel_id);
                    table.ForeignKey(
                        name: "FK_tb_sbe_channels_tb_sbe_gateways_gateway_id",
                        column: x => x.gateway_id,
                        principalTable: "tb_sbe_gateways",
                        principalColumn: "gateway_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_channels_channel_name",
                table: "tb_sbe_channels",
                column: "channel_name");

            migrationBuilder.CreateIndex(
                name: "IX_tb_sbe_channels_gateway_id",
                table: "tb_sbe_channels",
                column: "gateway_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_sbe_channels");
        }
    }
}
