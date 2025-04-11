﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projector.Models.Services;

namespace Projector.Controllers
{
    [Authorize]
    [ApiController]
    [Route("projects/{projectId}/assignments")]
    public class ProjectAssignmentController : Controller
    {
        private readonly ProjectAssignmentService _assignmentService;
        public ProjectAssignmentController(ProjectAssignmentService projectAssignmentService)
        {
            _assignmentService = projectAssignmentService;
        }

        [HttpPost("assign/{personId}")]
        public async Task<IActionResult> AssignPerson(int projectId, int personId)
        {
            var result = await _assignmentService.AssignPersonToProjectAsync(projectId, personId);
            if (result.IsSuccessful)
            {
                var person = await _assignmentService.GetPersonDataAsync(personId);
                if (person.IsSuccessful)
                {
                    return Ok(new
                    {
                        success = true,
                        person = new
                        {
                            id = person.Data.Id,
                            firstName = person.Data.FirstName,
                            lastName = person.Data.LastName,
                            userName = person.Data.Username
                        }
                    });
                }
            }
            return Json(new { success = false, message = "Error assigning the person to the project." });
        }
        [HttpPost("unassign/{personId}")]
        public async Task<IActionResult> RemovePerson(int projectId, int personId)
        {
            var result = await _assignmentService.RemovePersonToProjectAsync(projectId, personId);
            if (result.IsSuccessful)
            {
                var person = await _assignmentService.GetPersonDataAsync(personId);
                if (person.IsSuccessful)
                {
                    return Ok(new
                    {
                        success = true,
                        person = new
                        {
                            id = person.Data.Id,
                            firstName = person.Data.FirstName,
                            lastName = person.Data.LastName,
                            userName = person.Data.Username
                        }
                    });
                }
            }
            return Json(new { success = false, message = "Error removing the person to the project." });
        }
    }
}
