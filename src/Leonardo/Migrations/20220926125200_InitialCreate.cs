using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leonardo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sch_fib");

            migrationBuilder.CreateTable(
                name: "T_Fibonacci",
                schema: "sch_fib",
                columns: table => new
                {
                    FIB_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    FIB_Input = table.Column<int>(type: "int", nullable: false),
                    FIB_Output = table.Column<long>(type: "bigint", nullable: false),
                    FIB_CreatedTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "('0001-01-01T00:00:00.0000000')"),
                    FibLastExecutionTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fibonacci", x => x.FIB_Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_Fibonacci",
                schema: "sch_fib");
        }
    }
}
