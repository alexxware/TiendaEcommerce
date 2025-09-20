using FluentValidation;
using TiendaPuntoVenta.DTOs;

namespace TiendaPuntoVenta.Validators;

public class UserLoginValidator: AbstractValidator<LoginUserDto>
{
    public UserLoginValidator()
    {
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
    }
}