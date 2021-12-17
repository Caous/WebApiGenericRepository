using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiGenericRepository.Repository.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> FindById(int Id);
        Task<T> Create(T item);
        Task<T> Update(T item);
        Task<bool> Delete(int Id);
        Task<T> FindByName(string name);
    }
}
