using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACarSample.Migrations
{
    public partial class MemberDataRawUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE MemberRoles SET Name = LOWER(Name)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
