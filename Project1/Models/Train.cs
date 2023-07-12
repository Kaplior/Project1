namespace Project1.Models
{
    public class Train
    {
        public int Id { get; set; }
        public decimal Capacity { get; set; }
        public int TrainNumber { get; set; }
        public ICollection<Router> Router { get; set; }
        public ICollection<DriverTrainCategory> DriverTrainCategory { get; set; }
    }
}
