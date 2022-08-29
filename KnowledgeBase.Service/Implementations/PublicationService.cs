using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.PublicationDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class PublicationService : IPublicationService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PublicationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<PublicationGetDto> CreateAsync(PublicationPostDto postDto)
        {
            Publication publication = mapper.Map<Publication>(postDto);
            await unitOfWork.PublicationRepository.AddAsync(publication);
            await unitOfWork.PublicationRepository.SaveDbAsync();
            PublicationGetDto publicationGetDto = mapper.Map<PublicationGetDto>(publication);
            return publicationGetDto;
        }

        public async Task DeleteAsync(int id)
        {
            Publication publication = await unitOfWork.PublicationRepository.GetAsync(x=>x.Id==id && x.IsDeleted==false);
            if (publication == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            publication.IsDeleted = true;
            await unitOfWork.PublicationRepository.SaveDbAsync();
        
        }

        public async Task<List<PublicationGetDto>> GetAllAsync()
        {
            List<Publication> publications = unitOfWork.PublicationRepository.GetAll(x=>x.IsDeleted==false, "Country", "Author", "Organization", "Topic", "DocumentType", "PublicationToTags").ToList();
            if (publications.Count == 0)
                throw new ItemNotFoundException("Item Not Found");
            List<PublicationGetDto> publicationGets = mapper.Map<List<Publication>, List<PublicationGetDto>>(publications);
            return publicationGets;
        }

        public async Task<PublicationGetDto> GetAsync(int id)
        {
            Publication publication = await unitOfWork.PublicationRepository.GetAsync(x => x.Id == id && x.IsDeleted == false, "Country", "Author", "Organization", "Topic", "DocumentType", "PublicationToTags");
            if (publication == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            PublicationGetDto publicationGet = mapper.Map<PublicationGetDto>(publication);
            return publicationGet;

        }

        public async Task<PublicationGetDto> UpdateAsync(int id, PublicationPostDto postDto)
        {
            Publication publication = await unitOfWork.PublicationRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (publication == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            publication.Year = postDto.Year;
            publication.FileName = postDto.FileName;
            publication.FilePath = postDto.FilePath;
            publication.AuthorId = postDto.AuthorId;
            publication.CountryId = postDto.CountryId;
            publication.DocumentTypeId = postDto.DocumentTypeId;
            publication.OrganizationId = postDto.OrganizationId;
            publication.TopicId = postDto.TopicId;

            await unitOfWork.PublicationRepository.SaveDbAsync();
            PublicationGetDto publicationGet = mapper.Map<PublicationGetDto>(publication);
            return publicationGet;
        }
    }
}
