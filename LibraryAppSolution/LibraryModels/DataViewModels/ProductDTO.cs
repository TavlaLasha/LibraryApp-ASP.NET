using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryModels.DataViewModels
{
    public class ProductDTO
    {
        [Display(Name = "დასახელება")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "დასახელების მითითება სავალდებულოა"), MaxLength(250, ErrorMessage = "დასახელება არ უნდა აღემატებოდეს 250 სიმბოლოს"), MinLength(2, ErrorMessage = "დასახელება უნდა შეადგენდეს მინიმუმ 2 სიმბოლოს")]
        public string Name { get; set; }

        [Display(Name = "ანოტაცია")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "ანოტაციის მითითება სავალდებულოა"), MaxLength(500, ErrorMessage = "ანოტაცია არ უნდა აღემატებოდეს 500 სიმბოლოს"), MinLength(100, ErrorMessage = "ანოტაცია უნდა შეადგენდეს მინიმუმ 100 სიმბოლოს")]
        public string Annotation { get; set; }

        [Display(Name = "პროდუქტის ტიპი")]
        [DataType(DataType.Text)]
        public string Type { get; set; }

        [Display(Name = "ISBN")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "ISBN-ის მითითება სავალდებულოა"), MaxLength(13, ErrorMessage = "ISBN უნდა შედგებოდეს 13 სიმბოლოსგან"), MinLength(13, ErrorMessage = "ISBN უნდა შედგებოდეს 13 სიმბოლოსგან")]
        public string ISBN { get; set; }

        [Display(Name = "გამოშვების თარიღი")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "გამოშვების თარიღის მითითება სავალდებულოა")]
        //[Range(typeof(DateTime), "1/1/2002 00:00:00", "1/1/3020 00:00:00", ErrorMessage = "პროდუქტის წლოვანება უნდა იყოს მინიმუმ 18 წელი")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime Release_Date { get; set; }

        [Display(Name = "გამოშვება")]
        [DataType(DataType.Text)]
        public string Production { get; set; }

        [Display(Name = "გვერდების რაოდენობა")]
        [DataType(DataType.Text)]
        public int? PageCount { get; set; }

        [Display(Name = "მისამართი")]
        [DataType(DataType.Text)]
        public string Address { get; set; }

        [Display(Name = "არქივირებული")]
        public bool IsArchived { get; set; }
    }
    public class TypeDTO
    {
        public int Type_Id { get; set; }

        [Display(Name = "პროდუქტის ტიპი")]
        [DataType(DataType.Text)]
        public string Type_Name { get; set; }
    }
    public class ProductionDTO
    {
        public int Production_Id { get; set; }

        [Display(Name = "გამოშვება")]
        [DataType(DataType.Text)]
        public string Production_Name { get; set; }
    }
}
