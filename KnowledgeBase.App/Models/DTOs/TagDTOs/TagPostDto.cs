using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.TagDTOs
{
    public class TagPostDto
    {
        public string Name { get; set; }
    }

    public class TagPostDtoValidation : AbstractValidator<TagPostDto>
    {
        public TagPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(150).WithMessage("Name must be lower then 150");
        }
    }

}
