using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Services
{
    public class PBlendingMode
    {
        public static Dictionary<string, SKBlendMode> BlendinModesDict = new Dictionary<string, SKBlendMode>
            {
                { "Clear", SKBlendMode.Clear },
                    { "Color", SKBlendMode.Color },
                    { "Color Burn", SKBlendMode.ColorBurn },
                    { "Color Dodge", SKBlendMode.ColorDodge },
                    { "Darken", SKBlendMode.Darken },
                    { "Difference", SKBlendMode.Difference },
                    { "Destination", SKBlendMode.Dst },
                    { "Destination at Top", SKBlendMode.DstATop },
                    { "Destination In", SKBlendMode.DstIn },
                    { "Destination Out", SKBlendMode.DstOut },
                    { "Destination Over", SKBlendMode.DstOver },
                    { "Exclusion", SKBlendMode.Exclusion },
                    { "Hard Light", SKBlendMode.HardLight },
                    { "Hue", SKBlendMode.Hue },
                    { "Lighten", SKBlendMode.Lighten },
                    { "Luminosity", SKBlendMode.Luminosity },
                    { "Modulate", SKBlendMode.Modulate },
                    { "Multiply", SKBlendMode.Multiply },
                    { "Overlay", SKBlendMode.Overlay },
                    { "Plus", SKBlendMode.Plus },
                    { "Saturation", SKBlendMode.Saturation },
                    { "Screen", SKBlendMode.Screen },
                    { "Soft Light", SKBlendMode.SoftLight },
                    { "Source", SKBlendMode.Src },
                    { "Source at Top", SKBlendMode.SrcATop },
                    { "Source In", SKBlendMode.SrcIn },
                    { "Source Out", SKBlendMode.SrcOut },
                    { "Source Over", SKBlendMode.SrcOver },
                    { "XOR", SKBlendMode.Xor }
            };

        public static List<string> Names => BlendinModesDict.Keys.ToList();

        public string ModeName;
        public SKBlendMode SelectedMode { get { BlendinModesDict.TryGetValue(ModeName, out SKBlendMode mode); return mode; } }
        public PBlendingMode()
        {
            ModeName = "Source Over";
        }

    }
}
