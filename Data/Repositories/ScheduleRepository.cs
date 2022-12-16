using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly StmsContext scheduleContext;

        public ScheduleRepository(StmsContext teamContext)
        {
            this.scheduleContext = teamContext;
        }

        public void Save()
        {
            scheduleContext.SaveChanges();
        }

        public bool CreateEvent(Event t)
        {
            try
            {
                scheduleContext.Events.Add(t);
                Save();
                return true;
            }
            catch
            { 
                return false; 
            }
        }
    }
}
