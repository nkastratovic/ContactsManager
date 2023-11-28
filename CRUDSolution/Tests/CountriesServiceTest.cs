using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        public CountriesServiceTest(ICountriesService countriesService)
        {
            _countriesService = new CountriesService();
        }
        //When CountryAddRequest is null, it should throw ArgumentNullException

        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arange
            CountryAddRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }
        //When the CountryName is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_CountryNameIsNull()
        {
            //Arange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = null};

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }
        //When the CountryName is null, it duplicated ArgumentNullException
        [Fact]
        public void AddCountry_DuplicatedCountryName()
        {
            //Arange
            CountryAddRequest? request1 = new CountryAddRequest()
            {
                CountryName = "USA"
            };
            CountryAddRequest? request2 = new CountryAddRequest()
            {
                CountryName = "USA"
            };

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }
        //When you suppley proper country name, it should insert (add) the country to the existing list os countries
        [Fact]
        public void AddCountry_ProperCountryDetals()
        {
            //Arange
            CountryAddRequest? request = new CountryAddRequest()
            {
                CountryName = "Japan"};
            
            //Act
            CountryResponse response = _countriesService.AddCountry(request);

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
        }
    }
}
