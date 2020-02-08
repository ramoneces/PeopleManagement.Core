using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PeopleManagement.Data.Exceptions;
using PeopleManagement.Data.Model;
using PeopleManagement.Data.Context;

namespace PeopleManagement.Data.Services
{
    public class PeopleService : IPeopleService
    {
        ApplicationDbContext _context;

        public PeopleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Insert(Person person)
        {
            person.Id = Guid.NewGuid();
            _context.Add(person);
        }

        public Person GetById(Guid id)
        {
            var person = _context.People.First(p => p.Id == id);

            if (person == null)
            {
                throw new EntityNotFoundException("Person", person.Id);
            }

            return person;
        }

        public void Delete(Guid id)
        {
            var person = GetById(id);
            _context.People.Remove(person);
        }


        public List<Person> GetAll()
        {
            return _context.People.ToList();
        }

        public void Update(Person person)
        {
            try
            {
                _context.Update(person);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.People.Any(e => e.Id == person.Id))
                {
                    throw new EntityNotFoundException("Person", person.Id);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
