using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;

namespace WarehouseManagementWeb.Api.Validators.MeasureUnit
{
    /// <summary>
    /// Класс валидатора удаления единицы измерения.
    /// </summary>
    public class RemoveMeasureUnitValidator : AbstractValidator<int>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public RemoveMeasureUnitValidator()
        {
            RuleFor(mu => mu)
                .Must(mu => mu > 0).WithMessage(ValidationConst.NOT_VALID_MEASURE_UNIT_ID);
        }
    }
}
