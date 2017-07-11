using Pen.Drawing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;

namespace Pen.Drawing.Brushes
{
   public abstract class AbstractBrush: IBrush
    {
        protected PRenderer _renderer;
        public AbstractBrush(PRenderer ren)
        {
            _renderer = ren;
        }

        public abstract void Draw(List<PVector> data);
    }
}
