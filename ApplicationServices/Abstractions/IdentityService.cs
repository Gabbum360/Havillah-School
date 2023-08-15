using ApplicationServices.Common.Model;
using ApplicationServices.Entities;
using ApplicationServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Abstractions
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<IdentityService> _logger;
        public IdentityService(UserManager<ApplicationUser> userManager, IConfiguration configuration, ILogger<IdentityService> logger)
        {
            _userManager = userManager;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<ErrorResult<ApplicationUser>> ValidateApplicationUser(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null) return ErrorResult.Fail<ApplicationUser>("username or password invalid");
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid) return ErrorResult.Fail<ApplicationUser>("username or password invalid");
            return ErrorResult.Ok(user);

        }
        public async Task<ErrorResult> CreateUserAsync(Guid id, string firstName, string lastName, string email, string password)
        {
            var doesUserExist = await _userManager.FindByNameAsync(email);
            if (doesUserExist != null) return ErrorResult.Fail("Duplicate user"); ApplicationUser.Factory.Create();
            var user = ApplicationUser.Factory.Create(id, firstName, lastName, email);
            var result = await _userManager.CreateAsync(user, password);
            if (result == null)
            {
                _logger.LogError("Unable to register user for some reason in IdentityService.CreateUserAsync method");
                throw new Exception();
            }
            if (result.Succeeded)
            {
                return ErrorResult.Ok("registration succeeded!");
            }
            var errorArray = result.Errors.Select(x => x.Description).ToArray();
            var error = string.Join(";", errorArray);
            return ErrorResult.Fail(error);
        }
    }
}
