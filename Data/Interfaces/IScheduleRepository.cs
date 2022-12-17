using Data.DataTO;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IScheduleRepository
    {
        bool CreateEvent(string email, Event t);
        List<Event> GetMonthEvents(int teamId, DateTime date);
        List<DayEventTO> GetDayEvents(string email, int teamId, DateTime date);
    }
}
