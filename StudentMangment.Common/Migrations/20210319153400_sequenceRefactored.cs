using Microsoft.EntityFrameworkCore.Migrations;

namespace StudentMangment.Common.Migrations
{
    public partial class sequenceRefactored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence(
                name: "StdSeq",
                startValue: 100L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RestartSequence(
                name: "StdSeq",
                startValue: 1L);
        }
    }
}
