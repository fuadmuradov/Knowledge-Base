using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.PublicationTagDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class PublicationTagService : IPublicationTagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PublicationTagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<PublicationTagGetDto> CreateAsync(PublicationTagPostDto postDto)
        {
            PublicationToTag publicationToTag = mapper.Map<PublicationToTag>(postDto);
            await unitOfWork.PublicationTagRepository.AddAsync(publicationToTag);
            await unitOfWork.PublicationTagRepository.SaveDbAsync();
            PublicationTagGetDto publicationTagGet = mapper.Map<PublicationTagGetDto>(publicationToTag);
            return publicationTagGet;

        }

        public async Task DeleteAsync(int id)
        {
            PublicationToTag publicationToTag = await unitOfWork.PublicationTagRepository.GetAsync(x=>x.Id==id);
            if (publicationToTag == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            unitOfWork.PublicationTagRepository.Delete(publicationToTag);
            await unitOfWork.PublicationTagRepository.SaveDbAsync();
        }

        public async Task<List<PublicationTagGetDto>> GetAllAsync()
        {
            List<PublicationToTag> publicationToTags = unitOfWork.PublicationTagRepository.GetAll(x=>x.Id>0, "Publication", "Tag").ToList();
            if (publicationToTags.Count == 0)
                throw new ItemNotFoundException("Item Not Found");
            List<PublicationTagGetDto> publicationTagGets = mapper.Map<List<PublicationToTag>, List<PublicationTagGetDto>>(publicationToTags);
            return publicationTagGets;
        }

        public async Task<PublicationTagGetDto> GetAsync(int id)
        {
            PublicationToTag publicationToTag = await unitOfWork.PublicationTagRepository.GetAsync(x => x.Id == id, "Publication", "Tag");
            if (publicationToTag == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            PublicationTagGetDto publicationTagGet = mapper.Map<PublicationTagGetDto>(publicationToTag);
            return publicationTagGet;
        }

        public async Task<PublicationTagGetDto> UpdateAsync(int id, PublicationTagPostDto postDto)
        {
            PublicationToTag publicationToTag = await unitOfWork.PublicationTagRepository.GetAsync(x => x.Id == id);
            if (publicationToTag == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");

            publicationToTag.PublicationId = postDto.PublicationId;
            publicationToTag.TagId = postDto.TagId;
            await unitOfWork.PublicationTagRepository.SaveDbAsync();

            PublicationTagGetDto publicationTagGet = mapper.Map<PublicationTagGetDto>(publicationToTag);

            return publicationTagGet;
        }
    }
}
