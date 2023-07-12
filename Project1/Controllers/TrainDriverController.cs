using Microsoft.AspNetCore.Mvc;
using Project1.Interface;
using Project1.Models;


namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainDriverController : Controller
    {
        private readonly ITrainDriverRepository _trainDriver;

        public TrainDriverController(ITrainDriverRepository trainDriver)
        {
            _trainDriver = trainDriver;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TrainDriver>))]
        public IActionResult GetDrivers()
        {
            var drivers = _trainDriver.GetDrivers();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(drivers);
        }


    }
}
