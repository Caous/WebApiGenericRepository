using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiGenericRepository.Infraestructure.Database;
using WebApiGenericRepository.Model;
using WebApiGenericRepository.Repository.Interfaces;

namespace WebApiGenericRepository.Repository.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _appDbContext.Users.Include(d=> d.Departament).ToListAsync();
        }
    }
}
