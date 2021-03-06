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
    // [Route("api/[controller]")] // exposed at this endpoint
    [Route("api/hats")] // exposed at this endpoint
    [ApiController] // an api controller, so it returns json or xml
    public class HatsController : ControllerBase
    {
        IHatRepository _repo;
        public HatsController(IHatRepository hatRepo)
        {
            _repo = hatRepo;
        }
            
        [HttpGet]
        public IActionResult GetAllHats()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("styles/{style}")]
        public IEnumerable<Hat> GetHatsByStyle(HatStyle style){

            return _repo.GetByStyle(style);
        }
        
        [HttpPost]
        public void AddAHat(Hat newHat)
        {
            _repo.Add(newHat);
        }
    }
}
