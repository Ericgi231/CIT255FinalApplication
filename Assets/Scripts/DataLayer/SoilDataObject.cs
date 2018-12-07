using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.DataLayer
{
    public class SoilDataObject
    {
        public int OwnerId { get; set; }
        public int LandId { get; set; }
        public bool IsTilled { get; set; }
        public float GrowTime { get; set; }
        public float Age { get; set; }
        public float Value { get; set; }
        public string Material { get; set; }
        public string Mesh { get; set; }
    }
}
