using Microsoft.EntityFrameworkCore;
using Projector.Data;
using Projector.Models.ViewModels;
using Projector.Models.OutputModels;
using Projector.Models.Entities;

namespace Projector.Models.Services
{
    public class ProjectAssignmentService
    {
        private readonly ProjectorDbContext _context;
        public ProjectAssignmentService(ProjectorDbContext context)
        {
            _context = context;
        }
        public async Task<AssignmentViewModel> GetAssignmentData(int projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Persons)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }

            var allPersons = await _context.Persons.ToListAsync();

            var currentMemberIds = project.Persons.Select(m => m.Id).ToList();

            var viewModel = new AssignmentViewModel
            {
                ProjectId = project.Id,
                ProjectName = project.Name,
                CurrMembers = MapToPersonDTO(project.Persons),
                NotMembers = MapToPersonDTO(allPersons
                    .Where(p => !currentMemberIds.Contains(p.Id)))
            };
            return viewModel;
        }
        private List<PersonDTO> MapToPersonDTO(IEnumerable<Person> persons)
        {
            return persons.Select(p => new PersonDTO
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Username = p.UserName,
            }).ToList();
        }

        public async Task<CommandResult> AssignPersonToProjectAsync(int projectId, int personId)
        {
            var project = await _context.Projects
                .Include(p => p.Persons)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            var person = await _context.Persons.FindAsync(personId);

            if (project == null || person == null)
            {
                return CommandResult.Error("Project or Person not found.");
            }
            if (!project.Persons.Any(p => p.Id == personId))
            {
                project.Persons.Add(person);
                await _context.SaveChangesAsync();

                return CommandResult.Success();
            }
            return CommandResult.Error("Person added unsuccessfully to the project");
        }
        public async Task<CommandResult> RemovePersonToProjectAsync(int projectId, int personId)
        {
            var project = await _context.Projects
                .Include(p => p.Persons)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return CommandResult.Error("Project not found.");
            }

            var person = project.Persons.FirstOrDefault(p => p.Id == personId);

            
            if (person != null )
            {
                project.Persons.Remove(person);
                await _context.SaveChangesAsync();

                return CommandResult.Success();
            }
            return CommandResult.Error("Person removed unsuccessfully to the project");
        }
        public async Task<CommandResult.WithData<PersonDTO>> GetPersonDataAsync(int personId)
        {
            var person = await _context.Persons
                .FirstOrDefaultAsync(p => p.Id == personId);

            if (person == null)
            {
                return CommandResult.WithData<PersonDTO>.Error<PersonDTO>("Person not found");
            }

            return CommandResult.WithData<PersonDTO>.Success<PersonDTO>(new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Username = person.UserName,
            });
        }
    }
}
