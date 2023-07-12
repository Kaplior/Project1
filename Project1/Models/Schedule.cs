namespace Project1.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public ICollection<Router> Routers { get; set; }
    }
}
