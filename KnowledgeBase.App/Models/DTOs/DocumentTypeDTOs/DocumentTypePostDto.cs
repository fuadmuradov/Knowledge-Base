
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase.App.Models.DTOs.DocumentTypeDTOs
{
    public class DocumentTypePostDto
    {
        public string TypeName { get; set; }
    }

    public class DocumentTypePostDtoValidation : AbstractValidator<DocumentTypePostDto>
    {
        public DocumentTypePostDtoValidation()
        {
            RuleFor(x => x.TypeName).NotEmpty().NotNull().MaximumLength(150).WithMessage("Name must be lower then 50");
        }
    }
}
