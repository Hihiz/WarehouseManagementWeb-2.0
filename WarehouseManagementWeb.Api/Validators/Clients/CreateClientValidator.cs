using FluentValidation;
using WarehouseManagementWeb.Api.Validators.Constants;
using WarehouseManagementWeb.Application.Dto.Input.Client;

namespace WarehouseManagementWeb.Api.Validators.Clients
{
    /// <summary>
    /// Класс валидатора создания клиента.
    /// </summary>
    public class CreateClientValidator : AbstractValidator<CreateClientInput>
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        public CreateClientValidator()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_CLIENT_NAME)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_CLIENT_NAME)
                .MinimumLength(ValidationConst.MIN_CLIENT_NAME_LENGTH)
                    .WithMessage(ValidationConst.MINIMUM_LENGTH_CLIENT_NAME)
                .MaximumLength(ValidationConst.MAX_CLIENT_NAME_LENGTH)
                    .WithMessage(ValidationConst.MAXIMUM_LENGTH_CLIENT_NAME);

            RuleFor(c => c.Address)
                .NotNull().WithMessage(ValidationConst.NOT_VALID_CLIENT_ADDRESS)
                .NotEmpty().WithMessage(ValidationConst.EMPTY_CLIENT_ADDRESS)
                .MinimumLength(ValidationConst.MIN_CLIENT_ADDRESS_LENGTH)
                    .WithMessage(ValidationConst.MINIMUM_LENGTH_CLIENT_ADDRESS)
                .MaximumLength(ValidationConst.MAX_CLIENT_ADDRESS_LENGTH)
                    .WithMessage(ValidationConst.MAXIMUM_LENGTH_CLIENT_ADDRESS);
        }
    }
}
