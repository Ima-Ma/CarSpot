using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DriveXShowcaseAdmin.Migrations
{
    /// <inheritdoc />
    public partial class m3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DealerAccount",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(name: "first_name", type: "nvarchar(max)", nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dateofbirth = table.Column<string>(name: "date_of_birth", type: "nvarchar(max)", nullable: false),
                    selectedbrand = table.Column<string>(name: "selected_brand", type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    registrationdate = table.Column<string>(name: "registration_date", type: "nvarchar(max)", nullable: false),
                    dealerIdCode = table.Column<int>(name: "dealer_IdCode", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealerAccount", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminLogin");

            migrationBuilder.DropTable(
                name: "DealerAccount");
        }
    }
}
