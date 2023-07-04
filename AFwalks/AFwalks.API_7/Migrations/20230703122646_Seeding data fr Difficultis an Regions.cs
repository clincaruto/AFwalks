using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AFwalks.API_7.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdatafrDifficultisanRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("20b3c9c4-366c-4572-bba0-e0702125fefd"), "Hard" },
                    { new Guid("21663eb5-77eb-485d-8f42-63ae94c43cb9"), "Medium" },
                    { new Guid("6cbc474b-546a-462f-80ae-62b15a08210c"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("3c41788e-abfd-44e1-9750-32f27f64fd3a"), "STL", "Southland", null },
                    { new Guid("5f354405-b144-47ba-abaf-f160b2861de6"), "CND", "Sagemode", "https://wallpapercave.com/w/d8zqgVd" },
                    { new Guid("7a19d5d5-cd6b-4cf5-be96-d193043b4e5a"), "KEN", "Naruto", null },
                    { new Guid("d267bd9e-8cda-4cac-8188-e3872c7cf877"), "SGA", "Boss Nation", null },
                    { new Guid("e4c5d0b8-2a2f-48de-9623-9d808a8bd9cf"), "NGA", "Chuks Region", "https://wallpapercave.com/w/d8zqgVd" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("20b3c9c4-366c-4572-bba0-e0702125fefd"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("21663eb5-77eb-485d-8f42-63ae94c43cb9"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("6cbc474b-546a-462f-80ae-62b15a08210c"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("3c41788e-abfd-44e1-9750-32f27f64fd3a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("5f354405-b144-47ba-abaf-f160b2861de6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7a19d5d5-cd6b-4cf5-be96-d193043b4e5a"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d267bd9e-8cda-4cac-8188-e3872c7cf877"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e4c5d0b8-2a2f-48de-9623-9d808a8bd9cf"));
        }
    }
}
