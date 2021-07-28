using FluentValidation;

namespace ViewModel.Catalog.Category
{
    public class CategoryViewModelValidator : AbstractValidator<CategoryViewModel>
    {
        public CategoryViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required")
                .MaximumLength(200).WithMessage("Category name can't over 200 character");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required")
                .MaximumLength(200).WithMessage("Description can't over 200 character");
        }
    }
}
