using FluentValidation;
namespace Service.DTO.Account
{
    public class LoginDto
    {
        public string EmailOrUserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.EmailOrUserName).NotEmpty().WithMessage("Username or email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
