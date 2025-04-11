using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Projector.Data;
using Projector.Models.Entities;
using Projector.Models.InputModels;
using Projector.Models.ViewModels;
using Projector.Models.OutputModels;
using System.Text.RegularExpressions;

namespace Projector.Models.Services
{

    public class ProjectService
    {
        private readonly ProjectorDbContext _context;
        public ProjectService(ProjectorDbContext context)
        {
            _context = context;
        }
        public async Task<ProjectsViewModel> GetProjectsViewModelAsync()
        {
            var projects = await _context.Projects
            .Select(p => new ProjectItemDTO
            {
                Id = p.Id,
                Name = p.Name,
                Budget = p.Budget
            }).ToListAsync();

            if (!projects.Any())
            {
                return null;
            }

            return new ProjectsViewModel { Projects = projects };
        }
        public async Task<CommandResult.WithData<ProjectItemDTO>> CreateNewProjectAsync(CreateOrEditProjectInputModel args)
        {
            if (!Regex.IsMatch(args.Budget.ToString(), @"^\d{1,18}(\.\d{0,4})?$"))
            {
                return CommandResult.WithData<ProjectItemDTO>.Error<ProjectItemDTO>("Value exceeds budget limits.");
            }

            var project = _context.Projects.FirstOrDefault(p => p.Code == args.Code);
            if (project == null)
            {
                project = new Project { Name = args.Name, Code = args.Code, Budget = args.Budget, Remarks = args.Remarks, Currency = args.SelectedCurrency };
                _context.Add(project);
                await _context.SaveChangesAsync();
                return CommandResult.WithData<ProjectItemDTO>.Success<ProjectItemDTO>( new ProjectItemDTO { Id = project.Id});
            }
            return CommandResult.WithData<ProjectItemDTO>.Error<ProjectItemDTO>("Project already exists.");

        }
        public async Task<CommandResult.WithData<ProjectItemDTO>> EditProjectAsync(int id, CreateOrEditProjectInputModel args)
        {
            if (!Regex.IsMatch(args.Budget.ToString(), @"^\d{1,18}(\.\d{0,4})?$"))
            {
                return CommandResult.WithData<ProjectItemDTO>.Error<ProjectItemDTO>("Value exceeds budget limits.");
            }

            var project = _context.Projects.FirstOrDefault(p => p.Id == id);

            if (project != null)
            {
                project.Name = args.Name;
                project.Code = args.Code;
                project.Budget = args.Budget;
                project.Remarks = args.Remarks;
                await _context.SaveChangesAsync();
                return CommandResult.WithData<ProjectItemDTO>.Success<ProjectItemDTO>(new ProjectItemDTO { Id = project.Id });
            }
            return CommandResult.WithData<ProjectItemDTO>.Error<ProjectItemDTO>("Project not found.");

        }
        public async Task<CreateOrEditProjectInputModel> GetProjectByIdAsync(int projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }
            return new CreateOrEditProjectInputModel 
            {
                Code = project.Code,
                Name = project.Name,
                Budget = project.Budget,
                Remarks = project.Remarks,
                SelectedCurrency = project.Currency
            };
        }
        public async Task<ProjectDetailsViewModel> GetProjectAndMembersByIdAsync(int projectId)
        {
            var project = await _context.Projects.Select(p => new ProjectDetailsDTO
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Budget = p.Budget,
                Remarks = p.Remarks,
                Members = p.Persons
                .Select(person => new PersonDTO { 
                    Id = person.Id, 
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Username = person.UserName,
                }).ToList()
            }).FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }
            return new ProjectDetailsViewModel
            {
                Code = project.Code,
                Name = project.Name,
                Budget = project.Budget,
                Remarks = project.Remarks,
                Members = project.Members
                .Select(member => new ProjectMembersViewModel
                {
                    FullName = $"{member.FirstName} {member.LastName}"

                }).ToList()
            };
        }
       
    }
}
