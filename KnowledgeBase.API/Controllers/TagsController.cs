using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.TagDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace KnowledgeBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService tagService;

        public TagsController(ITagService tagService)
        {
            this.tagService = tagService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TagGet(int id)
        {
            var response = await tagService.GetAsync(id);
            return StatusCode(200, response);
        }

        [HttpGet]
        public async Task<IActionResult> TagGetAll()
        {
            var response = await tagService.GetAllAsync();
            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> TagCreate(TagPostDto postDto)
        {
            var response = await tagService.CreateAsync(postDto);
            return StatusCode(200, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> TagUpdate(int id, TagPostDto postDto)
        {
            var response = await tagService.UpdateAsync(id, postDto);
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> TagDelete(int id)
        {
            await tagService.DeleteAsync(id);
            return Ok();
        }

    }
}
