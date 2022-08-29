using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.OrganizationDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class OrganizatoinService : IOrganizationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrganizatoinService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<OrganizationGetDto> CreateAsync(OrganizationPostDto postDto)
        {
           Organization organization = mapper.Map<Organization>(postDto);
           await unitOfWork.OrganizationRepository.AddAsync(organization);
            await unitOfWork.OrganizationRepository.SaveDbAsync();
            OrganizationGetDto organizationGet = mapper.Map<OrganizationGetDto>(organization);
            return organizationGet;
        }

        public async Task DeleteAsync(int id)
        {
            Organization organization = await unitOfWork.OrganizationRepository.GetAsync(x=>x.Id==id && x.IsDeleted==false);
            if (organization == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            organization.IsDeleted = true;
            await unitOfWork.OrganizationRepository.SaveDbAsync();
        }

        public async Task<List<OrganizationGetDto>> GetAllAsync()
        {
            List<Organization> organizations = unitOfWork.OrganizationRepository.GetAll(x=>x.IsDeleted==false, "Publications").ToList();
            if (organizations.Count == 0)
                throw new ItemNotFoundException("Item Not Found");
            List<OrganizationGetDto> organizationGets = mapper.Map<List<Organization>, List<OrganizationGetDto>>(organizations);
            return organizationGets;
        }

        public async Task<OrganizationGetDto> GetAsync(int id)
        {
            Organization organization = await unitOfWork.OrganizationRepository.GetAsync(x => x.Id == id && x.IsDeleted == false, "Publications");
            if (organization == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            OrganizationGetDto organizationGet = mapper.Map<OrganizationGetDto>(organization);
            return organizationGet;
        }

        public async Task<OrganizationGetDto> UpdateAsync(int id, OrganizationPostDto postDto)
        {
            Organization organization = await unitOfWork.OrganizationRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (organization == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            organization.Name = postDto.Name;
            organization.ModifiedAt = DateTime.UtcNow;
            await unitOfWork.OrganizationRepository.SaveDbAsync();
            OrganizationGetDto organizationGet = mapper.Map<OrganizationGetDto>(organization);

            return organizationGet;
        }
    }
}
