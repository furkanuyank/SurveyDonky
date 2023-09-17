using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyApp.Migrations.Survey
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "survey");

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "survey",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "surveys",
                schema: "survey",
                columns: table => new
                {
                    survey_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    will_published = table.Column<int>(type: "int", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    description = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true),
                    category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_surveys", x => x.survey_id);
                    table.ForeignKey(
                        name: "category_survey_id_fk",
                        column: x => x.category_id,
                        principalSchema: "survey",
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pollster",
                schema: "survey",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ip = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    survey_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pollster", x => x.id);
                    table.ForeignKey(
                        name: "survey_pollster_id_fk",
                        column: x => x.survey_id,
                        principalSchema: "survey",
                        principalTable: "surveys",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                schema: "survey",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_text = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    survey_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.question_id);
                    table.ForeignKey(
                        name: "survey_question_id_fk",
                        column: x => x.survey_id,
                        principalSchema: "survey",
                        principalTable: "surveys",
                        principalColumn: "survey_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                schema: "survey",
                columns: table => new
                {
                    answer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    choice = table.Column<int>(type: "int", nullable: false),
                    choice_index = table.Column<int>(type: "int", nullable: false),
                    answer_text = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    question_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answers", x => x.answer_id);
                    table.ForeignKey(
                        name: "s_question_answer_id_fk",
                        column: x => x.question_id,
                        principalSchema: "survey",
                        principalTable: "questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answers_question_id",
                schema: "survey",
                table: "answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_pollster_survey_id",
                schema: "survey",
                table: "pollster",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_survey_id",
                schema: "survey",
                table: "questions",
                column: "survey_id");

            migrationBuilder.CreateIndex(
                name: "IX_surveys_category_id",
                schema: "survey",
                table: "surveys",
                column: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers",
                schema: "survey");

            migrationBuilder.DropTable(
                name: "pollster",
                schema: "survey");

            migrationBuilder.DropTable(
                name: "questions",
                schema: "survey");

            migrationBuilder.DropTable(
                name: "surveys",
                schema: "survey");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "survey");
        }
    }
}
