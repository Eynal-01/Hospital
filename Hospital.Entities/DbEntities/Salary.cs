using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Entities.DbEntities
{
    public class Salary
    {
        public int SalaryId { get; set; }
        public double Amount { get; set; }
        public Guid? EmployeeId { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
