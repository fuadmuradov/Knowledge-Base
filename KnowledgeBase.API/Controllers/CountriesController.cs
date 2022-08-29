using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KnowledgeBase.Service.DTOs.CountryDTOs;
using KnowledgeBase.Service.IServices;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
namespace P2PLoan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;

        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CountryGet(int id)
        {
            var response = await countryService.GetAsync(id);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> CountryGetAll()
        {
            var response = await countryService.GetAllAsync();
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CountryCreate(CountryPostDto postDto)
        {
            var response = await countryService.CreateAsync(postDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> CountryUpdate(int id, CountryPostDto postDto)
        {
            var response = await countryService.UpdateAsync(id, postDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CountryDelete(int id)
        {
           await countryService.DeleteAsync(id);
            return Ok();
        }
    }
}
