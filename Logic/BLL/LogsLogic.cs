using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BLL
{
    public class LogsLogic
    {
        private readonly ILogRepository logsRepo;

        public LogsLogic(StmsContext context)
        {
            logsRepo = new LogRepository(context);
        }

        public void AddLog(string errorMessage, string? source)
        {
            var log = new Log()
            {
                Message = errorMessage + "\t" + (source ?? ""),
                LogTime = DateTime.Now
            };
            logsRepo.InsertLog(log);
            logsRepo.Save();
        }
    }
}
