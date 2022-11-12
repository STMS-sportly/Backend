using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface ILogRepository : IDisposable
    {
        void InsertLog(Log log);
        void Save();
    }
}
