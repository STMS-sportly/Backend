using Data.DataAccess;
using Data.DTOs;
using Data.Interfaces;
using Data.Models;
using Data.Repositories;
using Microsoft.Extensions.Logging;
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
                    EventId= item.EventId,
                    Date = item.EventDate,
                    Title = item.EventName
                });
            }

            res.OrderBy(e => e.Date);
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
                    EventId = item.Key.EventId,
                    Date = item.Key.EventDate,
                    Title = item.Key.EventName,
                    Description = item.Key.Description,
                    Editable = item.Value
                });
            }

            res.OrderBy(e => e.Date);
            return res;
        }

        public bool RemoveEvent(int eventId, int teamId)
        {
            return scheduleRepo.RemoveEvent(eventId, teamId);
        }

        public bool UpdatedEvent(int eventId, int teamId, UpdatedEventDTO updatedEvent)
        {
            var newEvent = new Event()
            {
                EventId = eventId,
                EventName = updatedEvent.Title,
                Description = updatedEvent.Description,
                EventDate = updatedEvent.Date,
                TeamId = teamId
            };
            return scheduleRepo.UpdatedEvent(eventId, teamId, newEvent);
        }
    }
}
