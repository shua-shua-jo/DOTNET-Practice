using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projector.Models.ViewModels;
using Projector.Models.InputModels;
using Projector.Models.OutputModels;
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
            var projects = await _projectService.GetProjectsAsync();
            return View(ProjectsViewModel.FromDTO(projects)); 
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
            var (project, notMembers) = await _projectAssignmentService.GetAssignmentDataAsync(projectId);
            if (project == null || notMembers == null)
            {
                ModelState.AddModelError(string.Empty, "Error assigning members to project.");
                return RedirectToAction("Index");
            }
            return View(AssignmentViewModel.FromProject(project, notMembers));
        }

        public async Task<IActionResult> Details(int id)
        {
            var projectDetails = await _projectService.GetProjectDetailsAsync(id);
            if (projectDetails == null)
            {
                ModelState.AddModelError(string.Empty, "Error getting project details.");
                return View("Index");
            }
            return View(ProjectDetailsViewModel.FromDTO(projectDetails));
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
