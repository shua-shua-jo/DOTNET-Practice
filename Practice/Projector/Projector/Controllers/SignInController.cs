using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
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
            if(!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Index", "Projects");
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
            var principal = _signInService.CreateClaimsPrincipalAsync(user.FirstName);
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
