using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eMTE.Temperature.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HealthMeasureConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    IsTemperatureMandate = table.Column<bool>(nullable: false),
                    TemperatureUnit = table.Column<string>(nullable: false),
                    IsCoughMandate = table.Column<bool>(nullable: false),
                    IsSneezingMandate = table.Column<bool>(nullable: false),
                    IsRunnyNoseMandate = table.Column<bool>(nullable: false),
                    IsShortnessBreathMandate = table.Column<bool>(nullable: false),
                    IsOxygenSaturationMandate = table.Column<bool>(nullable: false),
                    IsHeatRateMandate = table.Column<bool>(nullable: false),
                    IsImageWithPPEMandate = table.Column<bool>(nullable: false),
                    MeasureCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMeasureConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthMeasureConfigurations_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    IsOrganizationAdmin = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Hash = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    DisplayPicture = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    RoleDescription = table.Column<string>(nullable: true),
                    AlreadyInfected = table.Column<bool>(nullable: false),
                    InfectedFrom = table.Column<DateTime>(nullable: true),
                    InfectedTo = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DayMeasures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NotedDate = table.Column<DateTime>(nullable: false),
                    Intime = table.Column<string>(nullable: true),
                    OutTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayMeasures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayMeasures_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DayMeasures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedById = table.Column<Guid>(nullable: false),
                    ModifiedById = table.Column<Guid>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ModifiedTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    TeamManagerId = table.Column<Guid>(nullable: false),
                    TeamDescription = table.Column<string>(nullable: true),
                    DisplayPicture = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Users_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Teams_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Teams_Users_TeamManagerId",
                        column: x => x.TeamManagerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HealthMeasures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    TemperatureUnit = table.Column<string>(nullable: false),
                    Cough = table.Column<bool>(nullable: false),
                    Sneezing = table.Column<bool>(nullable: false),
                    RunnyNose = table.Column<bool>(nullable: false),
                    ShortnessBreath = table.Column<bool>(nullable: false),
                    OxygenSaturation = table.Column<string>(nullable: true),
                    HeatRate = table.Column<string>(nullable: true),
                    ImageWithPPE = table.Column<string>(nullable: true),
                    UpdateDateTime = table.Column<DateTime>(nullable: false),
                    DayMeasureId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthMeasures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HealthMeasures_DayMeasures_DayMeasureId",
                        column: x => x.DayMeasureId,
                        principalTable: "DayMeasures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeamUserMaps",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TeamId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamUserMaps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamUserMaps_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamUserMaps_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DayMeasures_OrganizationId",
                table: "DayMeasures",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_DayMeasures_UserId_NotedDate",
                table: "DayMeasures",
                columns: new[] { "UserId", "NotedDate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthMeasureConfigurations_OrganizationId",
                table: "HealthMeasureConfigurations",
                column: "OrganizationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HealthMeasures_DayMeasureId",
                table: "HealthMeasures",
                column: "DayMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_Name",
                table: "Organizations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CreatedById",
                table: "Teams",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_ModifiedById",
                table: "Teams",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                table: "Teams",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_OrganizationId",
                table: "Teams",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TeamManagerId",
                table: "Teams",
                column: "TeamManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUserMaps_UserId",
                table: "TeamUserMaps",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamUserMaps_TeamId_UserId",
                table: "TeamUserMaps",
                columns: new[] { "TeamId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId",
                table: "Users",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HealthMeasureConfigurations");

            migrationBuilder.DropTable(
                name: "HealthMeasures");

            migrationBuilder.DropTable(
                name: "TeamUserMaps");

            migrationBuilder.DropTable(
                name: "DayMeasures");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
