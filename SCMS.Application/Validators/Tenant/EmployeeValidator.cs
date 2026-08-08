using FluentValidation;
using scms.Application.Dtos.Tenant;

namespace scms.Application.Validators.Tenant;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(150).When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.PhoneNumber).MaximumLength(20);
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.DepartmentId).NotEmpty();
    }
}

public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployeeDto>
{
    public UpdateEmployeeValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(150).When(x => !string.IsNullOrEmpty(x.Email));
        RuleFor(x => x.PhoneNumber).MaximumLength(20);
        RuleFor(x => x.Type).IsInEnum();
        RuleFor(x => x.DepartmentId).NotEmpty();
    }
}
