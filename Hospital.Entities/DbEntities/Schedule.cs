using Hospital.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Entities.DbEntities
{
    public class Schedule:IEntity
    {
        public int Id { get; set; }
        public string? WorkTime { get; set; }
    }
}
