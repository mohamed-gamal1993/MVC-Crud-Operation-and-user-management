using BLL.Interfaces;
using DAL.Context;
using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context):base(context) 
        {

        }
    }
}
