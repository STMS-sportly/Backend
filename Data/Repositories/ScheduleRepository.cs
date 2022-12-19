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

        public bool CreateEvent(string email, Event t)
        {
            try
            {
                var user = scheduleContext.Users.Where(u => u.Email == email).FirstOrDefault();
                if (user == null)
                    return false;

                t.CreatorId = user.UserId;
                scheduleContext.Events.Add(t);
                Save();
                return true;
            }
            catch
            { 
                return false; 
            }
        }

        public List<Event> GetMonthEvents(int teamId, DateTime date)
        {
            var res = scheduleContext.Events.Where(e => e.EventDate.Month == date.Month && e.TeamId == teamId).ToList();
            return res;
        }

        public Dictionary<Event, bool> GetDayEvents(string email, int teamId, DateTime date)
        {
            var res = (from e in scheduleContext.Events
                       join t in scheduleContext.Teams on e.TeamId equals t.TeamId
                       join u in scheduleContext.UsersTeams on e.TeamId equals u.TeamId
                       join us in scheduleContext.Users on u.UserId equals us.UserId
                       where e.TeamId == teamId && us.Email == email && e.EventDate.Day == date.Day
                       select new
                       {
                           Event = new Event
                           {
                               EventId = e.EventId,
                               EventDate = e.EventDate,
                               Description = e.Description,
                               EventName = e.EventName
                           },
                           Editable = (e.CreatorId == us.UserId) || (u.UserType == 0 || u.UserType == 1)
                       }).ToList();

            var resDic = new Dictionary<Event, bool>();
            foreach ( var e in res)
            {
                resDic.Add(e.Event, e.Editable);
            }

            return resDic;
        }

        public bool RemoveEvent(int eventId, int teamId)
        {
            try
            {
                var tmp = scheduleContext.Events.Where(u => u.EventId == eventId && u.TeamId == teamId).FirstOrDefault();
                if (tmp == null)
                    return false;

                scheduleContext.Events.Remove(tmp);
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
