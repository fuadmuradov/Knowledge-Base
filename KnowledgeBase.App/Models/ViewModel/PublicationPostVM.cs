using FluentValidation;

namespace KnowledgeBase.App.Models.ViewModel
{
    public class PublicationPostVM
    {
        public int Year { get; set; }
        public int CountryId { get; set; }
        public int AuthorId { get; set; }
        public int OrganizationId { get; set; }
        public int DocumentTypeId { get; set; }
        public int TopicId { get; set; }

        public IFormFile file { get; set; }
    }

    public class PublicationPostVMValidation : AbstractValidator<PublicationPostVM>
    {
        public PublicationPostVMValidation()
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
