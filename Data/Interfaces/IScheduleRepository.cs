using Data.Models;

namespace Data.Interfaces
{
    public interface IScheduleRepository
    {
        bool CreateEvent(string email, Event t);
        List<Event> GetMonthEvents(int teamId, DateTime date);
        Dictionary<Event, bool> GetDayEvents(string email, int teamId, DateTime date);
        bool RemoveEvent(int eventId, int teamId);
        bool UpdatedEvent(int eventId, int teamId, Event updatedEvent);
    }
}
