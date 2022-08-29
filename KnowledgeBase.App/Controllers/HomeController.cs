using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using KnowledgeBase.App.Models;
using KnowledgeBase.App.Models.DTOs.AuthorDTOs;
using KnowledgeBase.App.Models.DTOs.CountryDTOs;
using KnowledgeBase.App.Models.DTOs.DocumentTypeDTOs;
using KnowledgeBase.App.Models.DTOs.OrganizationDTOs;
using KnowledgeBase.App.Models.DTOs.PublicationDTOs;
using KnowledgeBase.App.Models.DTOs.PublicationTagDTOs;
using KnowledgeBase.App.Models.DTOs.TagDTOs;
using KnowledgeBase.App.Models.DTOs.TopicDTOs;
using KnowledgeBase.App.Models.ViewModel;
using System.Diagnostics;

namespace KnowledgeBase.App.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(ILogger<HomeController> logger)
        {

        }

        public async Task<IActionResult> Index()
        {
            PublicationVM publication = await GetData();
            return View(publication);
        }

        public async Task<IActionResult> GetList(int[] authorList, int[] organizationList, int[] countryList, int[] documentList, int[] topicList)
        {
            List<PublicationGetDto> allPublications = new List<PublicationGetDto>();
            List<PublicationGetDto> existPublications = new List<PublicationGetDto>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Publications"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    allPublications = JsonConvert.DeserializeObject<List<PublicationGetDto>>(apiResponse);
                }
            }

            authorList.Count();

            if (authorList.Count() > 0)
            {
                foreach (var id in authorList)
                {
                    existPublications.AddRange(allPublications.Where(x => x.Author.Id == id).ToList());
                }
                allPublications.Clear();

                allPublications.AddRange(existPublications);
            }


            if (organizationList.Count() > 0)
            {
                existPublications.Clear();
                foreach (var id in organizationList)
                {
                    existPublications.AddRange(allPublications.Where(x => x.Organization.Id == id).ToList());
                }

                allPublications.Clear();

                allPublications.AddRange(existPublications);
            }

            if (countryList.Count() > 0)
            {
                existPublications.Clear();
                foreach (var id in countryList)
                {
                    existPublications.AddRange(allPublications.Where(x => x.Country.Id == id).ToList());
                }

                allPublications.Clear();

                allPublications.AddRange(existPublications);
            }

            if (documentList.Count() > 0)
            {
                existPublications.Clear();
                foreach (var id in documentList)
                {
                    existPublications.AddRange(allPublications.Where(x => x.DocumentType.Id == id).ToList());
                }

                allPublications.Clear();

                allPublications.AddRange(existPublications);
            }

            if (topicList.Count() > 0)
            {
                existPublications.Clear();
                foreach (var id in topicList)
                {
                    existPublications.AddRange(allPublications.Where(x => x.Topic.Id == id).ToList());
                }

                allPublications.Clear();

                allPublications.AddRange(existPublications);
            }
            return PartialView("_ListPartialView", existPublications);
        }



        public async Task<IActionResult> GetTagList(int tagId)
        {
            PublicationVM publicationVM = await GetTagData();
            List<PublicationGetDto> allPublications = publicationVM.Publications;
            List<PublicationTagGetDto> publicationTags = publicationVM.PublicationTags.Where(x => x.Tag.Id == tagId).ToList();
            List<PublicationGetDto> existPublications = new List<PublicationGetDto>();

            foreach (var item in publicationTags)
            {
                existPublications.Add(allPublications.FirstOrDefault(x => x.Id == item.Publication.Id));
            }

            return PartialView("_ListPartialView", existPublications);
        }


        private async Task<PublicationVM> GetData()
        {
            PublicationVM publicationVM = new PublicationVM();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Publications"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.Publications = JsonConvert.DeserializeObject<List<PublicationGetDto>>(apiResponse);
                }
            }


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

        private async Task<PublicationVM> GetTagData()
        {
            PublicationVM publicationVM = new PublicationVM();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://knowledgebase.api/api/Publications"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    publicationVM.Publications = JsonConvert.DeserializeObject<List<PublicationGetDto>>(apiResponse);
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