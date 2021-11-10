using LibraryBLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryModels.DataViewModels;
using LibraryDAL.EF;

namespace LibraryBLL.Services
{
    public class LocationManagement : ILocationManagement
    {
        LibraryDBContext db = new LibraryDBContext();

        public IEnumerable<CityDTO> GetAllCities()
        {
            return db.Cities.Select(i => new CityDTO
            {
                City_Id = i.City_Id,
                City_Name = i.City_Name
            });
        }

        public IEnumerable<CountryDTO> GetAllCountries()
        {
            return db.Countries.Select(i => new CountryDTO
            {
                Country_Id = i.Country_Id,
                Country_Name = i.Country_Name
            });
        }
        
    }
}
