using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepository departmentRepository { get ; set ; }
        public IEmployeeRepository employeeRepository { get ; set ; }
        public UnitOfWork(IDepartmentRepository departmentRepository,IEmployeeRepository employeeRepository) 
        {
           this. departmentRepository = departmentRepository;
           this. employeeRepository = employeeRepository;
        }
    }
}
