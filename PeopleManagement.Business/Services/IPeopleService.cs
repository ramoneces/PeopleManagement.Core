using System;
using System.Collections.Generic;
using PeopleManagement.Business.Dtos;

namespace PeopleManagement.Business.Services
{
    public interface IPeopleService
    {
        void Delete(Guid id);
        IEnumerable<PersonDto> GetAll();
        PersonDto GetById(Guid id);
        void Insert(PersonDto person);
        void Update(PersonDto person);
    }
}