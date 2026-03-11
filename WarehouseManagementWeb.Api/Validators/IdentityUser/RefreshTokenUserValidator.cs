using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Infrastructure.Identity.Models;

namespace WarehouseManagementWeb.Api.Validators.IdentityUser
{
    /// <summary>
    /// Класс валидатора обновления токена пользователя.
    /// </summary>
    public class RefreshTokenUserValidator : AbstractValidator<TokenInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public RefreshTokenUserValidator()
        {
            RuleFor(t => t.AccessToken)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_ACCESS_TOKEN)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_ACCESS_TOKEN);

            RuleFor(t => t.RefreshToken)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_REFRESH_TOKEN)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_REFRESH_TOKEN);
        }
    }
}