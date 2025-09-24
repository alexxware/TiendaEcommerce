using FluentValidation;
using TiendaPuntoVenta.DTOs;

namespace TiendaPuntoVenta.Validators;

public class ProductoValidator: AbstractValidator<ProductosDto>
{
    public ProductoValidator()
    {
        RuleFor(product => product.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(product => product.Id).GreaterThan(0).WithMessage("Id must be greater than 0");
        RuleFor(product => product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(product => product.Name).Length(3, 30).WithMessage("Name must be between 3 and 30 characters");
        RuleFor(product => product.UnitPrice).NotEmpty().WithMessage("Unit Price is required");
        RuleFor(product => product.UnitPrice).GreaterThan(0).WithMessage("Unit Price must be greater than 0");
        RuleFor(product => product.Stock).NotEmpty().WithMessage("Stock is required");
        RuleFor(product => product.Stock).GreaterThan(0).WithMessage("Stock must be greater than 0");
    }
}