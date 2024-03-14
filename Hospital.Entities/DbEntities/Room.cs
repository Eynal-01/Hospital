using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class Room
    {
        public int Id { get; set; } 
        public string? RoomNo { get; set; }
        public virtual ICollection<Doctor>? Doctors { get; set; }
    }
}