﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Attendance
    {
        public string? EmployeeName { get; set; }
        public Calendar? Calendar { get; set; }
        public bool Active { get; set; }
        public bool Unactive { get; set; }
    }
}
