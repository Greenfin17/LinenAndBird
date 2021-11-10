using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinenAndBird.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string ImageUrl { get; set; }
        public Guid FirebaseUid { get; set; }
        public string email { get; set; }
    }
}
