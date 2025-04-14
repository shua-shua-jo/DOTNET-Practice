using Microsoft.EntityFrameworkCore;
using Projector.Data;
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

        public async Task<(Project? Project, List<PersonDTO>? NotMembers)> GetAssignmentDataAsync(int projectId)
        {
            var project = await _context.Projects
                .Include(p => p.Persons)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            
            if (project == null)
            {
                return (null, null);
            }

            var allPersons = await _context.Persons
                .Where(p => p.IsActive)  // Only get active persons
                .ToListAsync();
            var currentMemberIds = project.Persons.Select(m => m.Id).ToList();

            var notMembers = MapToPersonDTO(allPersons.Where(p => !currentMemberIds.Contains(p.Id)));

            return (project, notMembers);
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
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);
            
            var project = await _context.Projects
                .Include(p => p.Persons)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return CommandResult.Error("Project not found.");
            }

            var person = await _context.Persons.FindAsync(personId);
            if (person == null)
            {
                return CommandResult.Error("Person not found.");
            }

            if (project.Persons.Any(p => p.Id == personId))
            {
                return CommandResult.Error("Person is already assigned to this project. Try refreshing this page.");
            }

            project.Persons.Add(person);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return CommandResult.Success();
        }

        public async Task<CommandResult> RemovePersonToProjectAsync(int projectId, int personId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(System.Data.IsolationLevel.RepeatableRead);
            
            var project = await _context.Projects
                .Include(p => p.Persons)
                .FirstOrDefaultAsync(p => p.Id == projectId);

            if (project == null)
            {
                return CommandResult.Error("Project not found.");
            }

            var person = project.Persons.FirstOrDefault(p => p.Id == personId);
            if (person == null)
            {
                return CommandResult.Error("Person is not assigned to this project. Try refreshing this page.");
            }

            project.Persons.Remove(person);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return CommandResult.Success();
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
