using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.Service.DTOs.AuthorDTOs
{
    public class AuthorPostDto
    {
        public string Name { get; set; }
    }

    public class AuthorPostDtoValidation : AbstractValidator<AuthorPostDto>
    {
        public AuthorPostDtoValidation()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(250);
        }
    }

}
