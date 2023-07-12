using Project1.Models;

namespace Project1.Interface
{
    public interface ITrainRepository
    {
        ICollection<Train> GetTrains();
        Train GetTrain(int id);
        ICollection<TrainDriver> GetDriverbyTrain(int trainId);
        bool TrainExists(int Tid);
        bool CreateTrain(Train train);
        bool UpdateTrain(Train train);
        bool Save();
    }
}
