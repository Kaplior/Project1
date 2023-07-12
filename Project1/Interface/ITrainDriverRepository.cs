using Project1.Models;

namespace Project1.Interface
{
    public interface ITrainDriverRepository
    {
        ICollection<TrainDriver> GetDrivers();
    }
}
