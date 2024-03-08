using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class DoctorSchedule
    {
        [Key]
        public int Id { get; set; }
        public string? DoctorId { get; set; }
        public string? ScheduleId { get; set; }
        public string? RoomId { get; set; }
    }
}