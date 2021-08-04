﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Conversion.Infrastructure.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Сurrencies",
                columns: table => new
                {
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false),
                    Ccy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CcyNm_RU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CcyNm_UZ = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CcyNm_UZC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CcyNm_EN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nominal = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Diff = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Сurrencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    ExchangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IncomeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutcomeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SourceCurrencyCurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TargetCurrencyCurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.ExchangeId);
                    table.ForeignKey(
                        name: "FK_Exchanges_Сurrencies_SourceCurrencyCurrencyId",
                        column: x => x.SourceCurrencyCurrencyId,
                        principalTable: "Сurrencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exchanges_Сurrencies_TargetCurrencyCurrencyId",
                        column: x => x.TargetCurrencyCurrencyId,
                        principalTable: "Сurrencies",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_SourceCurrencyCurrencyId",
                table: "Exchanges",
                column: "SourceCurrencyCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_TargetCurrencyCurrencyId",
                table: "Exchanges",
                column: "TargetCurrencyCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "Сurrencies");
        }
    }
}
