using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.CountryDTOs
{
    public class CountryPostDto
    {
        public string Name { get; set; }
    }

    public class CountryPostDtoValidation : AbstractValidator<CountryPostDto>
    {
        public CountryPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(150).WithMessage("Name must be lower then 150");
        }
    }
}
