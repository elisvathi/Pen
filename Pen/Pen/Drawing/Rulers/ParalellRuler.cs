using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Rulers
{
    public class ParalellRuler : StrictRuler
    {
        protected override List<PVector> CalculateData(List<PVector> inputData)
        {
            var strict = base.CalculateData(inputData);
            var vec = PVector.Sub(strict[0], inputData[0]);
            var retVal = new List<PVector>();
            foreach(var v in strict)
            {
                retVal.Add(PVector.Sub(v, vec));
            }
            return retVal;
        }
    }
}
