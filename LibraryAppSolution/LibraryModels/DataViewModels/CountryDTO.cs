using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels.DataViewModels
{
    public class CountryDTO
    {
        public int Country_Id { get; set; }

        //[Display(Name = "ქალაქი")]
        //[DataType(DataType.Text)]
        //[Required(ErrorMessage = "ქალაქის მითითება სავალდებულოა")]
        public string Country_Name { get; set; }
    }
}
