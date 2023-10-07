using DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGenericRepository<T>
    {

        Task<int> Create(T T);
        Task<int> Update(T T);
        Task<int> Delete(T T);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int? Id);
        Task<IEnumerable<T>> SearchName(string name);
    }
}
