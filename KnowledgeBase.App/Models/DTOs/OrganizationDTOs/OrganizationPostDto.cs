
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.OrganizationDTOs
{
    public class OrganizationPostDto
    {
        public string Name { get; set; }
    }

    public class OrganizationPostDtoValidation : AbstractValidator<OrganizationPostDto>
    {
        public OrganizationPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(2000).WithMessage("Name must be lower then 2000");
        }
    }
}
