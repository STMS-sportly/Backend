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

        public bool CreateEvent(CreateEventDTO newEvent)
        {
            var t = new Event()
            {
                Description = newEvent.Description,
                EventDate = newEvent.eventDate,
                EventName = newEvent.Title,
                TeamId = newEvent.TeamId
            };
            return scheduleRepo.CreateEvent(t);
        }
    }
}
