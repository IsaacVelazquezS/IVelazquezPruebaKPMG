using Azure;
using BL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Text;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly BL.City _city;
        private readonly BL.Education _education;
        private readonly BL.EverBenched _everBenched;
        private readonly BL.Gender _gender;
        private readonly BL.Empleado _empleado;


        public EmpleadoController(IConfiguration configuration, BL.City city, BL.Education education, BL.EverBenched everBenched, BL.Gender gender, Empleado empleado)
        {
            _configuration = configuration;
            _city = city;
            _education = education;
            _everBenched = everBenched;
            _gender = gender;
            _empleado = empleado;
        }

        [Authorize(Roles = "RRHH,Gerente")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empleados = new List<object>();
            empleado.City = new ML.City();
            empleado.Education = new ML.Education();
            empleado.EverBenched = new ML.EverBenched();
            empleado.Gender = new ML.Gender();

            ML.Result result = _city.GetAll();
            ML.Result resultEducation = _education.GetAll();
            ML.Result resultEverBenched = _everBenched.GetAll();
            ML.Result ResultGender = _gender.GetAll();

            if (result.Correct && result.Objects != null)
            {
                empleado.City.Cities = result.Objects;
            }
            if (resultEducation.Correct && resultEducation.Objects != null)
            {
                empleado.Education.Educations = resultEducation.Objects;
            }
            if (resultEverBenched.Correct && resultEverBenched.Objects != null)
            {
                empleado.EverBenched.EverBenches = resultEverBenched.Objects;
            }
            if (ResultGender.Correct && ResultGender.Objects != null)
            {
                empleado.Gender.Genders = ResultGender.Objects;
            }
            try
            {
                using (var client = new HttpClient())
                {
                    string baseUrl = _configuration["ApiSettings:BaseUrl"];
                    client.BaseAddress = new Uri(baseUrl);

                    var token = Request.Cookies["AuthToken"];

                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Bearer", token
                        );

                    var response = client.PostAsJsonAsync("GetAll", empleado);

                    response.Wait();

                    if (response.Result.IsSuccessStatusCode)
                    {
                        var jsonResponse = response.Result.Content.ReadAsStringAsync().Result;
                        ML.Result resultApi = JsonConvert.DeserializeObject<ML.Result>(jsonResponse);

                        if (resultApi.Objects != null)
                        {
                            foreach (var item in resultApi.Objects)
                            {
                                ML.Empleado empleadoObj = JsonConvert.DeserializeObject<ML.Empleado>(item.ToString());
                                empleado.Empleados.Add(empleadoObj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(empleado);

        }
        [Authorize(Roles = "RRHH,Gerente")]
        [HttpPost]
        public IActionResult GetAll(ML.Empleado empleado)
        {
            empleado.Empleados = new List<object>();
            empleado.City = new ML.City();
            empleado.Education = new ML.Education();
            empleado.EverBenched = new ML.EverBenched();
            empleado.Gender = new ML.Gender();

            ML.Result result = _city.GetAll();
            ML.Result resultEducation = _education.GetAll();
            ML.Result resultEverBenched = _everBenched.GetAll();
            ML.Result ResultGender = _gender.GetAll();

            if (result.Correct && result.Objects != null)
            {
                empleado.City.Cities = result.Objects;
            }
            if (resultEducation.Correct && resultEducation.Objects != null)
            {
                empleado.Education.Educations = resultEducation.Objects;
            }
            if (resultEverBenched.Correct && resultEverBenched.Objects != null)
            {
                empleado.EverBenched.EverBenches = resultEverBenched.Objects;
            }
            if (ResultGender.Correct && ResultGender.Objects != null)
            {
                empleado.Gender.Genders = ResultGender.Objects;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    string baseUrl = _configuration["ApiSettings:BaseUrl"];
                    client.BaseAddress = new Uri(baseUrl);
                    var token = Request.Cookies["AuthToken"];

                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Bearer", token
                        );
                    var response = client.PostAsJsonAsync("GetAll", empleado);

                    response.Wait();

                    if (response.Result.IsSuccessStatusCode)
                    {
                        var jsonResponse = response.Result.Content.ReadAsStringAsync().Result;
                        ML.Result resultApi = JsonConvert.DeserializeObject<ML.Result>(jsonResponse);

                        if (resultApi.Objects != null)
                        {
                            foreach (var item in resultApi.Objects)
                            {
                                ML.Empleado empleadoObj = JsonConvert.DeserializeObject<ML.Empleado>(item.ToString());
                                empleado.Empleados.Add(empleadoObj);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(empleado);
        }

        [Authorize(Roles = "RRHH")]
        [HttpGet]
        public IActionResult Delete(int IdEmpleado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string baseUrl = _configuration["ApiSettings:BaseUrl"];
                    client.BaseAddress = new Uri(baseUrl);
                    var token = Request.Cookies["AuthToken"];

                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Bearer", token
                        );
                    client.DeleteAsync($"Delete/{IdEmpleado}").Wait();

                    var task = client.DeleteAsync($"Delete/{IdEmpleado}");
                    task.Wait();

                    if (task.Result.IsSuccessStatusCode)
                    {
                        var jsonResponse = task.Result.Content.ReadAsStringAsync().Result;
                        ML.Result resultApi = JsonConvert.DeserializeObject<ML.Result>(jsonResponse);

                        if (resultApi.Correct)
                        {
                            return RedirectToAction("GetAll");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        [Authorize(Roles = "RRHH")]
        [HttpGet]
        public ActionResult Formulario(int? IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado
            {
                City = new ML.City(),
                Education = new ML.Education(),
                Gender = new ML.Gender(),
                EverBenched = new ML.EverBenched()
            };

            if (IdEmpleado.HasValue)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        string baseUrl = _configuration["ApiSettings:BaseUrl"];
                        client.BaseAddress = new Uri(baseUrl);
                        var token = Request.Cookies["AuthToken"];

                        client.DefaultRequestHeaders.Authorization =
                            new System.Net.Http.Headers.AuthenticationHeaderValue(
                                "Bearer", token
                            );
                        var response = client.GetAsync($"GetById/{IdEmpleado.Value}").Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = response.Content.ReadAsStringAsync().Result;
                            ML.Result resultApi = JsonConvert.DeserializeObject<ML.Result>(jsonResponse);



                            if (resultApi.Object != null)
                            {
                                empleado = JsonConvert.DeserializeObject<ML.Empleado>(resultApi.Object.ToString());

                              
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                }
            }

            ML.Result resultCity = _city.GetAll();
            ML.Result resultEducation = _education.GetAll();
            ML.Result resultGender = _gender.GetAll();
            ML.Result resultBenched = _everBenched.GetAll();
          

            if (resultCity.Correct)
                empleado.City.Cities = resultCity.Objects;

            if (resultEducation.Correct)
                empleado.Education.Educations = resultEducation.Objects;

            if (resultGender.Correct)
                empleado.Gender.Genders = resultGender.Objects;

            if (resultBenched.Correct)
                empleado.EverBenched.EverBenches = resultBenched.Objects;

            return View(empleado);
        }
        [Authorize(Roles = "RRHH")]
        [HttpPost]
        public ActionResult Formulario(ML.Empleado empleado)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string baseUrl = _configuration["ApiSettings:BaseUrl"];
                    client.BaseAddress = new Uri(baseUrl);
                    var token = Request.Cookies["AuthToken"];

                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue(
                            "Bearer", token
                        );
                    var json = JsonConvert.SerializeObject(empleado);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    if (empleado.IdEmpleado == 0)
                    {
                        var task = client.PostAsJsonAsync("Add", empleado);
                        task.Wait();

                        if (task.Result.IsSuccessStatusCode)
                        {
                            var jsonResponse = task.Result.Content.ReadAsStringAsync().Result;
                            ML.Result resultApi = JsonConvert.DeserializeObject<ML.Result>(jsonResponse);

                            if (resultApi.Correct)
                            {
                                return RedirectToAction("GetAll");
                            }
                            else
                            {
                                return View(new { IdEmpleado = empleado.IdEmpleado });
                            }
                        }
                    }
                    else
                    {
                        var task = client.PutAsJsonAsync("Update", empleado);
                        task.Wait();

                        if (task.Result.IsSuccessStatusCode)
                        {
                            var jsonResponse = task.Result.Content.ReadAsStringAsync().Result;
                            ML.Result resultApi = JsonConvert.DeserializeObject<ML.Result>(jsonResponse);

                            if (resultApi.Correct)
                            {
                                return RedirectToAction("GetAll");
                            }
                            else
                            {
                                return View(new { IdEmpleado = empleado.IdEmpleado });
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult CargaMasiva()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CargaMasiva(IFormFile archivoCSV)
        {
            if (archivoCSV != null)
            {
                ML.Result result = _empleado.CargaMasivaCSV(archivoCSV);

                if (result.Correct)
                {
                    ViewBag.Mensaje = (string)result.Object;
                }
                else
                {
                    ViewBag.Error = result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Error = "Archivo inválido";
            }
            return View();
        }
    }
}

