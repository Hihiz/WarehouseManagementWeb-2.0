using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Application.Dto.Input.Resource;

namespace WarehouseManagementWeb.Api.Validators.Resources
{
    /// <summary>
    /// Класс валидатора создания ресурса.
    /// </summary>
    public class UpdateResourceValidator : AbstractValidator<UpdateResourceInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public UpdateResourceValidator()
        {
            RuleFor(r => r.Id)
                .Must(r => r > 0).WithMessage(ValidationConst.NOT_VALID_RESOURCE_ID);

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
