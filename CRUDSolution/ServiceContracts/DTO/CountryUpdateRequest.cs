using Entities;
using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO
{
 /// <summary>
 /// Represents the DTO class that contains the person details to update
 /// </summary>
 public class CountryUpdateRequest
  {
    [Required(ErrorMessage = "Country ID can't be blank")]
    public Guid CountryID { get; set; }

    [Required(ErrorMessage = "Country Name can't be blank")]
    public string? CountryName { get; set; }

    /// <summary>
    /// Converts the current object of CountryAddRequest into a new object of Country type
    /// </summary>
    /// <returns>Returns Country object</returns>
    public Country ToCountry()
    {
      return new Country() { CountryID = CountryID, CountryName = CountryName };
    }
  }
}
