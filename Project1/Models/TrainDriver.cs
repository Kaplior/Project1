namespace Project1.Models
{
    public class TrainDriver
    {
        public int Id { get; set; }
        public int TrainId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public int ContactNumber { get; set; }
        public int WorkingHours { get; set;}
    }
}
