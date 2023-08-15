using ApplicationServices.AuthenticationManagement;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Validations
{
    public class LoginValidationCommand : AbstractValidator<LoginCommand>
    {
        public LoginValidationCommand()
        {
            RuleFor(x => x.Username).NotNull().NotEmpty().NotEqual("string").WithMessage("Username can not be empty");
            RuleFor(x => x.Password).NotNull().NotEmpty().NotEqual("string").WithMessage("Password can not be empty");
        }
    }
}
