using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.MathExtenions
{
   public static class NumberConversions
    {
        public static double ToDouble(this float val)
        {
            return (double)val;
        }
        public static float ToFloat(this double val)
        {
            return (float)val;
        }
        public static double ToRadians(this double val)
        {
            return val * Math.PI / 180;
        }
        public static double ToDegrees(this double val)
        {
            return val * (180 / Math.PI);
        }
    }
}
