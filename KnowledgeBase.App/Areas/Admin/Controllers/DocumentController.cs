using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Models.DTOs.DocumentTypeDTOs;
using System.Text;

namespace KnowledgeBase.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class DocumentController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<DocumentTypeGetDto> documentList = new List<DocumentTypeGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Documents"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    documentList = JsonConvert.DeserializeObject<List<DocumentTypeGetDto>>(apiResponse);
                }
            }

            return View(documentList);
        }

        [HttpPost]
        public async Task<IActionResult> DocumentCreate(string aName)
        {

            if (!string.IsNullOrEmpty(aName))
            {
                DocumentTypePostDto documentPostDto = new DocumentTypePostDto();
                documentPostDto.TypeName = aName;
                DocumentTypeGetDto document = new DocumentTypeGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(documentPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/Documents", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        document = JsonConvert.DeserializeObject<DocumentTypeGetDto>(apiResponse);
                    }
                }
            }

            return LocalRedirect("/admin/document/index");
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> DocumentGet(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            DocumentTypeGetDto document = new DocumentTypeGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Documents/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    document = JsonConvert.DeserializeObject<DocumentTypeGetDto>(apiResponse);
                }
            }

            if (document == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            return Json(document);
        }

        public async Task<IActionResult> DocumentEdit(string aName, int aId)
        {
            if (!string.IsNullOrEmpty(aName))
            {
                DocumentTypePostDto documentPostDto = new DocumentTypePostDto();
                documentPostDto.TypeName = aName;
                DocumentTypeGetDto document = new DocumentTypeGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(documentPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://knowledgebase.api/api/Documents/" + aId, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        document = JsonConvert.DeserializeObject<DocumentTypeGetDto>(apiResponse);
                    }
                }
            }
            return LocalRedirect("/admin/document/index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DocumentDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/Documents/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return LocalRedirect("/admin/document/index");
        }
    }
}
