using FluentValidation;

namespace Service.DTO.Admin.Tag
{
    public class TagEditDto
    {
        public string Name { get; set; }
    }

    public class TagEditDtoValidator : AbstractValidator<TagEditDto>
    {
        public TagEditDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
        }
    }
}
