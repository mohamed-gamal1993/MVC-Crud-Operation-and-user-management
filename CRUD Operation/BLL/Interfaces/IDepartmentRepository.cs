using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDepartmentRepository:IGenericRepository<Department>
    {
        //Department GetDepartment(int? Id);
        //IEnumerable<Department> GetAll();
        //int Create(Department department);
        //int Update(Department department);
        //int Delete(Department department);

    }
}
