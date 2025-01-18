using Microsoft.AspNetCore.Identity;
using Nano_Health.Models;

namespace Nano_Health.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user, UserManager<User> userManager);
    }
}
