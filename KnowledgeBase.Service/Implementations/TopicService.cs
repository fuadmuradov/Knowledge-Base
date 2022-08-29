using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.TopicDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<TopicGetDto> CreateAsync(TopicPostDto postDto)
        {
            Topic topic = mapper.Map<Topic>(postDto);
            await unitOfWork.TopicRepository.AddAsync(topic);
            await unitOfWork.TopicRepository.SaveDbAsync();
            TopicGetDto topicGet = mapper.Map<TopicGetDto>(topic);
            return topicGet;

        }

        public async Task DeleteAsync(int id)
        {
            Topic topic = await unitOfWork.TopicRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (topic == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            topic.IsDeleted = true;
            await unitOfWork.TopicRepository.SaveDbAsync();
        }

        public async Task<List<TopicGetDto>> GetAllAsync()
        {
            List<Topic> topics = unitOfWork.TopicRepository.GetAll(x => !x.IsDeleted, "Publications").ToList();
            if (topics.Count == 0)
                throw new ItemNotFoundException("Item Not Found");
            List<TopicGetDto> topicGets = mapper.Map<List<Topic>, List<TopicGetDto>>(topics);
            return topicGets;

        }

        public async Task<TopicGetDto> GetAsync(int id)
        {
            Topic topic = await unitOfWork.TopicRepository.GetAsync(x => x.Id == id && x.IsDeleted == false, "Publications");
            if (topic == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            TopicGetDto topicGets = mapper.Map<TopicGetDto>(topic);
            return topicGets;
        }

        public async Task<TopicGetDto> UpdateAsync(int id, TopicPostDto postDto)
        {
            Topic topic = await unitOfWork.TopicRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (topic == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            topic.Name = postDto.Name;
            await unitOfWork.TopicRepository.SaveDbAsync();
            TopicGetDto topicGets = mapper.Map<TopicGetDto>(topic);
            return topicGets;
        }
    }
}
