namespace Project1.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int RouterId { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
