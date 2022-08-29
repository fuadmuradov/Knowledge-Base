using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.PublicationDTOs
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
            RuleFor(x => x.Year).NotNull().WithMessage("Year is Required..").NotEmpty().WithMessage("Year is Required..");
            RuleFor(x => x.AuthorId).NotNull().WithMessage("Author is Required..").NotEmpty().WithMessage("Author is Required..");
            RuleFor(x => x.CountryId).NotNull().WithMessage("Country is Required..").NotEmpty().WithMessage("Country is Required..");
            RuleFor(x => x.OrganizationId).NotNull().WithMessage("Organization is Required..").NotEmpty().WithMessage("Organization is Required..");
            RuleFor(x => x.DocumentTypeId).NotNull().WithMessage("DocumentType is Required..").NotEmpty().WithMessage("DocumentType is Required..");
            RuleFor(x => x.TopicId).NotNull().WithMessage("Topic is Required..").NotEmpty().WithMessage("Topic is Required..");
        }
    }
}
