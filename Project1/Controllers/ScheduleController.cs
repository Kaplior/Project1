using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project1.Dto;
using Project1.Interface;
using Project1.Models;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository _schedRepos;
        private readonly IMapper _mapper;
        public ScheduleController(IScheduleRepository schedRepos, IMapper mapper)
        {
            _schedRepos = schedRepos;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Schedule>))]
        public IActionResult GetSchedules()
        {
            var schedules = _mapper.Map<List<ScheduleDto>>(_schedRepos.GetSchedules());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(schedules);
        }

        [HttpGet("{sid}")]
        [ProducesResponseType(200, Type = typeof(Schedule))]
        [ProducesResponseType(400)]
        public IActionResult GetSchedule(int sid)
        {
            if (!_schedRepos.ScheduleExists(sid))
                return NotFound();

            var schedule = _mapper.Map<ScheduleDto>(_schedRepos.GetSchedule(sid));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(schedule);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSchedule([FromBody] ScheduleDto scheduleCreate)
        {
            if (scheduleCreate == null)
                return BadRequest(ModelState);

            var schedule = _schedRepos.GetSchedules()
                 .Where(t => t.ArrivalTime == scheduleCreate.ArrivalTime)
                 .FirstOrDefault();

            if (schedule != null)
            {
                ModelState.AddModelError("", "Schedule already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var scheduleMap = _mapper.Map<Schedule>(scheduleCreate);
            if (!_schedRepos.CreateSchedule(scheduleMap))
            {
                ModelState.AddModelError("", "Smth went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Nice");
        }
    }
}
