using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private readonly STMSContext _userContext;

        public UnitOfWork(STMSContext userContext)
        {
            _userContext = userContext;
            Users = new UserRepository(userContext);
        }

        public IUserRepository Users { get; set; }

        public int Complete()
        {
            return _userContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _userContext.SaveChangesAsync();
        }



        public void Dispose()
        {
            _userContext.Dispose();
        }
    }
}
