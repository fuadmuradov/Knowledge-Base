using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.CountryDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<CountryGetDto> CreateAsync(CountryPostDto postDto)
        {
            Country country = mapper.Map<Country>(postDto);
            await unitOfWork.CountryRepository.AddAsync(country);
            await unitOfWork.CountryRepository.SaveDbAsync();
            CountryGetDto countryGet = mapper.Map<CountryGetDto>(country);
            return countryGet;
            
        }

        public async Task DeleteAsync(int id)
        {
            Country country = await unitOfWork.CountryRepository.GetAsync(x=>x.Id==id && x.IsDeleted==false);
            if (country == null) throw new ItemNotFoundException("Item Not Found");
            country.IsDeleted = true;
            await unitOfWork.CountryRepository.SaveDbAsync();
                      
        }

        public async Task<List<CountryGetDto>> GetAllAsync()
        {
            List<Country> countries = unitOfWork.CountryRepository.GetAll(x=>x.IsDeleted==false).ToList();
            if (countries.Count == 0) throw new ItemNotFoundException("Item Not Found");
            List<CountryGetDto> countryGets = mapper.Map<List<Country>, List<CountryGetDto>>(countries);
            return countryGets;
        }

        public async Task<CountryGetDto> GetAsync(int id)
        {
            Country country = await unitOfWork.CountryRepository.GetAsync(x => x.Id == id && x.IsDeleted == false, "Publications");
            if (country == null) throw new ItemNotFoundException("Item Not Found");
            CountryGetDto countryGet = mapper.Map<CountryGetDto>(country);
            return countryGet;
        }

        public async Task<CountryGetDto> UpdateAsync(int id, CountryPostDto postDto)
        {
            Country country = await unitOfWork.CountryRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (country == null) throw new ItemNotFoundException("Item Not Found");
            country.Name = postDto.Name;
            country.ModifiedAt = DateTime.UtcNow;
            await unitOfWork.CountryRepository.SaveDbAsync();
            CountryGetDto countryGet = mapper.Map<CountryGetDto>(country);
            return countryGet;
        }
    }
}
