using Project1.Models;

namespace Project1.Interface
{
    public interface ITrainDriverRepository
    {
        ICollection<TrainDriver> GetDrivers();
        TrainDriver GetDriver(int id);
        TrainDriver GetDriver(string name);
        bool TrainDriverExists(int drivid);
        bool CreateTrainDriver(TrainDriver trainDriver);
        bool Save();

    }
}
