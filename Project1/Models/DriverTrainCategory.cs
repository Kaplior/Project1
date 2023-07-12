namespace Project1.Models
{
    public class DriverTrainCategory
    {
        public int DriverId { get; set; }
        public int TrainId { get; set; }
        public TrainDriver TrainDriver { get; set; }
        public Train Train { get; set; }
    }
}
