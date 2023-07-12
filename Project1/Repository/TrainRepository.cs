using Project1.Data;
using Project1.Interface;
using Project1.Models;

namespace Project1.Repository
{
    public class TrainRepository : ITrainRepository
    {
        private readonly DataContext _context;

        public TrainRepository(DataContext context)
        {
            _context = context;
        }

        public Train GetTrain(int id) 
        {
            return _context.Trains.Where(t => t.Id == id).FirstOrDefault();
        }
        public ICollection<Train> GetTrains()
        {
            return _context.Trains.OrderBy(t => t.Id).ToList();
        }

        public ICollection<TrainDriver> GetDriverbyTrain(int tid)
        {
            return _context.DriverTrainCategory.Where(t=>t.TrainId== tid).Select(d=>d.TrainDriver).ToList();
        }

        public bool TrainExists(int Tid)
        {
            return _context.Trains.Any(t => t.Id == Tid);
            //throw new NotImplementedException();
        }

        //----------------------------------//
        public bool CreateTrain(Train train)
        {
            _context.Add(train);
            return Save();
        }
        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
 