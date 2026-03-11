using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Infrastructure.Identity.Models;

namespace WarehouseManagementWeb.Api.Validators.IdentityUser
{
    /// <summary>
    /// Класс валидатора регистрации пользователя.
    /// </summary>
    public class SignUpUserValidator : AbstractValidator<UserSignUpInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public SignUpUserValidator()
        {
            RuleFor(x => x.FirstName)
              .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_FIRST_NAME)
              .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_FIRST_NAME)
              .MinimumLength(3).WithMessage(ValidationConst.MINIMUM_LENGTH_USER_FIRST_NAME)
              .MaximumLength(20).WithMessage(ValidationConst.MAXIMUM_LENGTH_USER_FIRST_NAME);

            RuleFor(x => x.LastName)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_LAST_NAME)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_LAST_NAME)
                .MinimumLength(3).WithMessage(ValidationConst.MINIMUM_LENGTH_USER_LAST_NAME)
                .MaximumLength(20).WithMessage(ValidationConst.MAXIMUM_LENGTH_USER_LAST_NAME);

            RuleFor(x => x.Email)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_EMAIL)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_EMAIL)
                .EmailAddress().WithMessage(ValidationConst.INVALID_USER_EMAIL_FORMAT);

            RuleFor(x => x.Password)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_PASSWORD)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_PASSWORD);

            RuleFor(x => x.PasswordConfirm)
              .NotNull().WithMessage(ValidationConst.NOT_VALID_USER_PASSWORD_CONFIRM)
              .NotEmpty().WithMessage(ValidationConst.EMPTY_USER_PASSWORD_CONFIRM);
              
            RuleFor(x => x)
                .Must(x => x.Password == x.PasswordConfirm).WithMessage(ValidationConst.USER_PASSWORDS_NOT_MATCH);
        }
    }
}
