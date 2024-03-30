using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class HospitalInfo
    {
        public int? Id { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? HospitalWorkongStartTime { get; set; }
        public string? HospitalWorkongEndTime { get; set; }
        public DateTime? HospitalOpenTime { get; set; }
        public DateTime? HospitalCloseTime { get; set; }
    }
}
