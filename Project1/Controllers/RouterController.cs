using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project1.Dto;
using Project1.Interface;
using Project1.Models;

namespace Project1.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class RouterController : Controller
        {
            private readonly IRouterRepository _routerRepos;
            private readonly IMapper _mapper;
            public RouterController(IRouterRepository routerRepos, IMapper mapper)
            {
                _routerRepos = routerRepos;
                _mapper = mapper;
            }


            [HttpGet]
            [ProducesResponseType(200, Type = typeof(IEnumerable<Router>))]
            public IActionResult GetSchedules()
            {
                var routers = _mapper.Map<List<RouterDto>>(_routerRepos.GetRouters());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                return Ok(routers);
            }

            [HttpGet("{rid}")]
            [ProducesResponseType(200, Type = typeof(Router))]
            [ProducesResponseType(400)]
            public IActionResult GetRouter(int rid)
            {
                if (!_routerRepos.RouterExists(rid))
                    return NotFound();

                var router = _mapper.Map<RouterDto>(_routerRepos.GetRouter(rid));

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(router);
            }
        }
    }

