using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Models.DTOs.OrganizationDTOs;
using System.Text;

namespace KnowledgeBase.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class OrganizationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<OrganizationGetDto> organizationList = new List<OrganizationGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Organizations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    organizationList = JsonConvert.DeserializeObject<List<OrganizationGetDto>>(apiResponse);
                }
            }

            return View(organizationList);
        }

        [HttpPost]
        public async Task<IActionResult> OrganizationCreate(string aName)
        {

            if (!string.IsNullOrEmpty(aName))
            {
                OrganizationPostDto organizationPostDto = new OrganizationPostDto();
                organizationPostDto.Name = aName;
                OrganizationGetDto organization = new OrganizationGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(organizationPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/Organizations", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        organization = JsonConvert.DeserializeObject<OrganizationGetDto>(apiResponse);
                    }
                }
            }

            return LocalRedirect("/admin/organization/index");
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> OrganizationGet(int? id)
        {
            if (id == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            OrganizationGetDto organization = new OrganizationGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Organizations/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    organization = JsonConvert.DeserializeObject<OrganizationGetDto>(apiResponse);
                }
            }

            if (organization == null)
            {
                return Json(new
                {
                    status = 404
                });
            }

            return Json(organization);
        }

        public async Task<IActionResult> OrganizationEdit(string aName, int aId)
        {
            if (!string.IsNullOrEmpty(aName))
            {
                OrganizationPostDto organizationPostDto = new OrganizationPostDto();
                organizationPostDto.Name = aName;
                OrganizationGetDto organization = new OrganizationGetDto();
                using (var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(organizationPostDto), Encoding.UTF8, "application/json");
                    using (var response = await httpClient.PutAsync("http://knowledgebase.api/api/Organizations/" + aId, content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        organization = JsonConvert.DeserializeObject<OrganizationGetDto>(apiResponse);
                    }
                }
            }
            return LocalRedirect("/admin/organization/index");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> OrganizationDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/Organizations/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return LocalRedirect("/admin/organization/index");
        }
    }
}
