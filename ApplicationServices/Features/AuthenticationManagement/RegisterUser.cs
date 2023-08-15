using ApplicationServices.AuthenticationManagement.Events;
using ApplicationServices.Common.Model;
using ApplicationServices.DataStorage;
using ApplicationServices.Entities.Events;
using ApplicationServices.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.AuthenticationManagement;

public record RegistrationCommand(string FirstName, string LastName, string Email, string Password) : IRequest<ErrorResult<string>>;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, ErrorResult<string>>
{
    private readonly IIdentityService _identityService;
    private readonly IPublisher _publisher;
    public RegistrationCommandHandler(IIdentityService identityService,
                                      IPublisher publisher)
    {
        _identityService = identityService;
        _publisher = publisher;
    }

    public async Task<ErrorResult<string>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
    {
        var generatedPassword = new Random(6).ToString();
        Guid userId = Guid.NewGuid();
        var password = string.IsNullOrEmpty(request.Password) ? generatedPassword : request.Password;
        var result = await _identityService.CreateUserAsync(userId, request.FirstName, request.LastName,
            request.Email, password!);
        if (!result.IsSuccess)
        {
            throw new Exception(result.Error);
        }
       /* var (roleId, isCreated) = isUseRoleCreatedResponse.Value;
        if (!isCreated)
        {
            await _identityService.DeleteUserAsync(appuser.Id.ToString());
            throw new Exception(isUseRoleCreatedResponse.Error);
        }

        var admin = await _identityService.GetUserInRoleAsync(Guid.Parse("3e7d9440-48d7-4174-b9c5-0ea5be7d9e7d"));

        //appuser!.Apply(new UserEvents.UserCreated() { Email = appuser.Email, Id = userId });
        if (request.UserType == "Customer")
        {
            _publisher.Publish(new UserRegisteredEvent()
            {
                Email = request.Email,
                Name = $"{request.FirstName} {request.LastName}"
            });

            _publisher.Publish(new NewStudentRegisteredEvent()
            {
                AdminEmail = admin.Email,
                AdminName = $"{admin.FirstName} {admin.LastName}",
                StudentFullName = $"{request.FirstName} {request.LastName}",
                StudentEmailAddress = request.Email,
                StudentPhoneNumber = "",
                DatOfRegistration = DateTime.UtcNow.ToString()
            });
        }
        else
        {
            _publisher.Publish(new AdminRegisteredEvent()
            {
                Email = request.Email,
                Name = $"{request.FirstName} {request.LastName}"
            });
        }*/

        /*await _customerClient.PublishCustomer(new AddCustomerCommand
        {
            Id = userId,
            FirstName = request.FirstName,
            MiddleName = "",
            LastName = request.LastName,
            Dob = "03/05/1977",
            Email = request.Email,
            PhoneNumber = "",
            Sex = "Male",
            RoleId = roleId,
            Address = ""
        });*/
        return ErrorResult.Ok<string>("Registered successfully");
    }
}
