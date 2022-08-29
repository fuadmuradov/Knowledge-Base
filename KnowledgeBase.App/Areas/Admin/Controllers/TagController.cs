using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Models.DTOs.TagDTOs;
using System.Text;

namespace KnowledgeBase.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class TagController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<TagGetDto> tagList = new List<TagGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Tags"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tagList = JsonConvert.DeserializeObject<List<TagGetDto>>(apiResponse);
                }
            }

            return View(tagList);
        }

        [HttpPost]
        public async Task<IActionResult> TagCreate(string aName)
        {

            if (!string.IsNullOrEmpty(aName))
            {
                TagPostDto tagPostDto = new TagPostDto();
                tagPostDto.Name = aName;
                TagGetDto tag = new TagGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(tagPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/Tags", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        tag = JsonConvert.DeserializeObject<TagGetDto>(apiResponse);
                    }
                }
            }

            return LocalRedirect("/admin/tag/index");
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> TagGet(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            TagGetDto tag = new TagGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Tags/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    tag = JsonConvert.DeserializeObject<TagGetDto>(apiResponse);
                }
            }

            if (tag == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            return Json(tag);
        }

        public async Task<IActionResult> TagEdit(string aName, int aId)
        {
            if (!string.IsNullOrEmpty(aName))
            {
                TagPostDto tagPostDto = new TagPostDto();
                tagPostDto.Name = aName;
                TagGetDto tag = new TagGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(tagPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://knowledgebase.api/api/Tags/" + aId, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        tag = JsonConvert.DeserializeObject<TagGetDto>(apiResponse);
                    }
                }
            }
            return LocalRedirect("/admin/tag/index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TagDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/Tags/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return LocalRedirect("/admin/tag/index");
        }
    }
}
