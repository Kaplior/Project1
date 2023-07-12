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
        public IActionResult GetDriverbyTrain(int Trainid)
        {
            var driver = _mapper.Map<List<TrainDriverDto>>(_trainRepos.GetDriverbyTrain(Trainid));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(driver);
        }
    }
}
