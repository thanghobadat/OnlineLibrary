using FluentValidation;

namespace ViewModel.Catalog.Document
{
    public class DocumentImageViewModelValidator : AbstractValidator<DocumentImageViewModel>
    {
        public DocumentImageViewModelValidator()
        {
            RuleFor(x => x.Caption).NotEmpty().WithMessage("Category name is required")
                .MaximumLength(200).WithMessage("Category name can't over 200 character");
        }
    }
}
