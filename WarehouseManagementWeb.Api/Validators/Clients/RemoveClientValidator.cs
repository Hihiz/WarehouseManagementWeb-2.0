using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;

namespace WarehouseManagementWeb.Api.Validators.Clients
{
    /// <summary>
    /// Класс валидатора удаления клиента.
    /// </summary>
    public class RemoveClientValidator : AbstractValidator<int>
    {
        public RemoveClientValidator()
        {
            RuleFor(с => с)
                .Must(с => с > 0).WithMessage(ValidationConst.NOT_VALID_CLIENT_ID);
        }
    }
}
