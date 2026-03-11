using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Infrastructure.Identity.Models;

namespace WarehouseManagementWeb.Api.Validators.IdentityUser
{
    /// <summary>
    /// Класс валидатора аутентификации пользователя.
    /// </summary>
    public class SignInUserValidator : AbstractValidator<UserSignInInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public SignInUserValidator()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_EMAIL)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_EMAIL)
                .EmailAddress().WithMessage(ValidationConst.INVALID_USER_EMAIL_FORMAT);

            RuleFor(x => x.Password)
               .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_PASSWORD)
               .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_PASSWORD);

        }
    }
}