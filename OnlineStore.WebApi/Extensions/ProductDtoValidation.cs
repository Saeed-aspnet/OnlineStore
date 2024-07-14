using FluentValidation;
using OnlineStore.Application.Dtos;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(x => x.Title).Cascade(CascadeMode.StopOnFirstFailure)
                             .NotEmpty().WithMessage("Title can not be empty")
                             .NotNull().WithMessage("Title can not be null.")
                             .Must(x => x.Length < 40).WithMessage("Title should be less than 40 characters.");
    }
}