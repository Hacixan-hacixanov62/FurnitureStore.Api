
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Service.DTO.Admin.AskUsFrom
{
    public class AskUsFromCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }

    public class AskUsFromCreateDtoValidator : AbstractValidator<AskUsFromCreateDto>
    {
        public AskUsFromCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");             

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email cannot be empty.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Message cannot be empty.")
                .MinimumLength(10).WithMessage("Message must be at least 10 characters long.")
                .MaximumLength(500).WithMessage("Message cannot exceed 500 characters.");
        }
    }
}
