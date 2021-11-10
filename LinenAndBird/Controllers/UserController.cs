using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;
using LinenAndBird.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace LinenAndBird.Controllers
{
    [Route("api/users")]
    [ApiController]
    
    public class UserController : FirebaseController
    {
        UserRepository _repo;

        public UserController(UserRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllUsers()
        {
            var fbUserId = User.FindFirst(claim => claim.Type == "user_id").Value;
            var uid = GetFirebaseUid();
            return Ok(_repo.GetAll());
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _repo.GetById(id);

            if (user == null)
            {
                return NotFound($"No user with the id {id} was found.");
            }
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser(User userObj)
        {
            if (string.IsNullOrEmpty(userObj.DisplayName) || string.IsNullOrEmpty(userObj.ImageUrl)
                || string.IsNullOrEmpty(userObj.email))
            {
                return BadRequest("DisplayName, ImageUrl and Email all required.");
            }
            var result = _repo.AddUser(userObj);
            if (result.Equals(Guid.Empty))
            {
                return BadRequest("User not added");
            }
            else return Ok(result);
        }
    }
}
