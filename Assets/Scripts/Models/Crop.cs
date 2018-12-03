using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Models
{
    class Crop
    {
        public float GrowTime { get; set; }
        public float Age { get; set; }
        public float Value { get; set; }
        public Material Material { get; set; }
        public Mesh Mesh { get; set; }
    }
}
