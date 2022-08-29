using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.DTOs.PublicationDTOs
{
    public class PublicationPostDto
    {
    
        public int Year { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public int CountryId { get; set; }
        public int AuthorId { get; set; }
        public int OrganizationId { get; set; }
        public int DocumentTypeId { get; set; }
        public int TopicId { get; set; }
    }

    public class PublicationPostDtoValidation : AbstractValidator<PublicationPostDto>
    {
        public PublicationPostDtoValidation()
        {
            RuleFor(x => x.Year).NotNull().NotEmpty();
            RuleFor(x => x.FileName).NotNull().NotEmpty();
            RuleFor(x => x.FilePath).NotNull().NotEmpty();
            RuleFor(x => x.AuthorId).NotNull().NotEmpty();
            RuleFor(x => x.CountryId).NotNull().NotEmpty();
            RuleFor(x => x.OrganizationId).NotNull().NotEmpty();
            RuleFor(x => x.DocumentTypeId).NotNull().NotEmpty();
            RuleFor(x => x.TopicId).NotNull().NotEmpty();
        }
    }
}
