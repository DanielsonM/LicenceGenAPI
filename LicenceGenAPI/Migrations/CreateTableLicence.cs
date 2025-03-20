using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LicenceGenAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateTableLicence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Licence",
                columns: table => new
                {
                    int_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    str_user_name = table.Column<string>(type: "text", nullable: false),
                    str_status = table.Column<string>(type: "text", nullable: false),
                    str_licence_key = table.Column<string>(type: "text", nullable: false),
                    str_date_request = table.Column<string>(type: "text", nullable: false),
                    str_date_expiration = table.Column<string>(type: "text", nullable: false),
                    str_observation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licence", x => x.int_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Licence");
        }
    }
}
