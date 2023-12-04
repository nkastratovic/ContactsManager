using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
  public partial class GetPersons_StoredProcedure : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      string sp_GetAllPersons = @"CREATE PROCEDURE [DBO].[GetAllPersons]
AS BEGIN
SELECT PersonID, PersonName, Email, DateOfBirth, Gender, CountryID, Address, ReceiveNewsLetters FROM [dbo].[Persons]
END";
      migrationBuilder.Sql(sp_GetAllPersons);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      string sp_GetAllPersons = @"DROP PROCEDURE [DBO].[GetAllPersons]";
      migrationBuilder.Sql(sp_GetAllPersons);
    }
  }
}
