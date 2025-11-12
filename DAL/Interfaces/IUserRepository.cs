using SmartLedger.DAL.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartLedger.DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
