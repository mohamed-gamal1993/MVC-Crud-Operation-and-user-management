using AutoMapper;
using DAL.Entites;
using PL.Models;

namespace PL.Mappers
{
    public class DepartmentProfile:Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel,Department>().ReverseMap();
        }
    }
}
