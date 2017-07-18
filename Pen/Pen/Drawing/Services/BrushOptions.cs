using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Services
{
   public struct BrushOptions
    {
        public float XSpacing { get; set; }
        public float XDispersion { get; set; }
        public float YDispersion { get; set; }
        public float RotationDispersion { get; set; }
        public float HardnessDispersion { get; set; }

        public float HueDispersion { get; set; }
        public float SaturationDispersion { get; set; }
        public float LightnessDispersion { get; set; }

        public float ScaleDispersion { get; set; }
        public float OpacityDispersion { get; set; }
        public float StartingLength { get; set; }
        public float EndingLength { get; set; }
        public SKBlendMode BlendMode { get; set; }
    }
}
