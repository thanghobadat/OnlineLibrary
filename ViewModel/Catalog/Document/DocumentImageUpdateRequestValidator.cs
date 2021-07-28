using FluentValidation;

namespace ViewModel.Catalog.Document
{
    public class DocumentImageUpdateRequestValidator : AbstractValidator<DocumentImageUpdateRequest>
    {
        public DocumentImageUpdateRequestValidator()
        {
            RuleFor(x => x.Caption).NotEmpty().WithMessage("Category name is required")
                .MaximumLength(200).WithMessage("Category name can't over 200 character");
            RuleFor(x => x.ThumbnailImage).NotEmpty().WithMessage("Please Choose Image");
        }
    }
}
