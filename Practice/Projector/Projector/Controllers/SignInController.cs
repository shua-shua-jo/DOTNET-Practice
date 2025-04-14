using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projector.Models.ViewModels;
using Projector.Models.InputModels;
using Projector.Models.Services;

namespace Projector.Controllers
{
    public class SignInController : Controller
    {
        private readonly SignInService _signInService;

        private readonly ILogger<SignInController> _logger;

        public SignInController(SignInService signInService, ILogger<SignInController> logger)
        {
            _signInService = signInService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Projects");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SignInInputModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await _signInService.AuthenticateUserAsync(model.Username, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("InvSign", "Invalid login attempt.");
                return View(model);
            }
            var principal = _signInService.CreateClaimsPrincipalAsync(user.FirstName, user.UserName);
            var authProperties = _signInService.CreateAuthProperties();

            await _signInService.SignInAsync(HttpContext, principal, authProperties);
            return RedirectToAction("Index", "Projects");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "SignIn");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeactivateAccount()
        {
            var username = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest(new { success = false, message = "Not found." });
            }

            var result = await _signInService.DeactivateAccountAsync(username);
            if (result.IsSuccessful)
            {
                await HttpContext.SignOutAsync("CookieAuth");
                return Ok(new { success = true });
            }

            return BadRequest(new { success = false, message = result.Errors[0] });
        }

        [Authorize]
        public IActionResult Account()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
