using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Services
{
    public class DrawingConfigService
    {
        public float SWidth { get; set; }
        public bool UseFill { get; set; }
        public bool UseStroke { get; set; }
        public SKColor StrokeColor { get; set; }
        public SKColor FillColor { get; set; }
        public DrawingConfigService()
        {
            SWidth = 1;
            UseFill = false;
            UseStroke = true;
            StrokeColor = SKColors.Black;
            FillColor = SKColors.Gray;
        }
        public SKPaint SPaint
        {
            get
            {
                return new SKPaint()
                {
                    Color = UseStroke?StrokeColor:FillColor,
                    IsAntialias = true,
                    Style = UseFill && UseStroke ? SKPaintStyle.StrokeAndFill : UseFill ? SKPaintStyle.Fill : SKPaintStyle.Stroke,
                    StrokeWidth = SWidth
                };
            }
        }
    }
}
