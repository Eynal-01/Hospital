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
        public string Id { get; set; }

        public string? SenderId { get; set; }

        public virtual CustomIdentityUser? Sender { get; set; }

        public string? ReceiverId { get; set; }

        public virtual CustomIdentityUser? Receiver { get; set; }

        public string? Message { get; set; }

        public bool IsCheck { get; set; } = false;

        public DateTime Date { get; set; }

        public Notification()
        {

        }
    }
}