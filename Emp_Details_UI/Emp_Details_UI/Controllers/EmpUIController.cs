using Emp_Details_UI.Models;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace Emp_Details_UI.Controllers
{
    public class EmpUIController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly string BaseUrlEmp;



        public EmpUIController(IConfiguration configuration)
        {
            _configuration = configuration;
            BaseUrlEmp = configuration["BaseUrlEmp"];
        }



        // this is a HttpGet method

        public async Task<IActionResult> Index()
        {
            try
            {
                string urlParameters = "GetEmp";
                using (var httpClient = new HttpClient())
                { 
                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrlEmp + urlParameters);

                    if (response.IsSuccessStatusCode)
                    {

                        string responseContent = await response.Content.ReadAsStringAsync();
                        List<EmpUIModel> lst = JsonConvert.DeserializeObject<List<EmpUIModel>>(responseContent);


                        ViewBag.EMP = lst;


                        return View();
                    }
                    else
                    {

                        return View("Error");
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


    }

        
        // this is a HttpPost method
        [HttpPost]
         public async Task<IActionResult> SaveEmp(EmpUIModel model)
        {
            try
            {


                string urlParameters = "SaveEmp";
                string data = JsonConvert.SerializeObject(model);
                using (var httpClient = new HttpClient())
                {
                    StringContent Content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(BaseUrlEmp + urlParameters, Content);



                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        int r = JsonConvert.DeserializeObject<int>(responseData);

                        if (r > 0)
                        {
                            TempData["MSG"] = "success";

                        }
                        else if (r == -1)
                        {
                            TempData["MSG"] = "exist";

                        }
                        else
                        {
                            TempData["MSG"] = "Fail";
                        }



                        return Redirect("/EmpUI/Index");



                    }
                    else
                    {
                        return Json("Error");

                    }

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        // this is a HttpGet method
        [HttpPost]
        public async Task<IActionResult> DeleteEmp(int id)
        {
            try
            {


                string urlParameters = "DeleteEmp";

                using (var httpClient = new HttpClient())
                {

                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrlEmp + urlParameters + "/" + id);



                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        int r = JsonConvert.DeserializeObject<int>(responseData);
                        return Json(r);

                    }
                    else
                    {
                        return Json("Error");

                    }

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }





        
        [HttpGet]
         public async Task<JsonResult> EditEmp(int id)
        {

            try
            {


                using (var httpClient = new HttpClient())
                {

                    string urlParameters = "EditEmp";
                    HttpResponseMessage response = await httpClient.GetAsync(BaseUrlEmp + urlParameters + "/" + id);



                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();

                        EmpUIModel lst = JsonConvert.DeserializeObject<EmpUIModel>(data);


                        var info = lst;
                        return Json(info);

                    }
                    else
                    {

                        return Json("Error");
                    }

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }






    }
}
