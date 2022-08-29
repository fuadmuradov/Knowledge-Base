using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.DocumentTypeDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace KnowledgeBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentTypeService documentTypeService;

        public DocumentsController(IDocumentTypeService documentTypeService)
        {
            this.documentTypeService = documentTypeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DocumentGet(int id)
        {
            var response = await documentTypeService.GetAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> DocumentGetAll()
        {
            var response = await documentTypeService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> DocumentCreate(DocumentTypePostDto postDto)
        {
            var response = await documentTypeService.CreateAsync(postDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> DocumentPut(int id, DocumentTypePostDto postDto)
        {
            var response = await documentTypeService.UpdateAsync(id, postDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DocumentDelete(int id)
        {
           await documentTypeService.DeleteAsync(id);
            return Ok();
        }
    }
}
