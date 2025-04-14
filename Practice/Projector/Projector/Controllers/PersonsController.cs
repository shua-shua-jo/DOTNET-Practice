using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projector.Models.InputModels;
using Projector.Models.OutputModels;
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

        public async Task<IActionResult> Index()
        {
            var persons = await _personService.GetAllPersonsAsync();
            return View(persons);
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
                ModelState.AddModelError(string.Empty, result.Errors[0]);
                
                return View(model);
            }
            return RedirectToAction("Index", "Persons");
        }
    }
}
