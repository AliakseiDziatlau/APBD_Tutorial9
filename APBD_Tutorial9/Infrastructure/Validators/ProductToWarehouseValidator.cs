using System.Data;
using APBD_Tutorial9.Application.Commands;
using APBD_Tutorial9.Domain;
using FluentValidation;

namespace APBD_Tutorial9.Infrastructure.Validators;

public class ProductToWarehouseValidator : AbstractValidator<AddProductToWarehouseCommand>
{
    public ProductToWarehouseValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Amount must be greater than 0");
    }
}