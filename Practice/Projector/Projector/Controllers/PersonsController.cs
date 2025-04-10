using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projector.Models.InputModels;
using Projector.Models.Services;

namespace Projector.Controllers
{
    [Authorize]
    public class PersonsController : Controller
    {
        private readonly PersonService _personService;
        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewPersonInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("PerInp", "Invalid input. Please fill in the correct details.");
                return View(model);
            }
            var result = await _personService.CreateNewPersonAsync(model);
            if (!result.IsSuccessful)
            {
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    ModelState.AddModelError(string.Empty, result.Errors[i]);
                }
                return View(model);
            }
            return RedirectToAction("Index", "Projects");
        }
    }
}
