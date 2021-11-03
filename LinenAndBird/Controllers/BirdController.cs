using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using LinenAndBird.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace LinenAndBird.Controllers
{
    [Route("api/birds")]
    [ApiController]
    [Authorize]
    public class BirdController : FirebaseController 
    {
        BirdRepository _repo;

        // add parameter to constructor to ask for the configuration
        /* from: services.AddSingleton<IConfiguration>(Configuration); /* -> any time someone asks for this thing,
                                                                             in startup.cs */
        // known as Dependency Injection - can't work without a configuration file

        // Startup.cs addTransient
        public BirdController(BirdRepository repo)
        {
            _repo = repo;
            /* AddSingleton code: 
            var connectionString = config.GetConnectionString("LinenAndBird");
            _repo = new BirdRepository(connectionString);
            */

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllBirds()
        {
            // var fbUserId = User.FindFirst(claim => claim.Type == "user_id").Value;
            var uid = GetFirebaseUid();
            return Ok(_repo.GetAll());
        }

        // per endpoint authorization
        // [Authorize]
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
