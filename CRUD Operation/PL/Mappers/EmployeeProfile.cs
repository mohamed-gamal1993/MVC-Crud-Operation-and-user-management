using AutoMapper;
using DAL.Entites;
using PL.Models;

namespace PL.Mappers
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel,Employee>().ReverseMap();
        }
    }
}
