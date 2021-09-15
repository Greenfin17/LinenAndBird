using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using LinenAndBird.DataAccess;

namespace LinenAndBird.Controllers
{
    [Route("api/birds")]
    [ApiController]
    public class BirdController : ControllerBase
    {
        BirdRepository _repo;
        public BirdController()
        {
            _repo = new BirdRepository();
        }

        [HttpGet]
        public IActionResult GetAllBirds()
        {
            return Ok(_repo.GetAll());
        }

        [HttpPost]
        public IActionResult AddBird(Bird newBird)
        {
            if (string.IsNullOrEmpty(newBird.Name) || string.IsNullOrEmpty(newBird.Color))
            {
                return BadRequest("Name and Color are required fields"); //400 range
            }
            _repo.Add(newBird);
            return Created($"/api/birds/{newBird.Id}", newBird); // 200 - 299, 201 is created, 204 is accepted,
        }

        [HttpGet("{id}")]
        public IActionResult GetBirdById(Guid id)
        {
            var bird = _repo.GetById(id);

            if (bird == null)
            {
                return NotFound($"No bird with the id {id} was found.");
            }
            return Ok(bird);
        }

        // api/birds/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteBird(Guid id)
        {
            _repo.Remove(id);
            return Ok();
        }

        // api/birds/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateBird(Guid id, Bird bird)
        {
            var birdToUpdate = _repo.GetById(id); 
            if (birdToUpdate == null)
            {
                return NotFound($"Could not find bird with the id {id} for updating.");
            }
            var updatedBird = _repo.UpDate(id, bird);

            return Ok(updatedBird);


        }
    }
}
