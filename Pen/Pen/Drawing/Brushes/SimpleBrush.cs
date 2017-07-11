﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;
using Pen.Drawing.Services;

namespace Pen.Drawing.Brushes
{
    public class SimpleBrush : AbstractBrush
    {
        public SimpleBrush(PRenderer ren) : base(ren)
        {
        }

        public override void Draw(List<PVector> data)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                _renderer.DrawLine(data[i], data[i + 1]);
            }
            
        }
    }
}
