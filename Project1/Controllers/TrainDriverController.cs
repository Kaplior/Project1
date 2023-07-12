using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project1.Dto;
using Project1.Interface;
using Project1.Models;


namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainDriverController : Controller
    {
        private readonly ITrainDriverRepository _trainDriverRepos;
        private readonly IMapper _mapper;
        public TrainDriverController(ITrainDriverRepository trainDriverRepos, IMapper mapper )
        {
            _trainDriverRepos = trainDriverRepos;
            _mapper = mapper;
        }


        //GET

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TrainDriver>))]
        public IActionResult GetDrivers()
        {
            var drivers = _mapper.Map<List<TrainDriverDto>>(_trainDriverRepos.GetDrivers());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(drivers);
        }

        [HttpGet("{drivid}")]
        [ProducesResponseType(200, Type = typeof(TrainDriver))]
        [ProducesResponseType(400)]
        public IActionResult GetDriver(int drivid)
        {
            if (!_trainDriverRepos.TrainDriverExists(drivid))
                return NotFound();

            var driver = _mapper.Map<TrainDriverDto>(_trainDriverRepos.GetDriver(drivid));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(driver);
        }



        //POST

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public IActionResult CreateTrainDriver([FromBody] TrainDriverDto trainDriverCreate)
        {
            if (trainDriverCreate == null)
                return BadRequest(ModelState);

            var driver = _trainDriverRepos.GetDrivers()
                 .Where(d => d.Name.Trim().ToUpper() == trainDriverCreate.Name.TrimEnd().ToUpper())
                 .FirstOrDefault();

             if (driver != null)
             {
                 ModelState.AddModelError("", "TrainDriver already exists");
                 return StatusCode(422, ModelState);
             }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var driverMap = _mapper.Map<TrainDriver>(trainDriverCreate);
            if (!_trainDriverRepos.CreateTrainDriver(driverMap))
            {
                ModelState.AddModelError("", "Smth went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Nice");

        }





        //UPDATE

        [HttpPut("{DriverId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(405)]
        public IActionResult UpdateTrain(int DriverId, [FromBody] TrainDriverDto updatedTrainDriver)
        {
            if (updatedTrainDriver == null)
                return BadRequest(ModelState);
            if (DriverId != updatedTrainDriver.Id)
                return BadRequest(ModelState);
            if (!_trainDriverRepos.TrainDriverExists(DriverId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var trainDriverMap = _mapper.Map<TrainDriver>(updatedTrainDriver);

            if (!_trainDriverRepos.UpdateTrainDriver(trainDriverMap))
            {
                ModelState.AddModelError("", "Smth went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        //Delete

        [HttpDelete("{DriverId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTrainDriver(int DriverId)
        {
            if (!_trainDriverRepos.TrainDriverExists(DriverId))
            {
                return NotFound();
            }

            var driverToDelete = _trainDriverRepos.GetDriver(DriverId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_trainDriverRepos.DeleteTrainDriver(driverToDelete))
            {
                ModelState.AddModelError("", "SMTH went wrong man");
            }
            return NoContent();
        }

    }
}
