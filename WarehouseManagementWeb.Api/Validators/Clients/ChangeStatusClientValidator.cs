using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Application.Dto.Input.Client;
using WarehouseManagementWeb.Domain.Enums;

namespace WarehouseManagementWeb.Api.Validators.Clients
{
    /// <summary>
    /// Класс валидатора обновления статуса клиента.
    /// </summary>
    public class ChangeStatusClientValidator : AbstractValidator<ChangeStatusClientInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public ChangeStatusClientValidator()
        {
            RuleFor(c => c.ClientId)
               .Must(c => c > 0).WithMessage(ValidationConst.NOT_VALID_CLIENT_ID);

            RuleFor(c => c.ClientStatusEnum)
                .IsInEnum()
                .NotEqual(DirectoryStatusEnum.Undefined).WithMessage(ValidationConst.NOT_VALID_CLIENT_STATUS);
        }
    }
}
