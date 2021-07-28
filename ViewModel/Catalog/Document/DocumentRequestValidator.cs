using FluentValidation;

namespace ViewModel.Catalog.Document
{
    public class DocumentRequestValidator : AbstractValidator<DocumentRequest>
    {
        public DocumentRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required")
                .MaximumLength(200).WithMessage("Category name can't over 200 character");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description can't over 200 character");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required")
                .MaximumLength(200).WithMessage("Description can't over 200 character");
        }
    }
}
