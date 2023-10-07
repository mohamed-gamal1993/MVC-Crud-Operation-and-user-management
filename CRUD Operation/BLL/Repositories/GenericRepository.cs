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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Create(T item)
        {
            context.Set<T>().Add(item);
            return await context.SaveChangesAsync();
        }
        public async Task<int> Update(T item)
        {
            context.Set<T>().Update(item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T item)
        {
            context.Set<T>().Remove(item);
            return await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)await context.Set<Employee>().Include(e => e.Department).ToListAsync();
            return await context.Set<T>().ToListAsync();
        }


        public async Task<T> Get(int? Id)
        => await context.Set<T>().FindAsync(Id);


        public async Task<IEnumerable<T>> SearchName(string name)
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await context.Set<Employee>().Where(e => e.Name.Contains(name)).ToListAsync();
            }
            else
            {
                return (IEnumerable<T>)await context.Set<Department>().Where(e => e.Name.Contains(name)).ToListAsync();
            }

            return await context.Set<T>().ToListAsync();

        }
    }
}
