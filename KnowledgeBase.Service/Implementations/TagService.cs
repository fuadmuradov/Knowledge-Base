using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.TagDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class TagService : ITagService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<TagGetDto> CreateAsync(TagPostDto postDto)
        {
            Tag tag = mapper.Map<Tag>(postDto);
            await unitOfWork.TagRepository.AddAsync(tag);
            await unitOfWork.TagRepository.SaveDbAsync();
            TagGetDto tagGet = mapper.Map<TagGetDto>(tag);
            return tagGet;
        }

        public async Task DeleteAsync(int id)
        {
            Tag tag = await unitOfWork.TagRepository.GetAsync(x=>x.Id==id && x.IsDeleted==false);
            if (tag == null) throw new ItemNotFoundException($"Item Not Found by Id({id})");
            tag.IsDeleted = true;
            unitOfWork.TagRepository.SaveDbAsync().Wait();
        }

        public async Task<List<TagGetDto>> GetAllAsync()
        {
            List<Tag> tags = unitOfWork.TagRepository.GetAll(x=>x.IsDeleted==false, "PublicationToTags").ToList();
            if (tags.Count == 0) throw new ItemNotFoundException("Item Not Found");
            List<TagGetDto> tagGets = mapper.Map<List<Tag>,List<TagGetDto>>(tags);
            return tagGets;
        }

        public async Task<TagGetDto> GetAsync(int id)
        {
            Tag tag = await unitOfWork.TagRepository.GetAsync(x => x.Id == id && x.IsDeleted == false, "PublicationToTags");
            if (tag == null) throw new ItemNotFoundException($"Item Not Found by Id({id})");
            TagGetDto tagGet = mapper.Map<TagGetDto>(tag);
            return tagGet;
        }

        public async Task<TagGetDto> UpdateAsync(int id, TagPostDto postDto)
        {
            Tag tag = await unitOfWork.TagRepository.GetAsync(x => x.Id == id && x.IsDeleted == false, "PublicationToTags");
            if (tag == null) throw new ItemNotFoundException($"Item Not Found by Id({id})");
            tag.Name = postDto.Name;
            tag.ModifiedAt = DateTime.UtcNow;
            await unitOfWork.TagRepository.SaveDbAsync();
            TagGetDto tagGet = mapper.Map<TagGetDto>(tag);
            return tagGet;
        }
    }
}
