using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Rulers
{
    public class StrictRuler : AbstractRuler
    {
        private PLine vija;
        public StrictRuler(PLine v)
        {
            vija = v;
        }
        public StrictRuler(PVector start, PVector end) : this(new PLine(start, end))
        {

        }
        public StrictRuler() : this(new PVector(0, 0), new PVector(1, 1)) { }

        public override List<PVector> ControlPoints { get { return new List<PVector> { vija.Start.Copy(), vija.End.Copy() }; } set { vija.Start = value[0]; vija.End = value[1]; } }

        protected override List<PVector> CalculateData(List<PVector> inputData)
        {
            var retVal = new List<PVector>();
            foreach(var v in inputData)
            {
                retVal.Add(vija.ClosesetPoint(v));
            }
            return retVal;
        }
    }
}
