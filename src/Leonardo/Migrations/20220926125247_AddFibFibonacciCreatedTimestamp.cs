using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Leonardo.Migrations
{
    public partial class AddFibFibonacciCreatedTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FibLastExecutionTimestamp",
                schema: "sch_fib",
                table: "T_Fibonacci",
                newName: "FIB_FibLastExecutionTimestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FIB_FibLastExecutionTimestamp",
                schema: "sch_fib",
                table: "T_Fibonacci",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "('0001-01-01T00:00:00.0000000')",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FIB_FibLastExecutionTimestamp",
                schema: "sch_fib",
                table: "T_Fibonacci",
                newName: "FibLastExecutionTimestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FibLastExecutionTimestamp",
                schema: "sch_fib",
                table: "T_Fibonacci",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "('0001-01-01T00:00:00.0000000')");
        }
    }
}
