using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.PublicationTagDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace KnowledgeBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationTagsController : ControllerBase
    {
        private readonly IPublicationTagService publicationTag;

        public PublicationTagsController(IPublicationTagService publicationTag)
        {
            this.publicationTag = publicationTag;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> PublicationTagGet(int id)
        {
            var response = await publicationTag.GetAsync(id);
            return StatusCode(200, response);
        }

        [HttpGet]
        public async Task<IActionResult> PublicationTagGetAll()
        {
            var response = await publicationTag.GetAllAsync();
            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> PublicationTagCreate(PublicationTagPostDto postDto)
        {
            var response = await publicationTag.CreateAsync(postDto);
            return StatusCode(200, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PublicationTagUpdate(int id, PublicationTagPostDto postDto)
        {
            var response = await publicationTag.UpdateAsync(id, postDto);
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> PublicationTagDelete(int id)
        {
            await publicationTag.DeleteAsync(id);
            return Ok();
        }
    }
}
