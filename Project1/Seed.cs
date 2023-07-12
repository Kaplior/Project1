using Project1.Data;
using Project1.Models;
using System;
using System.Diagnostics.Metrics;

namespace Project1
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.DriverTrainCategory.Any())
            {
                var driverTrainCategory = new List<DriverTrainCategory>()
                {
                    new DriverTrainCategory()
                    {
                        Train = new Train()
                        {
                            TrainNumber = 1,
                            Capacity = 150,
                            DriverTrainCategory = new List<DriverTrainCategory>()
                            {
                                new DriverTrainCategory { TrainDriver = new TrainDriver() { Name = "John"}}
                            },
                            Router = new List<Router>()
                            {
                                new Router { LineName="Chilanzar",StartingStation = "Buyuk Ipak Yuli", EndingStation = "Almazar",
                                Schedule = new Schedule(){ ArrivalTime =  new DateTime(2023,07,12), DepartureTime = new DateTime(2023,07,12) } },
                            }
                        },
                        TrainDriver = new TrainDriver()
                        {
                            Name = "John",
                            BirthDate = new DateTime(1984,12,04),
                            Salary = 60000,
                            ContactNumber = "901234567",
                            WorkingHours = 8
                        }
                    }
                };
                dataContext.DriverTrainCategory.AddRange(driverTrainCategory);
                dataContext.SaveChanges();
            }
        }
    }
}