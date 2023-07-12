using Project1.Data;
using Project1.Interface;
using Project1.Models;

namespace Project1.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataContext _context;
        public ScheduleRepository(DataContext context)
        {
            _context = context;
        }

        public Schedule GetSchedule(int id)
        {
            return _context.Schedules.Where(s => s.Id == id).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public ICollection<Schedule> GetSchedules()
        {
            return _context.Schedules.OrderBy(s => s.Id).ToList();
        }

        public bool ScheduleExists(int sid)
        {
            return _context.Schedules.Any(s => s.Id == sid);
            //throw new NotImplementedException();
        }


        public bool CreateSchedule(Schedule schedule)
        {
            _context.Add(schedule);
            return Save();
        }

        public bool UpdateSchedule(Schedule schedule)
        {
            _context.Update(schedule);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
