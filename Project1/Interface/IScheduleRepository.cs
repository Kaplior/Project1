using Project1.Models;

namespace Project1.Interface
{
    public interface IScheduleRepository
    {
        ICollection<Schedule> GetSchedules();
        Schedule GetSchedule(int id);
        bool ScheduleExists(int sid);
        bool CreateSchedule(Schedule schedule);
        bool UpdateSchedule(Schedule schedule);
        bool Save();
    }
}
