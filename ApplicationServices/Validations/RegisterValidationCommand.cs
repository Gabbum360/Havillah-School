using ApplicationServices.AuthenticationManagement;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Validations
{
    public class RegisterValidationCommand : AbstractValidator<RegistrationCommand>
    {
        public RegisterValidationCommand()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().NotEqual("string").WithMessage("FirstName can not be empty");
            RuleFor(x => x.LastName).NotNull().NotEmpty().NotEqual("string").WithMessage("LastName can not be empty");
            RuleFor(x => x.Email).NotNull().NotEmpty().NotEqual("string").WithMessage("Email can not be empty");
            //RuleFor(x => x.UserType).NotNull().NotEmpty().NotEqual("string").WithMessage("Usertype can not be empty");
            RuleFor(x => x.Password).NotNull().NotEmpty().NotEqual("string").WithMessage("Password can not be empty");
        }
    }
}
