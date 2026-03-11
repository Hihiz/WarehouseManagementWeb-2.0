using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;

namespace WarehouseManagementWeb.Api.Validators.Clients
{
    /// <summary>
    /// Класс валидатора получения деталей клиента.
    /// </summary>
    public class GetClientByIdValidator : AbstractValidator<int>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public GetClientByIdValidator()
        {
            RuleFor(x => x)
                .Must(x => x > 0).WithMessage(ValidationConst.NOT_VALID_CLIENT_ID);
        }
    }
}
