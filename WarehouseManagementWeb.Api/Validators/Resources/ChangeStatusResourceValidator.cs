using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Application.Dto.Input.Resource;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Api.Validators.Resources
{
    /// <summary>
    /// Класс валидатора обновления статуса ресурса.
    /// </summary>
    public class ChangeStatusResourceValidator : AbstractValidator<ChangeStatusResourceInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ChangeStatusResourceValidator()
        {
            RuleFor(r => r.ResourceId)
               .Must(r => r > 0).WithMessage(ValidationConst.NOT_VALID_RESOURCE_ID);

            RuleFor(r => r.ResourceStatusEnum)
                 .IsInEnum()
                 .NotEqual(DirectoryStatusEnum.Undefined).WithMessage(ValidationConst.NOT_VALID_RESOURCE_STATUS);
        }
    }
}
