namespace Project1.Models
{
    public class Router
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public string LineName { get; set;}
        public string StartingStation { get; set;}
        public string EndingStation { get; set;}
    }
}
