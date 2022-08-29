using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Models.DTOs.CountryDTOs;
using System.Text;

namespace KnowledgeBase.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class CountryController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<CountryGetDto> countryList = new List<CountryGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Countries"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    countryList = JsonConvert.DeserializeObject<List<CountryGetDto>>(apiResponse);
                }
            }

            return View(countryList);
        }

        [HttpPost]
        public async Task<IActionResult> CountryCreate(string aName)
        {

            if (!string.IsNullOrEmpty(aName))
            {
                CountryPostDto countryPostDto = new CountryPostDto();
                countryPostDto.Name = aName;
                CountryGetDto country = new CountryGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(countryPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/Countries", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        country = JsonConvert.DeserializeObject<CountryGetDto>(apiResponse);
                    }
                }
            }

            return LocalRedirect("/admin/country/index");
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> CountryGet(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            CountryGetDto country = new CountryGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Countries/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    country = JsonConvert.DeserializeObject<CountryGetDto>(apiResponse);
                }
            }

            if (country == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            return Json(country);
        }

        public async Task<IActionResult> CountryEdit(string aName, int aId)
        {
            if (!string.IsNullOrEmpty(aName))
            {
                CountryPostDto countryPostDto = new CountryPostDto();
                countryPostDto.Name = aName;
                CountryGetDto country = new CountryGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(countryPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://knowledgebase.api/api/Countries/" + aId, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        country = JsonConvert.DeserializeObject<CountryGetDto>(apiResponse);
                    }
                }
            }
            return LocalRedirect("/admin/country/index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CountryDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/Countries/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return LocalRedirect("/admin/country/index");
        }
    }
}
