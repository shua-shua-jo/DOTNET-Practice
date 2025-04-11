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

        public async Task<Person> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.Persons
                .FirstOrDefaultAsync(u => u.UserName == username);

            // Return null if user not found or password doesn't match
            if (user == null || user.Password != password)
            {
                return null;
            }

            return user;

        }
        public ClaimsPrincipal CreateClaimsPrincipalAsync(string firstName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, firstName)
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

    }
}
