using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataLayer
{
    interface IRepository
    {
        DataContainer ReadAll();
        DataContainer ReadById(int id);
        void WriteAll(DataContainer Data);
        void DeleteById(int id);
    }
}
