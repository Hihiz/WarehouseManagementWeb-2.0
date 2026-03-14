using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Application.Dto.Input.MeasureUnit;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Api.Validators.MeasureUnit
{
    /// <summary>
    /// Класс валидатора обновления статуса единицы измерения.
    /// </summary>
    public class ChangeStatusMeasureUnitValidator : AbstractValidator<ChangeStatusMeasureUnitInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ChangeStatusMeasureUnitValidator()
        {
            RuleFor(mu => mu.MeasureUnitId)
                .Must(mu => mu > 0).WithMessage(ValidationConst.NOT_VALID_MEASURE_UNIT_ID);

            RuleFor(mu => mu.MeasureUnitStatusEnum)
                 .IsInEnum()
                 .NotEqual(DirectoryStatusEnum.Undefined).WithMessage(ValidationConst.NOT_VALID_MEASURE_UNIT_STATUS);
        }
    }
} 