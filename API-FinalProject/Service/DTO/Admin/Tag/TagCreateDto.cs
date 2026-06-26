using FluentValidation;
using Service.DTO.Admin.AskUsFrom;

namespace Service.DTO.Admin.Tag
{
    public class TagCreateDto
    {
        public string Name { get; set; }
    }

    public class TagCreateDtoValidator : AbstractValidator<TagCreateDto>
    {
        public TagCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");           
        }
    }
}
