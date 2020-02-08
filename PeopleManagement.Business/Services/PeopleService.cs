using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PeopleManagement.Business.Dtos;
using PeopleManagement.Data.Context;
using PeopleManagement.Data.Exceptions;
using PeopleManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleManagement.Business.Services
{
    public class PeopleService : IPeopleService
    {
        ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PeopleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Insert(PersonDto PersonDto)
        {
            PersonDto.Id = Guid.NewGuid();
            _context.Add(_mapper.Map<Person>(PersonDto));
        }

        public PersonDto GetById(Guid id)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                throw new EntityNotFoundException("PersonDto", id);
            }

            return _mapper.Map<PersonDto>(person);
        }

        public void Delete(Guid id)
        {
            var person = GetById(id);
            _context.People.Remove(_mapper.Map<Person>(person));
        }


        public IEnumerable<PersonDto> GetAll()
        {
            return _mapper.Map<List<PersonDto>>(_context.People.ToList());
        }

        public void Update(PersonDto person)
        {
            try
            {
                _context.Update(_mapper.Map<Person>(person));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.People.Any(e => e.Id == person.Id))
                {
                    throw new EntityNotFoundException("PersonDto", person.Id);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
