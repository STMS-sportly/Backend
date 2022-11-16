using Data.DataAccess;
using Data.Interfaces;
using Data.Models;

namespace Data.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly StmsContext userContext;

        public LogRepository(StmsContext context)
        {
            userContext = context;
        }

        public void InsertLog(Log log)
        {
            userContext.AppLogs?.Add(log);
        }

        public void Save()
        {
            userContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                userContext.Dispose();
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
