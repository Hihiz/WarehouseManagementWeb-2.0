using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;

namespace WarehouseManagementWeb.Api.Validators.MeasureUnit
{
    /// <summary>
    /// Класс валидатора получения деталей единицы измерения.
    /// </summary>
    public class GetMeasureUnitByIdValidator : AbstractValidator<int>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public GetMeasureUnitByIdValidator()
        {
            RuleFor(mu => mu)
                .Must(mu => mu > 0).WithMessage(ValidationConst.NOT_VALID_MEASURE_UNIT_ID);
        }
    }
}
