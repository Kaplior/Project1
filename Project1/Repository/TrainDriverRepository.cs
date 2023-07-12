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
        public ICollection<TrainDriver> GetDrivers() 
        {
            return _context.TrainDrivers.OrderBy(d=>d.Id).ToList();
        }
    }
}
