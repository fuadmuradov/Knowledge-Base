using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.AuthorDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace KnowledgeBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public AuthorsController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AuthorGet(int id)
        {
            var response = await authorService.GetAsync(id);
            return StatusCode(200, response);
        }

        [HttpGet]
        public async Task<IActionResult> AuthorGetAll()
        {
            var response = await authorService.GetAllAsync();
            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> AuthorCreate(AuthorPostDto postDto)
        {
            var response = await authorService.CreateAsync(postDto);
            return StatusCode(200, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AuthorUpdate(int id, AuthorPostDto postDto)
        {
            var response = await authorService.UpdateAsync(id, postDto);
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> AuthorDelete(int id)
        {
            await authorService.DeleteAsync(id);
            return StatusCode(200);
        }
    }
}
