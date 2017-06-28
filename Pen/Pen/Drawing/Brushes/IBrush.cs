﻿using Pen.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Brushes
{
   public interface IBrush
    {
        void Draw(List<PVector> data);
    }
}
