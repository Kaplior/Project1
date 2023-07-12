using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project1.Dto;
using Project1.Interface;
using Project1.Models;
using System.Diagnostics;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : Controller
    {
        private readonly ITrainRepository _trainRepos;
        private readonly IMapper _mapper;
        public TrainController(ITrainRepository trainRepos, IMapper mapper)
        {
            _trainRepos = trainRepos;
            _mapper = mapper;
        }



        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Train>))]
        public IActionResult GetTrains()
        {
            var trains = _mapper.Map<List<TrainDto>>(_trainRepos.GetTrains());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(trains);
        }

        [HttpGet("{tid}")]
        [ProducesResponseType(200, Type = typeof(Train))]
        [ProducesResponseType(400)]
        public IActionResult GetTrain(int tid)
        {
            if (!_trainRepos.TrainExists(tid))
                return NotFound();

            var train = _mapper.Map<TrainDto>(_trainRepos.GetTrain(tid));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(train);
        }

        [HttpGet("TrainDriver/{TrainId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TrainDriver>))]
        [ProducesResponseType(400)]
        public IActionResult GetDriverbyTrain(int TrainId)
        {
            var driver = _mapper.Map<List<TrainDriverDto>>(_trainRepos.GetDriverbyTrain(TrainId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(driver);
        }




        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTrain([FromBody] TrainDto trainCreate)
        {
            if (trainCreate == null)
                return BadRequest(ModelState);

            var train = _trainRepos.GetTrains()
                 .Where(t => t.TrainNumber == trainCreate.TrainNumber)
                 .FirstOrDefault();

            if (train != null)
            {
                ModelState.AddModelError("", "Train already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var trainMap = _mapper.Map<Train>(trainCreate);
            if (!_trainRepos.CreateTrain(trainMap))
            {
                ModelState.AddModelError("", "Smth went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Nice");
        }




        [HttpPut("{TrainId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(405)]
        public IActionResult UpdateTrain(int TrainId, [FromBody] TrainDto updatedTrain)
        {
            if(updatedTrain==null)
                return BadRequest(ModelState);
            if (TrainId != updatedTrain.Id)
                return BadRequest(ModelState);
            if(!_trainRepos.TrainExists(TrainId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var trainMap = _mapper.Map<Train>(updatedTrain);

            if(!_trainRepos.UpdateTrain(trainMap))
            {
                ModelState.AddModelError("", "Smth went wrong");
                return StatusCode(500,ModelState);
            }
            
            return NoContent();
        }



        //Delete

        [HttpDelete("{TrainId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTrain(int TrainId)
        {
            if (!_trainRepos.TrainExists(TrainId))
            {
                return NotFound();
            }
            
            var trainToDelete = _trainRepos.GetTrain(TrainId);

            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            if(!_trainRepos.DeleteTrain(trainToDelete))
            {
                ModelState.AddModelError("", "SMTH went wrong man");
            }    
            return NoContent();
        }

    }
}
