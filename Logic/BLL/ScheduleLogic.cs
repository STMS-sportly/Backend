using Data.DataAccess;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Logic.ALL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.BLL
{
    public class ScheduleLogic
    {
        private readonly IScheduleRepository scheduleRepo;

        public ScheduleLogic(StmsContext context)
        {
            scheduleRepo = new ScheduleRepository(context);
        }

        public bool CreateEvent(string email,int teamId, CreateEventDTO newEvent)
        {
            var t = new Event()
            {
                Description = newEvent.Description,
                EventDate = newEvent.eventDate,
                EventName = newEvent.Title,
                TeamId = teamId,
            };
            return scheduleRepo.CreateEvent(email, t);
        }

        public List<GetMonthEventsDTO> GetMonthEvents(int teamId, DateTime date)
        {
            var res = new List<GetMonthEventsDTO>();
            var events = scheduleRepo.GetMonthEvents(teamId, date);
            foreach(var item in events)
            {
                res.Add(new GetMonthEventsDTO()
                {
                    Date = item.EventDate,
                    Title = item.EventName
                });
            }

            return res;
        }

        public List<GetDayEventsDTO> GetDayEvents(string email,int teamId, DateTime date)
        {
            var res = new List<GetDayEventsDTO>();
            var events = scheduleRepo.GetDayEvents(email, teamId, date);
            foreach (var item in events)
            {
                res.Add(new GetDayEventsDTO()
                {
                    Date = item.Date,
                    Title = item.Title,
                    Description = item.Description,
                    Editable = item.Editable
                });
            }

            return res;
        }
    }
}
