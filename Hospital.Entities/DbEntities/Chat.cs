using Hospital.Core.Abstract;
using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }
        public string? ReceiverId { get; set; }
        public string? SenderId { get; set; }

        public virtual Admin? ReceiverAdmin { get; set; }
        public virtual Doctor? ReceiverDoctor { get; set; }
        public virtual ICollection<Message>? Messages { get; set; }
    }
}
