using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataLayer
{
    public class FarmDataObject
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public int Score { get; set; }
        public string LastSave { get; set; }
    }
}
