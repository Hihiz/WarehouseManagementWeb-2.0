using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Application.Dto.Input.Resource;

namespace WarehouseManagementWeb.Api.Validators.Resources
{
    /// <summary>
    /// Класс валидатора создания ресурса.
    /// </summary>
    public class CreateResourceValidator : AbstractValidator<CreateResourceInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public CreateResourceValidator()
        {
            RuleFor(r => r.Title)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_RESOURCE_TITLE)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_RESOURCE_TITLE)
                .MinimumLength(ValidationConst.MIN_RESOURCE_TITLE_LENGTH)
                    .WithMessage(ValidationConst.MINIMUM_LENGTH_RESOURCE_TITLE)
                .MaximumLength(ValidationConst.MAX_RESOURCE_TITLE_LENGTH)
                    .WithMessage(ValidationConst.MAXIMUM_LENGTH_RESOURCE_TITLE);
        }
    }
}
