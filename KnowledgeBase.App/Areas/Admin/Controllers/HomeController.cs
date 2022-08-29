using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Extension;
using KnowledgeBase.App.Models.DTOs.AuthorDTOs;
using KnowledgeBase.App.Models.DTOs.CountryDTOs;
using KnowledgeBase.App.Models.DTOs.DocumentTypeDTOs;
using KnowledgeBase.App.Models.DTOs.OrganizationDTOs;
using KnowledgeBase.App.Models.DTOs.PublicationDTOs;
using KnowledgeBase.App.Models.DTOs.PublicationTagDTOs;
using KnowledgeBase.App.Models.DTOs.TagDTOs;
using KnowledgeBase.App.Models.DTOs.TopicDTOs;
using KnowledgeBase.App.Models.ViewModel;
using System.Text;

namespace KnowledgeBase.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("admin/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment webHost;

        public HomeController(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
        }
        
        public async Task<IActionResult> Index()
        {
            PublicationVM publicationVM = await GetData();
            ViewBag.Tags = publicationVM.Tags;
            ViewBag.PublicationTags = publicationVM.PublicationTags;


            List<PublicationGetDto> publicationList = new List<PublicationGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Publications"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationList = JsonConvert.DeserializeObject<List<PublicationGetDto>>(apiResponse);
                }
            }

            return View(publicationList);
            return View();
        }

        public async Task<IActionResult> Create()
        {
            PublicationVM publicationVM = await GetData();

            ViewBag.Authors = publicationVM.Authors;
            ViewBag.Countries = publicationVM.Countries;
            ViewBag.DocumentTypes = publicationVM.DocumentTypes;
            ViewBag.Organizations = publicationVM.Organizations;
            ViewBag.Topics = publicationVM.Topics;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PublicationPostVM publicationPost)
        {

          //  if (!ModelState.IsValid) return View(publicationPost);

            PublicationPostDto publication = new PublicationPostDto();
            PublicationGetDto publicationGet = new PublicationGetDto();

            if(publicationPost.file == null)
            {
                return LocalRedirect("/admin/home/create");
            }
            publication.Year = publicationPost.Year;
            publication.FileName = publicationPost.file.FileName;
            publication.AuthorId = publicationPost.AuthorId;
            publication.CountryId = publicationPost.CountryId;
            publication.OrganizationId = publicationPost.OrganizationId;
            publication.DocumentTypeId = publicationPost.DocumentTypeId;
            publication.TopicId = publicationPost.TopicId;

            string folder = @"files\";
            publication.FilePath = await publicationPost.file.SavaAsync(webHost.WebRootPath, folder);

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(publication), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/Publications", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationGet = JsonConvert.DeserializeObject<PublicationGetDto>(apiResponse);
                }
            }

            return LocalRedirect("/admin/home/index");
        }

        public async Task<IActionResult> PublicationDelete(int id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/Publications/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return LocalRedirect("/admin/home/index");
        }

        public async Task<IActionResult> Update(int id)
        {
            PublicationVM publicationVM = await GetData();

            ViewBag.Authors = publicationVM.Authors;
            ViewBag.Countries = publicationVM.Countries;
            ViewBag.DocumentTypes = publicationVM.DocumentTypes;
            ViewBag.Organizations = publicationVM.Organizations;
            ViewBag.Topics = publicationVM.Topics;

            PublicationGetDto existPublication = new PublicationGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Publications/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    existPublication = JsonConvert.DeserializeObject<PublicationGetDto>(apiResponse);
                }
            }
            PublicationPostVM publication = new PublicationPostVM();
            publication.Year = existPublication.Year;
            publication.AuthorId = existPublication.Author.Id;
            publication.CountryId = existPublication.Country.Id;
            publication.OrganizationId = existPublication.Organization.Id;
            publication.DocumentTypeId = existPublication.DocumentType.Id;
            publication.TopicId = existPublication.Topic.Id;

            TempData["pubId"] = id.ToString();

            return View(publication);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PublicationPostVM publicationPostVM)
        {
            int id = Convert.ToInt32(TempData["pubId"]);
            PublicationGetDto existPublication = new PublicationGetDto() { };
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Publications/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    existPublication = JsonConvert.DeserializeObject<PublicationGetDto>(apiResponse);
                }
            }

            PublicationPostDto postDto = new PublicationPostDto();
            postDto.Year = publicationPostVM.Year;
            postDto.AuthorId = publicationPostVM.AuthorId;
            postDto.CountryId = publicationPostVM.CountryId;
            postDto.OrganizationId = publicationPostVM.OrganizationId;
            postDto.DocumentTypeId = publicationPostVM.DocumentTypeId;
            postDto.TopicId = publicationPostVM.TopicId;

            if(publicationPostVM.file != null)
            {
                postDto.FileName = publicationPostVM.file.FileName;
                try
                {
                    string folder = @"files\";
                    postDto.FilePath = await publicationPostVM.file.SavaAsync(webHost.WebRootPath, folder);
                    FileExtension.Delete(webHost.WebRootPath, folder, existPublication.FilePath);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "unexpected error for Update");
                    return View();
                }
            }
            else
            {
                postDto.FileName = existPublication.FileName;
                postDto.FilePath = existPublication.FilePath;
            }

            PublicationGetDto publicationGet = new PublicationGetDto();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(postDto), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("http://knowledgebase.api/api/Publications/" + id, content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationGet = JsonConvert.DeserializeObject<PublicationGetDto>(apiResponse);
                }
            }
            return LocalRedirect("/admin/home/index");
        }

        public async Task<IActionResult> TagUpdate(int id)
        {
            PublicationVM publicationVM = await GetData();
            ViewBag.Tags = publicationVM.Tags;
            ViewBag.PublicationTags = publicationVM.PublicationTags.Where(x=>x.Publication.Id==id).ToList();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Publications/" + id.ToString()))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Publication = JsonConvert.DeserializeObject<PublicationGetDto>(apiResponse);
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TagUpdate(PublicationTagVM publicationTag)
        {
            PublicationVM publicationVM = await GetData();
            List<PublicationTagGetDto> existPublicactionAndTag = publicationVM.PublicationTags.Where(x => x.Publication.Id == publicationTag.PublicationId).ToList();
           if(existPublicactionAndTag.Count != 0)
            {
                foreach (var item in existPublicactionAndTag)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.DeleteAsync("http://knowledgebase.api/api/PublicationTags/" + item.Id))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }


            if (publicationTag.TagIds.Count() > 0)
            {

                foreach (var item in publicationTag.TagIds)
                {
                    PublicationTagPostDto tagPostDto = new PublicationTagPostDto()
                    {
                        PublicationId = publicationTag.PublicationId,
                        TagId = item
                    };

                    using (var httpClient = new HttpClient())
                    {
                        StringContent content = new StringContent(JsonConvert.SerializeObject(tagPostDto), Encoding.UTF8, "application/json");
                        using (var response = await httpClient.PostAsync("http://knowledgebase.api/api/PublicationTags", content))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }

            return LocalRedirect("/admin/home/index/");
        }
        private async Task<PublicationVM> GetData()
        {
            PublicationVM publicationVM = new PublicationVM();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Authors"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.Authors = JsonConvert.DeserializeObject<List<AuthorGetDto>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Countries"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.Countries = JsonConvert.DeserializeObject<List<CountryGetDto>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Documents"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.DocumentTypes = JsonConvert.DeserializeObject<List<DocumentTypeGetDto>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Organizations"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.Organizations = JsonConvert.DeserializeObject<List<OrganizationGetDto>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Topics"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.Topics = JsonConvert.DeserializeObject<List<TopicGetDto>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Tags"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.Tags = JsonConvert.DeserializeObject<List<TagGetDto>>(apiResponse);
                }
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/PublicationTags"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.PublicationTags = JsonConvert.DeserializeObject<List<PublicationTagGetDto>>(apiResponse);
                }
            }
            return publicationVM;
        }

       
    }
}
