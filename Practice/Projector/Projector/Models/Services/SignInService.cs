using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Projector.Data;
using Projector.Models.Entities;

namespace Projector.Models.Services
{
    public class SignInService
    {
        private readonly ProjectorDbContext _context;

        public SignInService(ProjectorDbContext context)
        {
            _context = context;
        }

        public async Task<Person?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Persons
                .Where(u => u.UserName == username)
                .FirstOrDefaultAsync();

            // Return null if user not found, password doesn't match, or account is deactivated
            if (user == null || user.Password != password || !user.IsActive)
            {
                return null;
            }

            return user;
        }

        public ClaimsPrincipal CreateClaimsPrincipalAsync(string firstName, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, firstName),
                new Claim(ClaimTypes.Email, email)
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "CookieAuth"));
        }

        public AuthenticationProperties CreateAuthProperties(bool isPersistent = false)
        {
            return new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
                IsPersistent = isPersistent
            };
        }
        public async Task SignInAsync(HttpContext httpContext, ClaimsPrincipal principal, AuthenticationProperties properties)
        {
            await httpContext.SignInAsync("CookieAuth", principal, properties);
        }

        public async Task<CommandResult> DeactivateAccountAsync(string username)
        {
            var user = await _context.Persons
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return CommandResult.Error("Error: User not found.");
            }

            user.IsActive = false;
            await _context.SaveChangesAsync();
            return CommandResult.Success();
        }
    }
}
