using Project1.Models;

namespace Project1.Interface
{
    public interface IScheduleRepository
    {
        ICollection<Schedule> GetSchedules();
        Schedule GetSchedule(int id);
        bool ScheduleExists(int sid);
    }
}
