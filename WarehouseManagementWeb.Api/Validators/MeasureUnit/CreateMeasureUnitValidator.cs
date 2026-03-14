using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Application.Dto.Input.MeasureUnit;

namespace WarehouseManagementWeb.Api.Validators.MeasureUnit
{
    /// <summary>
    /// Класс валидатора создания единицы измерения.
    /// </summary>
    public class CreateMeasureUnitValidator : AbstractValidator<CreateMeasureUnitInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public CreateMeasureUnitValidator()
        {
            RuleFor(mu => mu.Title)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_MEASURE_UNIT_TITLE)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_MEASURE_UNIT_TITLE)
                .MinimumLength(ValidationConst.MIN_MEASURE_UNIT_TITLE_LENGTH)
                    .WithMessage(ValidationConst.MINIMUM_LENGTH_MEASURE_UNIT_TITLE)
                .MaximumLength(ValidationConst.MAX_MEASURE_UNIT_TITLE_LENGTH)
                    .WithMessage(ValidationConst.MAXIMUM_LENGTH_MEASURE_UNIT_TITLE);
        }
    }
}
