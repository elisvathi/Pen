using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Rulers
{
    public abstract class AbstractRuler : IRuler
    {
        public abstract List<PVector> ControlPoints { get; set; }
        public double RulerAffectWeight { get; set; }

        public List<PVector> ApplyRuler(List<PVector> inputData)
        {
            var generatedData = CalculateData(inputData);
            var retVal = new List<PVector>();
            for (int i = 0; i < inputData.Count; i++)
            {
                var vec = PVector.Sub(generatedData[i], inputData[1]);
                vec.Mult(RulerAffectWeight);
                vec.Add(inputData[i]);
                retVal.Add(vec);
            }
            return retVal;
        }
        protected abstract List<PVector> CalculateData(List<PVector> inputData);
    }
}
