using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.PublicationTagDTOs
{
    public class PublicationTagPostDto
    {
        public int PublicationId { get; set; }
        public int TagId { get; set; }
    }

    public class PublicationTagPostDtoValidation : AbstractValidator<PublicationTagPostDto>
    {
        public PublicationTagPostDtoValidation()
        {
            RuleFor(x => x.PublicationId).NotEmpty().NotNull().WithMessage("Publication Id is Required...");
            RuleFor(x => x.TagId).NotEmpty().NotNull().WithMessage("Tag Id is Required...");
        }
    }
}
