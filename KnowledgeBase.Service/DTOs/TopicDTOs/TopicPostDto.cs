using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.DTOs.TopicDTOs
{
    public class TopicPostDto
    {
        public string Name { get; set; }

    }

    public class TopicPostDtoValidation : AbstractValidator<TopicPostDto>
    {
        public TopicPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(150).WithMessage("Name must be lower then 150");
        }
    }
}
