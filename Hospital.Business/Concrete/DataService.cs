using Hospital.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Concrete
{
    public class DataService : IDataService
    {
        public int _data { get; set; }

        public void SaveData(int data)
        {
            _data = data;
        }

        public int RetrieveData()
        {
            return _data;
        }
    }
}
