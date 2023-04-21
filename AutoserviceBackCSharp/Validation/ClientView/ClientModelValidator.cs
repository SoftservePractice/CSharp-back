using FluentValidation;
using AutoserviceBackCSharp.Models;

namespace AutoserviceBackCSharp.Validation.ClientView
{
    public class ClientModelValidator : AbstractValidator<ClientViewModel>
    {
        public ClientModelValidator()
        {
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Invalid name field length. Min length is 3 symbols")
            .MaximumLength(50).WithMessage("Invalid name field length. Max length exceeded. Max length is 50 symbols");

            RuleFor(x => x.Phone).Custom((value, context) =>
            {
                UserFieldsValidator uservalidator = new UserFieldsValidator();

                if (!uservalidator.ValidatePhone(value))
                {
                    context.AddFailure("Invalid phone number field");
                }
            });

            RuleFor(x => x.Email).Custom((value, context) =>
            {
                UserFieldsValidator uservalidator = new UserFieldsValidator();

                if (!uservalidator.ValidateEmail(value))
                {
                    context.AddFailure("Invalid email field");
                }
            });
        }
    }
}
