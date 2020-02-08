using System;
using System.Collections.Generic;
using PeopleManagement.Data.Model;

namespace PeopleManagement.Data.Services
{
    public interface IPeopleService
    {
        void Delete(Guid id);
        List<Person> GetAll();
        Person GetById(Guid id);
        void Insert(Person person);
        void Update(Person person);
    }
}