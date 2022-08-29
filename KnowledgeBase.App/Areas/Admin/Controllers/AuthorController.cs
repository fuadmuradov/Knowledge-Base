using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Models.DTOs.AuthorDTOs;
using System.Text;

namespace KnowledgeBase.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class AuthorController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<AuthorGetDto> authorList = new List<AuthorGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Authors"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    authorList = JsonConvert.DeserializeObject<List<AuthorGetDto>>(apiResponse);
                }
            }

            return View(authorList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AuthorDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/Authors/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return LocalRedirect("/admin/author/index");
        }


        [HttpPost]
        public async Task<IActionResult> AuthorCreate(string aName)
        {

            if (!string.IsNullOrEmpty(aName)) 
            {
                AuthorPostDto authorPostDto = new AuthorPostDto();
                authorPostDto.Name = aName;
                AuthorGetDto author = new AuthorGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(authorPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/Authors", content))
                    {
                       string apiResponse = await response.Content.ReadAsStringAsync();
                        author = JsonConvert.DeserializeObject<AuthorGetDto>(apiResponse);
                    }
                }
            }

            return LocalRedirect("/admin/author/index");
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> AuthorGet(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            AuthorGetDto author = new AuthorGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Authors/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    author = JsonConvert.DeserializeObject<AuthorGetDto>(apiResponse);
                }
            }

            if (author == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            return Json(author);
        }

        public async Task<IActionResult> AuthorEdit(string aName, int aId)
        {
            if (!string.IsNullOrEmpty(aName))
            {
                AuthorPostDto authorPostDto = new AuthorPostDto();
                authorPostDto.Name = aName;
                AuthorGetDto author = new AuthorGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(authorPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://knowledgebase.api/api/Authors/" + aId, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        author = JsonConvert.DeserializeObject<AuthorGetDto>(apiResponse);
                    }
                }
            }
            return LocalRedirect("/admin/author/index");
        }

    }
}
