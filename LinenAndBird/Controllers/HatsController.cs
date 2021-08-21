﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;

namespace LinenAndBird.Controllers
{
    // [Route("api/[controller]")] // exposed at this endpoint
    [Route("api/hats")] // exposed at this endpoint
    [ApiController] // an api controller, so it returns json or xml
    public class HatsController : ControllerBase
    {
        static List<Hat> _hats = new List<Hat> {
            new Hat
            {
                Color = "Blue",
                Designer = "Charlie",
                Style = HatStyle.OpenBack
            },
            new Hat
            {
                Color = "Black",
                Designer = "Nathan",
                Style = HatStyle.WideBrim
            },
            new Hat
            {
                Color = "Magenta",
                Designer = "Charlie",
                Style = HatStyle.OpenBack
            }
        };
        [HttpGet]
        public List<Hat> GetAllHats()
        {
            return _hats;
        }

        [HttpGet("styles/{style}")]
        public IEnumerable<Hat> GetHatsByStyle(HatStyle style){
            return _hats.Where(hat => hat.Style == style);
        }
        
        [HttpPost]
        public void AddAHat(Hat newHat)
        {
            _hats.Add(newHat);
        }
    }
}
