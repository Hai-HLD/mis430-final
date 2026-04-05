using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransparencyPortal.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "default_questions",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    slug = table.Column<string>(type: "varchar(128)", maxLength: 128, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    title = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", precision: 6, nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_default_questions", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "preset_responses",
                columns: table => new
                {
                    id = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    default_question_id = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    body_text = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    version = table.Column<int>(type: "int", nullable: false),
                    published_at = table.Column<DateTime>(type: "datetime(6)", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_preset_responses", x => x.id);
                    table.ForeignKey(
                        name: "fk_preset_responses_default_questions_default_question_id",
                        column: x => x.default_question_id,
                        principalTable: "default_questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "inquiries",
                columns: table => new
                {
                    id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    path_type = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    default_question_id = table.Column<ulong>(type: "bigint unsigned", nullable: true),
                    elaboration_text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    custom_question_text = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    preset_response_id = table.Column<ulong>(type: "bigint unsigned", nullable: true),
                    first_response_at = table.Column<DateTime>(type: "datetime(6)", precision: 6, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", precision: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inquiries", x => x.id);
                    table.ForeignKey(
                        name: "fk_inquiries_default_questions_default_question_id",
                        column: x => x.default_question_id,
                        principalTable: "default_questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_inquiries_preset_responses_preset_response_id",
                        column: x => x.preset_response_id,
                        principalTable: "preset_responses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_default_questions_slug",
                table: "default_questions",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_inquiries_created_at",
                table: "inquiries",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "ix_inquiries_default_question_id",
                table: "inquiries",
                column: "default_question_id");

            migrationBuilder.CreateIndex(
                name: "ix_inquiries_path_type",
                table: "inquiries",
                column: "path_type");

            migrationBuilder.CreateIndex(
                name: "ix_inquiries_preset_response_id",
                table: "inquiries",
                column: "preset_response_id");

            migrationBuilder.CreateIndex(
                name: "ix_preset_responses_default_question_id",
                table: "preset_responses",
                column: "default_question_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inquiries");

            migrationBuilder.DropTable(
                name: "preset_responses");

            migrationBuilder.DropTable(
                name: "default_questions");
        }
    }
}
