using Hospital.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public Guid InvoiceId { get; set; }
        public CustomIdentityUser? Patient { get; set; }
        public string? PaymentType { get; set; }
        public DateTime PaidDate { get; set; }
        public double PaidAmount { get; set; }
    }
}
