using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Infraestructure.Database;
using WebApiGenericRepository.Model;
using WebApiGenericRepository.Repository.Generic;
using WebApiGenericRepository.Repository.Interfaces;

namespace WebApiGenericRepository.Repository.Services
{
    public class DepartamentRepository : GenericRepository<Departament>, IDepartamentRepository
    {
        private readonly AppDbContext _appDbContext;

        public DepartamentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
