using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyApp.Migrations.Test
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "test");

            migrationBuilder.CreateTable(
                name: "tests",
                schema: "test",
                columns: table => new
                {
                    test_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    will_published = table.Column<int>(type: "int", nullable: false),
                    created_datetime = table.Column<DateTime>(type: "datetime", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    will_send = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(90)", maxLength: 90, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tests", x => x.test_id);
                });

            migrationBuilder.CreateTable(
                name: "pollsters",
                schema: "test",
                columns: table => new
                {
                    pollster_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    school_id = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    correct = table.Column<int>(type: "int", nullable: false),
                    wrong = table.Column<int>(type: "int", nullable: false),
                    test_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pollsters", x => x.pollster_id);
                    table.ForeignKey(
                        name: "test_pollster_id_fk",
                        column: x => x.test_id,
                        principalSchema: "test",
                        principalTable: "tests",
                        principalColumn: "test_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                schema: "test",
                columns: table => new
                {
                    question_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question_text = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true),
                    correct_answer = table.Column<int>(type: "int", nullable: false),
                    test_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.question_id);
                    table.ForeignKey(
                        name: "test_question_id_fk",
                        column: x => x.test_id,
                        principalSchema: "test",
                        principalTable: "tests",
                        principalColumn: "test_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "answers",
                schema: "test",
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
                        name: "t_question_answer_id_fk",
                        column: x => x.question_id,
                        principalSchema: "test",
                        principalTable: "questions",
                        principalColumn: "question_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answers_question_id",
                schema: "test",
                table: "answers",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "IX_pollsters_test_id",
                schema: "test",
                table: "pollsters",
                column: "test_id");

            migrationBuilder.CreateIndex(
                name: "IX_questions_test_id",
                schema: "test",
                table: "questions",
                column: "test_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "answers",
                schema: "test");

            migrationBuilder.DropTable(
                name: "pollsters",
                schema: "test");

            migrationBuilder.DropTable(
                name: "questions",
                schema: "test");

            migrationBuilder.DropTable(
                name: "tests",
                schema: "test");
        }
    }
}
