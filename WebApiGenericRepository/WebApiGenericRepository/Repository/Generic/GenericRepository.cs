using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Infraestructure.Database;

namespace WebApiGenericRepository.Repository.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<T> Create(T item)
        {
            await _appDbContext.Set<T>().AddAsync(item);
            await _appDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<bool> Delete(int Id)
        {
            T item = await _appDbContext.Set<T>().FindAsync(Id);
            _appDbContext.Set<T>().Remove(item);
            var result = await _appDbContext.SaveChangesAsync();

            return true;

        }

        public async Task<T> FindById(int Id)
        {
            return await _appDbContext.Set<T>().FindAsync(Id);
        }

        public async Task<T> FindByName(string name)
        {
            //return await _appDbContext.Set<T>().Where(x=> x.Equals(name));
            return null;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> Update(T item)
        {
            _appDbContext.Set<T>().Update(item);
            await _appDbContext.SaveChangesAsync();
            return item;
        }
    }
}
