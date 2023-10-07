using BLL.Interfaces;
using DAL.Context;
using DAL.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class EmployeeRepository:GenericRepository<Employee>,IEmployeeRepository
    {
        private readonly ApplicationDbContext context;

        public EmployeeRepository(ApplicationDbContext context) : base(context) 
        {
            this.context = context;
        }

       
       
    }
}
