using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUDExample.Controllers
{
  [Route("[controller]")]
  public class CountriesController : Controller
  {
    //private fields
    private readonly ICountriesService _countriesService;

    //constructor
    public CountriesController(ICountriesService countriesService)
    {
      _countriesService = countriesService;
    }

    //Url: countries/index
    [Route("[action]")]
    public IActionResult Index(/*string searchBy, string? searchString, string sortBy = nameof(PersonResponse.PersonName), SortOrderOptions sortOrder = SortOrderOptions.ASC*/)
    {
      ////Search
      //ViewBag.SearchFields = new Dictionary<string, string>()
      //{
      //  { nameof(CountryResponse.CountryName), "Country Name" }
      //};
      List<CountryResponse> countries = _countriesService.GetAllCountries();
      //ViewBag.CurrentSearchBy = searchBy;
      //ViewBag.CurrentSearchString = searchString;

      //Sort
      //List<PersonResponse> sortedCountries =  _personsService.GetSortedCountries(persons, sortBy, sortOrder);
      //ViewBag.CurrentSortBy = sortBy;
      //ViewBag.CurrentSortOrder = sortOrder.ToString();

      return View(countries); //Views/Countries/Index.cshtml
    }


  //Executes when the user clicks on "Create Person" hyperlink (while opening the create view)
  //Url: persons/create
  [Route("[action]")]
  [HttpGet]
  public IActionResult Create()
  {
   return View();
  }

  [HttpPost]
  //Url: country/create
  [Route("[action]")]
  public IActionResult Create(CountryAddRequest countryAddRequest)
  {
   if (!ModelState.IsValid)
   {
    List<CountryResponse> countries = _countriesService.GetAllCountries();
    ViewBag.Countries = countries.Select(temp =>
    new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

    ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
    return View();
   }

   //call the service method
   CountryResponse countryResponse = _countriesService.AddCountry(countryAddRequest);

   //navigate to Index() action method (it makes another get request to "persons/index"
   return RedirectToAction("Index", "Countries");
  }

  [HttpGet]
  [Route("[action]/{countryID}")] //Eg: /countries/edit/1
  public IActionResult Edit(Guid countryID)
  {
   CountryResponse? countryResponse = _countriesService.GetCountryByCountryID(countryID);
   if (countryResponse == null)
   {
    return RedirectToAction("Index");
   }

   CountryUpdateRequest countryUpdateRequest = countryResponse.ToCountryUpdateRequest();

   List<CountryResponse> countries = _countriesService.GetAllCountries();
   ViewBag.Countries = countries.Select(temp =>
   new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

   return View(countryUpdateRequest);
  }


  //[HttpPost]
  //[Route("[action]/{personID}")]
  //public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
  //{
  //  PersonResponse? personResponse = _personsService.GetPersonByPersonID(personUpdateRequest.PersonID);

  //  if (personResponse == null)
  //  {
  //    return RedirectToAction("Index");
  //  }

  //  if (ModelState.IsValid)
  //  {
  //    PersonResponse updatedPerson = _personsService.UpdatePerson(personUpdateRequest);
  //    return RedirectToAction("Index");
  //  }
  //  else
  //  {
  //    List<CountryResponse> countries = _countriesService.GetAllCountries();
  //    ViewBag.Countries = countries.Select(temp =>
  //    new SelectListItem() { Text = temp.CountryName, Value = temp.CountryID.ToString() });

  //    ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
  //    return View(personResponse.ToPersonUpdateRequest());
  //  }
  //}


  //[HttpGet]
  //[Route("[action]/{personID}")]
  //public IActionResult Delete(Guid? personID)
  //{
  //  PersonResponse? personResponse = _personsService.GetPersonByPersonID(personID);
  //  if (personResponse == null)
  //    return RedirectToAction("Index");

  //  return View(personResponse);
  //}

  //[HttpPost]
  //[Route("[action]/{personID}")]
  //public IActionResult Delete(PersonUpdateRequest personUpdateResult)
  //{
  //  PersonResponse? personResponse = _personsService.GetPersonByPersonID(personUpdateResult.PersonID);
  //  if (personResponse == null)
  //    return RedirectToAction("Index");

  //  _personsService.DeletePerson(personUpdateResult.PersonID);
  //  return RedirectToAction("Index");
  //}
 }
}
