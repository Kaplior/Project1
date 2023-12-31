﻿using AutoMapper;
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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRouter([FromBody] RouterDto routerCreate)
        {
            if (routerCreate == null)
                return BadRequest(ModelState);

            var router = _routerRepos.GetRouters()
                 .Where(r => r.LineName.Trim().ToUpper() == routerCreate.LineName.TrimEnd().ToUpper())
                 .FirstOrDefault();

            if (router != null)
            {
                ModelState.AddModelError("", "Router already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var routerMap = _mapper.Map<Router>(routerCreate);
            if (!_routerRepos.CreateRouter(routerMap))
            {
                ModelState.AddModelError("", "Smth went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Nice");
        }




        //Update

        [HttpPut("{RouterId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(405)]
        public IActionResult UpdateTrain(int RouterId, [FromBody] RouterDto updatedRouter)
        {
            if (updatedRouter == null)
                return BadRequest(ModelState);
            if (RouterId != updatedRouter.Id)
                return BadRequest(ModelState);
            if (!_routerRepos.RouterExists(RouterId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();

            var routerMap = _mapper.Map<Router>(updatedRouter);

            if (!_routerRepos.UpdateRouter(routerMap))
            {
                ModelState.AddModelError("", "Smth went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }




        //Delete

        [HttpDelete("{RouterId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRouter(int RouterId)
        {
            if (!_routerRepos.RouterExists(RouterId))
            {
                return NotFound();
            }

            var routerToDelete = _routerRepos.GetRouter(RouterId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_routerRepos.DeleteRouter(routerToDelete))
            {
                ModelState.AddModelError("", "SMTH went wrong man");
            }
            return NoContent();
        }
    }
}

