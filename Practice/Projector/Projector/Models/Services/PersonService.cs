﻿using System.Text.RegularExpressions;
using Projector.Data;
using Projector.Models.Entities;
using Projector.Models.InputModels;
using Projector.Models.OutputModels;

namespace Projector.Models.Services
{
    public class PersonService
    {
        private readonly ProjectorDbContext _context;
        public PersonService(ProjectorDbContext context)
        {
            _context = context;
        }
        public async Task<CommandResult> CreateNewPersonAsync(CreateNewPersonInputModel args)
        { 
            var person = _context.Persons.FirstOrDefault(p => p.UserName == args.UserName);
            if (person == null)
            {
                person = new Person { UserName = args.UserName, FirstName = args.FirstName, LastName = args.LastName, Password = args.Password };
                _context.Add(person);
                await _context.SaveChangesAsync();
                return CommandResult.Success();
            }
            return CommandResult.Error("Username already exists.");

        }
    }
}
