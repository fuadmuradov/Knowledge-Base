using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.AuthorDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<AuthorGetDto> CreateAsync(AuthorPostDto postDto)
        {
            Author author = mapper.Map<Author>(postDto);
            await unitOfWork.AuthorRepository.AddAsync(author);
            await unitOfWork.AuthorRepository.SaveDbAsync();
            AuthorGetDto authorGet = mapper.Map<AuthorGetDto>(author);
            return authorGet;
        }

        public async Task DeleteAsync(int id)
        {
            Author author = await unitOfWork.AuthorRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (author == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            unitOfWork.AuthorRepository.Delete(author);
            author.IsDeleted = true;
            await unitOfWork.AuthorRepository.SaveDbAsync();
        }

        public async Task<List<AuthorGetDto>> GetAllAsync()
        {
            List<Author> authors = unitOfWork.AuthorRepository.GetAll(x => !x.IsDeleted, "Publications").ToList();
            if (authors.Count == 0) throw new ItemNotFoundException("Item Not Found");
            List<AuthorGetDto> authorGets = mapper.Map<List<Author>, List<AuthorGetDto>>(authors);
            return authorGets;
        }

        public async Task<AuthorGetDto> GetAsync(int id)
        {
            Author author = await unitOfWork.AuthorRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (author == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            AuthorGetDto authorGet = mapper.Map<AuthorGetDto>(author);
            return authorGet;
        }

        public async Task<AuthorGetDto> UpdateAsync(int id, AuthorPostDto postDto)
        {
            Author author = await unitOfWork.AuthorRepository.GetAsync(x => x.Id == id && !x.IsDeleted);
            if (author == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");

            author.Name = postDto.Name;
            author.ModifiedAt = DateTime.UtcNow;

            await unitOfWork.AuthorRepository.SaveDbAsync();
            AuthorGetDto authorGet = mapper.Map<AuthorGetDto>(author);
            return authorGet;
        }
    }
}
