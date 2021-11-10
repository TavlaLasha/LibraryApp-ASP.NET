using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels.DataViewModels
{
    public class AuthorDTO
    {
        [Display(Name = "სახელი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "სახელის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს"), MinLength(2, ErrorMessage = "სახელი უნდა შეადგენდეს მინიმუმ 2 სიმბოლოს")]
        public string FirstName { get; set; }

        [Display(Name = "გვარი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "გვარის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "გვარი არ უნდა აღემატებოდეს 50 სიმბოლოს"), MinLength(2, ErrorMessage = "გვარი უნდა შეადგენდეს მინიმუმ 2 სიმბოლოს")]
        public string LastName { get; set; }

        [Display(Name = "სქესი")]
        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [Display(Name = "პირადი ნომერი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "პირადი ნომრის მითითება სავალდებულოა"), MinLength(11, ErrorMessage = "პირადი ნომერი უნდა შედგებოდეს 11 სიმბოლოსგან"), MaxLength(11, ErrorMessage = "პირადი ნომერი უნდა შედგებოდეს 11 სიმბოლოსგან")]
        public string PN { get; set; }

        [Display(Name = "დაბადების თარიღი")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "დაბადების თარიღის მითითება სავალდებულოა")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "ქვეყანა")]
        [DataType(DataType.Text)]
        public string Country { get; set; }

        [Display(Name = "ქალაქი")]
        [DataType(DataType.Text)]
        public string City { get; set; }

        [Display(Name = "ტელეფონის ნომერი")]
        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "ტელეფონის ნომერი არ უნდა აღემატებოდეს 50 სიმბოლოს"), MinLength(4, ErrorMessage = "ტელეფონის ნომერი უნდა შეადგენდეს მინიმუმ 4 სიმბოლოს")]
        public string Phone { get; set; }

        [Display(Name = "ელ-ფოსტა")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "შეყვანილი ელ-ფოსტია არავალიდურია")]
        public string Email { get; set; }
    }
}
