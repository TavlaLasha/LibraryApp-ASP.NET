using LibraryModels.DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBLL.Contracts
{
    public interface ILocationManagement
    {
        IEnumerable<CountryDTO> GetAllCountries();
        IEnumerable<CityDTO> GetAllCities();
    }
}
