using AutoMapper;
using KnowledgeBase.Core;
using KnowledgeBase.Core.Entities;
using KnowledgeBase.Service.DTOs.DocumentTypeDTOs;
using KnowledgeBase.Service.Exceptions;
using KnowledgeBase.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.Implementations
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DocumentTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<DocumentTypeGetDto> CreateAsync(DocumentTypePostDto postDto)
        {
           DocumentType document = mapper.Map<DocumentType>(postDto);
           await unitOfWork.DocumentTypeRepository.AddAsync(document);
           await unitOfWork.DocumentTypeRepository.SaveDbAsync();
            DocumentTypeGetDto documentTypeGet = mapper.Map<DocumentTypeGetDto>(document);
            return documentTypeGet;
        }

        public async Task DeleteAsync(int id)
        {
            DocumentType document = await unitOfWork.DocumentTypeRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (document == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            document.IsDeleted = true;
            await unitOfWork.DocumentTypeRepository.SaveDbAsync();        
        }

        public async Task<List<DocumentTypeGetDto>> GetAllAsync()
        {
            List<DocumentType> documents = unitOfWork.DocumentTypeRepository.GetAll(x=>x.IsDeleted == false, "Publications").ToList();
            if (documents.Count == 0)
                throw new ItemNotFoundException("Item Not Found");
            List<DocumentTypeGetDto> documentTypeGets = mapper.Map <List<DocumentType>, List<DocumentTypeGetDto>>(documents);
            return documentTypeGets;
        }

        public async Task<DocumentTypeGetDto> GetAsync(int id)
        {
            DocumentType document = await unitOfWork.DocumentTypeRepository.GetAsync(x => x.Id == id && x.IsDeleted == false, "Publications");
            if (document == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            DocumentTypeGetDto documentTypeGets = mapper.Map <DocumentTypeGetDto>(document);
            return documentTypeGets;
        }

        public async Task<DocumentTypeGetDto> UpdateAsync(int id, DocumentTypePostDto postDto)
        {
            DocumentType document = await unitOfWork.DocumentTypeRepository.GetAsync(x => x.Id == id && x.IsDeleted == false);
            if (document == null)
                throw new ItemNotFoundException($"Item Not Found by Id({id})");
            document.TypeName = postDto.TypeName;
            document.ModifiedAt = DateTime.UtcNow;
            await unitOfWork.DocumentTypeRepository.SaveDbAsync();
            DocumentTypeGetDto documentTypeGet = mapper.Map <DocumentTypeGetDto>(document);
            return documentTypeGet;
        }
    }
}
