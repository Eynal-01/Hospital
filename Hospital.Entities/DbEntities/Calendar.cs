﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Calendar
    {
        public int Id { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }
        public string? Event { get; set; }
    }
}
