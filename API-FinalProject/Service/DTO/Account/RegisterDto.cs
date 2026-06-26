using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace Service.DTO.Account
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username must be at most 50 characters long")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, ErrorMessage = "Full name must be at most 100 characters long")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }



    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().WithMessage("Username is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email format is wrong")
                                 .NotNull().WithMessage("Name is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Password is required");
        }
    }
}
