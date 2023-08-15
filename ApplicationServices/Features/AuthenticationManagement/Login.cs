using ApplicationServices.Common.Model;
using ApplicationServices.DataStorage;
using ApplicationServices.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.AuthenticationManagement
{
    public record LoginCommand(string Username, string Password) : IRequest<ErrorResult>;
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ErrorResult>
    {
        private readonly SMDatabaseContext _dbContext;
        private readonly IIdentityService _identityService;
        private readonly IConfiguration _configuration;
        public LoginCommandHandler(SMDatabaseContext dbContext, IConfiguration configuration, IIdentityService identityService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _identityService = identityService;
        }

        public async Task<ErrorResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _identityService.ValidateApplicationUser(request.Username, request.Password);
                if (!result.IsSuccess) return ErrorResult.Fail<string>(result.Message, result.Message, "400");
                return  ErrorResult.Ok<string>("login details c", "", "200");
            }
            catch (Exception)
            {
                return ErrorResult.Fail<string>("An error occured", "500");
            }
        }
    }
}
