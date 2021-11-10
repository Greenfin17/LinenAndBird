﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinenAndBird.Models;


namespace LinenAndBird.DataAccess
{
    public interface IHatRepository
    {
        Hat GetById(Guid hatId);
        IEnumerable<Hat> GetAll();
        IEnumerable<Hat> GetByStyle(HatStyle style);
        void Add(Hat newHat);
    }
}
