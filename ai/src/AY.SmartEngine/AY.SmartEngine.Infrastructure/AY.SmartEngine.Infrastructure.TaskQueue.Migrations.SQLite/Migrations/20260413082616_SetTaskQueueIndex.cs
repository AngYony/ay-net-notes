using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AY.SmartEngine.Infrastructure.TaskQueue.Migrations.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class SetTaskQueueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Jobs",
                newName: "JobStatus");

            migrationBuilder.CreateTable(
                name: "JobHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobId = table.Column<Guid>(type: "TEXT", nullable: false),
                    JobStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHistories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "idx_jobs_parent",
                table: "Jobs",
                column: "ParentJobId");

            migrationBuilder.CreateIndex(
                name: "idx_jobs_queue_status",
                table: "Jobs",
                columns: new[] { "QueueName", "JobStatus" });

            migrationBuilder.CreateIndex(
                name: "idx_jobs_scheduled",
                table: "Jobs",
                column: "ScheduledAt");

            migrationBuilder.CreateIndex(
                name: "idx_jobqueues_name",
                table: "JobQueues",
                column: "QueueName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_Jobhistory_jobid",
                table: "JobHistories",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Jobs_ParentJobId",
                table: "Jobs",
                column: "ParentJobId",
                principalTable: "Jobs",
                principalColumn: "JobId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Jobs_ParentJobId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobHistories");

            migrationBuilder.DropIndex(
                name: "idx_jobs_parent",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "idx_jobs_queue_status",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "idx_jobs_scheduled",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "idx_jobqueues_name",
                table: "JobQueues");

            migrationBuilder.RenameColumn(
                name: "JobStatus",
                table: "Jobs",
                newName: "Status");
        }
    }
}
