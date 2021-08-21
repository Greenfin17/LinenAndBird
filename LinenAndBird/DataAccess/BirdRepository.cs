using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;

namespace LinenAndBird.DataAccess
{
    public class BirdRepository
    {
        static List<Bird> _birds = new List<Bird>
        {
            new Bird
            {
                Name = "Jimmy",
                Color = "Red",
                Type = BirdType.Dead,
                Size = "medium",
                Accessories = new List<string> { "Beanie", "gold wing tips" }
            }
        };
        internal IEnumerable<Bird> GetAll()
        {
            return _birds;
        }

        internal void Add(Bird newBird)
        {
            _birds.Add(newBird);
        }
    }
}
