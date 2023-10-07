using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IDepartmentRepository departmentRepository { get; set; }
        public IEmployeeRepository employeeRepository { get; set; }
    }
}
