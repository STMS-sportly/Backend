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
        private readonly StmsContext _appContext;

        public UnitOfWork(StmsContext appContext)
        {
            _appContext = appContext;
            Users = new UserRepository(_appContext);
            Teams = new TeamRepository(_appContext);
        }

        public IUserRepository Users { get; set; }
        public ITeamRepository Teams { get; set; }

        public int Complete()
        {
            return _appContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _appContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                _appContext.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
