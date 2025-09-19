using FluentValidation;
using TiendaPuntoVenta.DTOs;

namespace TiendaPuntoVenta.Validators;

public class UserInsertValidator: AbstractValidator<InsertUserDto>
{
    public UserInsertValidator()
    {
        RuleFor(user => user.Username).NotEmpty().WithMessage("Username is required");
        RuleFor(user => user.Username).Length(2, 50).WithMessage("Username must be between 2 and 50 characters");
        RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(user => user.Email).EmailAddress().WithMessage("Email address format is invalid");
        RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
        RuleFor(user => user.Password).Length(6, 16).WithMessage("Password must be between 6 and 16 characters");
        
    }
}