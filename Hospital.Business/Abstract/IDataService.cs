﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Business.Abstract
{
    public interface IDataService
    {
        void SaveData(int data);
        int RetrieveData();
    }
}
