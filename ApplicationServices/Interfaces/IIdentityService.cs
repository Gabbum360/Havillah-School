using ApplicationServices.Common.Model;
using ApplicationServices.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationServices.Interfaces
{
    public interface IIdentityService
    {
        Task<ErrorResult<ApplicationUser>> ValidateApplicationUser(string username, string password);
        Task<ErrorResult> CreateUserAsync(Guid id, string firstname, string lastname, string email, string password);
    }
}
