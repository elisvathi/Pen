using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Drawing.Services;
using Pen.Geometry;

namespace Pen.Drawing.Brushes
{
    public class AdvancedBrush : AbstractBrush
    {
       
        public AdvancedBrush(PRenderer ren) : base(ren)
        {
        }

        public override void Draw(List<PVector> data)
        {
            throw new NotImplementedException();
        }
    }
}
