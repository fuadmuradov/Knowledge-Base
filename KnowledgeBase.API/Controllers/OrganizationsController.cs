using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.OrganizationDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace KnowledgeBase.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationService organizationService;

        public OrganizationsController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> OrganizationGet(int id)
        {
            var response = await organizationService.GetAsync(id);
            return StatusCode(200, response);
        }

        [HttpGet]
        public async Task<IActionResult> OrganizationGetAll()
        {
            var response = await organizationService.GetAllAsync();
            return StatusCode(200, response);
        }

        [HttpPost]
        public async Task<IActionResult> OrganizationCreate(OrganizationPostDto postDto)
        {
            var response = await organizationService.CreateAsync(postDto);
            return StatusCode(200, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> OrganizationUpdate(int id, OrganizationPostDto postDto)
        {
            var response = await organizationService.UpdateAsync(id, postDto);
            return StatusCode(200, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> OrganizationDelete(int id)
        {
           await organizationService.DeleteAsync(id);
            return Ok();
        }
    }
}
