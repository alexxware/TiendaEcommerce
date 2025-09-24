using FluentValidation;
using TiendaPuntoVenta.DTOs.ProductsDto;

namespace TiendaPuntoVenta.Validators;

public class ProductInsertValidator: AbstractValidator<InsertProductosDto>
{
    public ProductInsertValidator()
    {
        RuleFor(product => product.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(product => product.Name).Length(3, 30).WithMessage("Name must be between 3 and 30 characters");
        RuleFor(product => product.UnitPrice).NotEmpty().WithMessage("Unit Price is required");
        RuleFor(product => product.UnitPrice).GreaterThan(0).WithMessage("Unit Price must be greater than 0");
    }
}