using LibraryModels.DataViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LibraryClientApp.Controllers
{
    public class LibraryController : Controller
    {
        static HttpClient client = new HttpClient();
        string BaseURL = ConfigurationManager.AppSettings["libraryService"];

        [Authorize(Roles = "Admin, Manager, Operator")]
        public ActionResult Index()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetAllAuthors").Result;
                List<AuthorDTO> ct = new List<AuthorDTO>();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<List<AuthorDTO>>(response.Content.ReadAsStringAsync().Result);
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }
        [Authorize(Roles = "Admin, Manager, Operator")]
        public ActionResult AuthorDetails(string pn)
        {
            try
            {
                if (pn.Equals(""))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetAuthor/{pn}").Result;
                AuthorDTO ct = new AuthorDTO();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<AuthorDTO>(response.Content.ReadAsStringAsync().Result);
                }

                
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        [Authorize(Roles = "Admin, Manager, Operator")]
        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult AuthorProducts(string author_pn)
        {
            HttpResponseMessage products_res = client.GetAsync($"{BaseURL}/GetAuthorProducts/{author_pn}").Result;
            List<ProductDTO> co = new List<ProductDTO>();
            if (products_res.IsSuccessStatusCode)
            {
                co = JsonConvert.DeserializeObject< List<ProductDTO>>(products_res.Content.ReadAsStringAsync().Result);
            }
            ViewBag.Author_PN = author_pn;
            return PartialView("_AuthorProductsPartial", co);
        }

        [Authorize(Roles = "Admin, Manager, Operator")]
        public ActionResult ChangeArchiveStatus(string isbn)
        {
            ViewBag.ErrorMessage = "";
            try
            {
                if (isbn.Equals(""))
                    new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                HttpResponseMessage response = client.GetAsync($"{BaseURL}/ChangeArchiveStatus/{isbn}").Result;
                if (!response.IsSuccessStatusCode)
                {
                    new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return Redirect(Request.UrlReferrer.ToString());
                }
                return Redirect(Request.UrlReferrer.ToString());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "ცვლილების დროს მოხდა შეცდომა. ბოდიშს გიხდით";
                return View();
            }
        }
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult AddProduct()
        {
            ViewBag.ErrorMessage = "";
            try
            {
                HttpResponseMessage type_response = client.GetAsync($"{BaseURL}/GetAllTypes").Result;
                List<TypeDTO> tp = new List<TypeDTO>();
                if (type_response.IsSuccessStatusCode)
                {
                    tp = JsonConvert.DeserializeObject<List<TypeDTO>>(type_response.Content.ReadAsStringAsync().Result);
                }

                HttpResponseMessage prod_response = client.GetAsync($"{BaseURL}/GetAllProductions").Result;
                List<ProductionDTO> po = new List<ProductionDTO>();
                if (prod_response.IsSuccessStatusCode)
                {
                    po = JsonConvert.DeserializeObject<List<ProductionDTO>>(prod_response.Content.ReadAsStringAsync().Result);
                }
                ViewBag.TypeList = new SelectList(tp, "Type_Name", "Type_Name");
                ViewBag.ProductionList = new SelectList(po, "Production_Name", "Production_Name");

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "მოხდა შეცდომა. ბოდიშს გიხდით";
                return View();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddProduct(HttpPostedFileBase file, ProductDTO product, string author_pn)
        {
            ViewBag.ErrorMessage = "";
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("მონაცემები არავალიდურია!");

                string output = JsonConvert.SerializeObject(product);
                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync($"{BaseURL}/AddProduct/{author_pn}", stringContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "დამატების დროს მოხდა შეცდომა. ბოდიშს გიხდით";
                return View();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult AddAuthor()
        {
            ViewBag.ErrorMessage = "";
            try
            {
                //throw new Exception("");
                HttpResponseMessage co_response = client.GetAsync($"{BaseURL}/GetAllCountries").Result;
                List<CountryDTO> co = new List<CountryDTO>();
                if (co_response.IsSuccessStatusCode)
                {
                    co = JsonConvert.DeserializeObject<List<CountryDTO>>(co_response.Content.ReadAsStringAsync().Result);
                }

                HttpResponseMessage ci_response = client.GetAsync($"{BaseURL}/GetAllCities").Result;
                List<CityDTO> ci = new List<CityDTO>();
                if (ci_response.IsSuccessStatusCode)
                {
                    ci = JsonConvert.DeserializeObject<List<CityDTO>>(ci_response.Content.ReadAsStringAsync().Result);
                }

                List<string> GenderList = new List<string>();
                GenderList.Add("Male");
                GenderList.Add("Female");
                ViewBag.GenderList = new SelectList(GenderList);
                ViewBag.CountryList = new SelectList(co, "Country_Name", "Country_Name");
                ViewBag.CityList = new SelectList(ci, "City_Name", "City_Name");
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "მოხდა შეცდომა. ბოდიშს გიხდით";
                return View();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddAuthor(HttpPostedFileBase file, AuthorDTO author)
        {
            ViewBag.ErrorMessage = "";
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("მონაცემები არავალიდურია!");

                string output = JsonConvert.SerializeObject(author);
                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync($"{BaseURL}/AddAuthor", stringContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "დამატების დროს მოხდა შეცდომა. ბოდიშს გიხდით";
                return View();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult EditAuthor(string pn)
        {
            if (pn.Equals(""))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetAuthor/{pn}").Result;

            AuthorDTO ct = new AuthorDTO();

            HttpResponseMessage co_response = client.GetAsync($"{BaseURL}/GetAllCountries").Result;
            List<CountryDTO> co = new List<CountryDTO>();
            if (co_response.IsSuccessStatusCode)
            {
                co = JsonConvert.DeserializeObject<List<CountryDTO>>(co_response.Content.ReadAsStringAsync().Result);
            }

            HttpResponseMessage ci_response = client.GetAsync($"{BaseURL}/GetAllCities").Result;
            List<CityDTO> ci = new List<CityDTO>();
            if (ci_response.IsSuccessStatusCode)
            {
                ci = JsonConvert.DeserializeObject<List<CityDTO>>(ci_response.Content.ReadAsStringAsync().Result);
            }

            List<string> GenderList = new List<string>();
            GenderList.Add("Male");
            GenderList.Add("Female");
            ViewBag.GenderList = new SelectList(GenderList);
            ViewBag.CountryList = new SelectList(co, "Country_Name", "Country_Name");
            ViewBag.CityList = new SelectList(ci, "City_Name", "City_Name");

            if (response.IsSuccessStatusCode)
            {
                ct = JsonConvert.DeserializeObject<AuthorDTO>(response.Content.ReadAsStringAsync().Result);
                return View(ct);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditAuthor(string pn, HttpPostedFileBase file, [Bind(Include = "FirstName, LastName, Gender, BirthDate, Country, City, Phone, Email")] AuthorDTO author)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("მონაცემები არავალიდურია!");

                string output = JsonConvert.SerializeObject(author);
                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync($"{BaseURL}/EditAuthor/{pn}", stringContent).Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        public ActionResult DeleteAuthor(string pn)
        {
            try
            {
                if (pn.Equals(""))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                HttpResponseMessage response = client.GetAsync($"{BaseURL}/GetAuthor/{pn}").Result;
                AuthorDTO ct = new AuthorDTO();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<AuthorDTO>(response.Content.ReadAsStringAsync().Result);
                }
                return View(ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [ValidateAntiForgeryToken]
        [HttpDelete]
        public ActionResult DeleteAuthor(string pn, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"{BaseURL}/DeleteAuthor/{pn}").Result;

                if (!response.IsSuccessStatusCode)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}