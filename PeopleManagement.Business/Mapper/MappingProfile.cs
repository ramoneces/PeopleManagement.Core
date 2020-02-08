using AutoMapper;
using PeopleManagement.Business.Dtos;
using PeopleManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PeopleManagement.Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
        }
    }
}
