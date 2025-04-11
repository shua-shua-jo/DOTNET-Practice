using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projector.Models.ViewModels;
using Projector.Models.InputModels;
using Projector.Models.Services;

namespace Projector.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly ProjectService _projectService;
        private readonly ProjectAssignmentService _projectAssignmentService;

        public ProjectsController(ProjectService projectService, ProjectAssignmentService projectAssignmentService)
        {
            _projectService = projectService;
            _projectAssignmentService = projectAssignmentService;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = await _projectService.GetProjectsViewModelAsync();

            return View(viewModel); 
        }
        public IActionResult Create()
        {
            return View(new CreateOrEditProjectInputModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrEditProjectInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("ProjInp", "Invalid input. Please fill in the correct details.");
                return View(model);
            }
            var result = await _projectService.CreateNewProjectAsync(model);
            if (!result.IsSuccessful)
            {
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    ModelState.AddModelError(string.Empty, result.Errors[i]);
                }
                return View(model);
            }
            return RedirectToAction("Details", "Projects", new {id = result.Data.Id});
        }
        public async Task<IActionResult> Assignment(int projectId)
        {
            var viewModel = await _projectAssignmentService.GetAssignmentData(projectId);
            if (viewModel == null)
            {
                ModelState.AddModelError(string.Empty, "Error assigning members to project.");
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _projectService.GetProjectAndMembersByIdAsync(id);
            if (viewModel == null)
            {
                ModelState.AddModelError(string.Empty, "Error getting project details.");
                return View("Index");
            }

            return View(viewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _projectService.GetProjectByIdAsync(id);
            if (viewModel == null)
            {
                ModelState.AddModelError(string.Empty, "Error editing project.");
                return View("Index");
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CreateOrEditProjectInputModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid input. Please fill in the correct details.");
                return View(model);
            }
            var result = await _projectService.EditProjectAsync(id, model);
            if (!result.IsSuccessful)
            {
                for (int i = 0; i < result.Errors.Count; i++)
                {
                    ModelState.AddModelError(string.Empty, result.Errors[i]);
                }
                return View(model);
            }
            return RedirectToAction("Details", "Projects", new { id = result.Data.Id });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
