using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.TopicDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace KnowledgeBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService topicService;

        public TopicsController(ITopicService topicService)
        {
            this.topicService = topicService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> TopicGet(int id)
        {
            var response = await topicService.GetAsync(id);
            return StatusCode(200, response);
        }

        [HttpGet]
        public async Task<IActionResult> TopicGetAll()
        {
            var response = await topicService.GetAllAsync();
            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> TopicCreate(TopicPostDto postDto)
        {
            var response = await topicService.CreateAsync(postDto);
            return StatusCode(200, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> TopicUpdate(int id, TopicPostDto postDto)
        {
            var response = await topicService.UpdateAsync(id, postDto);
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> TopicDelete(int id)
        {
            await topicService.DeleteAsync(id);
            return Ok();
        }
    }
}
