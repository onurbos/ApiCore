using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiCore.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    email = table.Column<string>(maxLength: 200, nullable: false),
                    first_name = table.Column<string>(maxLength: 200, nullable: false),
                    last_name = table.Column<string>(maxLength: 200, nullable: false),
                    full_name = table.Column<string>(maxLength: 400, nullable: false),
                    is_active = table.Column<bool>(nullable: false),
                    culture = table.Column<string>(maxLength: 10, nullable: true),
                    currency = table.Column<string>(maxLength: 3, nullable: true),
                    is_subscriber = table.Column<bool>(nullable: false),
                    created_by = table.Column<string>(nullable: true),
                    updated_by = table.Column<string>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    picture = table.Column<string>(nullable: true),
                    profile_id = table.Column<int>(nullable: true),
                    role_id = table.Column<int>(nullable: true),
                    phone = table.Column<string>(maxLength: 50, nullable: true),
                    password_hash = table.Column<byte[]>(nullable: true),
                    password_salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "profiles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_by = table.Column<int>(nullable: false),
                    updated_by = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    has_admin_rights = table.Column<bool>(nullable: false),
                    send_email = table.Column<bool>(nullable: false),
                    send_sms = table.Column<bool>(nullable: false),
                    export_data = table.Column<bool>(nullable: false),
                    import_data = table.Column<bool>(nullable: false),
                    word_pdf_download = table.Column<bool>(nullable: false),
                    parent_id = table.Column<int>(nullable: false),
                    order = table.Column<int>(nullable: false),
                    system_code = table.Column<string>(nullable: true),
                    change_email = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profiles", x => x.id);
                    table.ForeignKey(
                        name: "FK_profiles_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_profiles_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_by = table.Column<int>(nullable: false),
                    updated_by = table.Column<int>(nullable: true),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: true),
                    deleted = table.Column<bool>(nullable: false),
                    label = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: true),
                    master = table.Column<bool>(nullable: false),
                    owners = table.Column<string>(nullable: true),
                    share_data = table.Column<bool>(nullable: false),
                    reports_to_id = table.Column<int>(nullable: true),
                    system_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                    table.ForeignKey(
                        name: "FK_roles_users_created_by",
                        column: x => x.created_by,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roles_roles_reports_to_id",
                        column: x => x.reports_to_id,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_roles_users_updated_by",
                        column: x => x.updated_by,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_profiles_created_by",
                schema: "public",
                table: "profiles",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_profiles_updated_by",
                schema: "public",
                table: "profiles",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_roles_created_by",
                schema: "public",
                table: "roles",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_roles_reports_to_id",
                schema: "public",
                table: "roles",
                column: "reports_to_id");

            migrationBuilder.CreateIndex(
                name: "IX_roles_updated_by",
                schema: "public",
                table: "roles",
                column: "updated_by");

            migrationBuilder.CreateIndex(
                name: "IX_users_created_at",
                schema: "public",
                table: "users",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "IX_users_deleted",
                schema: "public",
                table: "users",
                column: "deleted");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                schema: "public",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_full_name",
                schema: "public",
                table: "users",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "IX_users_profile_id",
                schema: "public",
                table: "users",
                column: "profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_role_id",
                schema: "public",
                table: "users",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_updated_at",
                schema: "public",
                table: "users",
                column: "updated_at");

            migrationBuilder.AddForeignKey(
                name: "FK_users_profiles_profile_id",
                schema: "public",
                table: "users",
                column: "profile_id",
                principalSchema: "public",
                principalTable: "profiles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_role_id",
                schema: "public",
                table: "users",
                column: "role_id",
                principalSchema: "public",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_profiles_users_created_by",
                schema: "public",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_profiles_users_updated_by",
                schema: "public",
                table: "profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_users_created_by",
                schema: "public",
                table: "roles");

            migrationBuilder.DropForeignKey(
                name: "FK_roles_users_updated_by",
                schema: "public",
                table: "roles");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "profiles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "public");
        }
    }
}
