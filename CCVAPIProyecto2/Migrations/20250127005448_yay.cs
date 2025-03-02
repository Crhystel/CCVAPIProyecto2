﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCVAPIProyecto2.Migrations
{
    /// <inheritdoc />
    public partial class yay : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaAsignacion",
                table: "ActividadEstudiantes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaAsignacion",
                table: "ActividadEstudiantes");
        }
    }
}
