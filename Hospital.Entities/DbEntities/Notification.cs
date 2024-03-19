using Hospital.Core.Abstract;
using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Notification:IEntity
    {
        public int Id { get; set; }

        public string? SenderId { get; set; }

        public string? ReceiverId { get; set; }

        //public string? SenderDoctorId { get; set; }

        //public string? ReceiverDoctorId { get; set; }


        public string? Message { get; set; }

        public bool IsCheck { get; set; } = false;

        public DateTime Date { get; set; }

        public virtual Admin? SenderAdmin { get; set; }
        public virtual Doctor? SenderDoctor { get; set; }


        public virtual Doctor? ReceiverDoctor { get; set; }
        public virtual Admin? ReceiverAdmin { get; set; }

        public Notification()
        {

        }
    }
}