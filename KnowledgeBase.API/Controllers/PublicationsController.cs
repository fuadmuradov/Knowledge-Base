using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.PublicationDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace KnowledgeBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly IPublicationService publicationService;

        public PublicationsController(IPublicationService publicationService)
        {
            this.publicationService = publicationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> PublicationGet(int id)
        {
           var response = await publicationService.GetAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> PublicationGetAll()
        {
            var response = await publicationService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PublicationCreate(PublicationPostDto postDto)
        {
            var response = await publicationService.CreateAsync(postDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PublicationUpdate(int id, PublicationPostDto postDto)
        {
            var response = await publicationService.UpdateAsync(id, postDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PublicationDelete(int id)
        {
             await publicationService.DeleteAsync(id);
             return Ok();
        }
    }
}
