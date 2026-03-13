using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;

namespace WarehouseManagementWeb.Api.Validators.Resources
{
    /// <summary>
    /// Класс валидатора удаления ресурса.
    /// </summary>
    public class RemoveResourceValidator : AbstractValidator<int>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public RemoveResourceValidator()
        {
            RuleFor(r => r)
               .Must(r => r > 0).WithMessage(ValidationConst.NOT_VALID_RESOURCE_ID);
        }
    }
}
