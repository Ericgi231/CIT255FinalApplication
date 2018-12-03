using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    class Crop
    {
        public int GrowTime { get; set; }
        public int Age { get; set; }
        public int Value { get; set; }
        public Material Material { get; set; }
        public Mesh Mesh { get; set; }
    }
}
