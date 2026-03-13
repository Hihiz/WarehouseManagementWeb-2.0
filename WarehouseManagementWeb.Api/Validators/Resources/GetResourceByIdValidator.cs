using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;

namespace WarehouseManagementWeb.Api.Validators.Resources
{
    /// <summary>
    /// Класс валидатора получения деталей ресурса.
    /// </summary>
    public class GetResourceByIdValidator : AbstractValidator<int>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public GetResourceByIdValidator()
        {
            RuleFor(r => r)
                .Must(r => r > 0).WithMessage(ValidationConst.NOT_VALID_RESOURCE_ID);
        }
    }
}
