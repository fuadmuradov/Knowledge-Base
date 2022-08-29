using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Models.DTOs.TopicDTOs;
using System.Text;

namespace KnowledgeBase.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class TopicController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<TopicGetDto> topicList = new List<TopicGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Topics"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    topicList = JsonConvert.DeserializeObject<List<TopicGetDto>>(apiResponse);
                }
            }

            return View(topicList);
        }

        [HttpPost]
        public async Task<IActionResult> TopicCreate(string aName)
        {

            if (!string.IsNullOrEmpty(aName))
            {
                TopicPostDto topicPostDto = new TopicPostDto();
                topicPostDto.Name = aName;
                TopicGetDto topic = new TopicGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(topicPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/Topics", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        topic = JsonConvert.DeserializeObject<TopicGetDto>(apiResponse);
                    }
                }
            }

            return LocalRedirect("/admin/topic/index");
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> TopicGet(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            TopicGetDto topic = new TopicGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Topics/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    topic = JsonConvert.DeserializeObject<TopicGetDto>(apiResponse);
                }
            }

            if (topic == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            return Json(topic);
        }

        public async Task<IActionResult> TopicEdit(string aName, int aId)
        {
            if (!string.IsNullOrEmpty(aName))
            {
                TopicPostDto topicPostDto = new TopicPostDto();
                topicPostDto.Name = aName;
                TopicGetDto topic = new TopicGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(topicPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://knowledgebase.api/api/Topics/" + aId, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        topic = JsonConvert.DeserializeObject<TopicGetDto>(apiResponse);
                    }
                }
            }
            return LocalRedirect("/admin/topic/index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TopicDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/Topics/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return LocalRedirect("/admin/topic/index");
        }
    }
}
