using Project1.Data;
using Project1.Interface;
using Project1.Models;

namespace Project1.Repository
{
    public class TrainDriverRepository : ITrainDriverRepository
    {
        private readonly DataContext _context;
        public TrainDriverRepository(DataContext context) 
        {
            _context = context;
        }

        public TrainDriver GetDriver(int id)
        {
            return _context.TrainDrivers.Where(d=>d.Id==id).FirstOrDefault();
            //throw new NotImplementedException();
        }

        public TrainDriver GetDriver(string name)
        {
            return _context.TrainDrivers.Where(d => d.Name == name).FirstOrDefault();
            //throw new NotImplementedException();
        }
        public ICollection<TrainDriver> GetDrivers() 
        {
            return _context.TrainDrivers.OrderBy(d=>d.Id).ToList();
        }

        public bool TrainDriverExists(int drivid)
        {
            return _context.TrainDrivers.Any(d => d.Id == drivid);
            //throw new NotImplementedException();
        }
    }
}
