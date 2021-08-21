﻿using LinenAndBird.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.DataAccess
{
    public class HatRepository
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

        internal List<Hat> GetAll() // iternal, anybody can use from within the project
        {
            return _hats;
        }

        internal IEnumerable<Hat> GetByStyle(HatStyle style)
        {
            return _hats.Where(hat => hat.Style == style);
        }

        internal void Add(Hat newHat)
        {
            throw new NotImplementedException();
        }
    }
}
