using Pen.UI.CustomControls;
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

        public BindableValue<float> S_Width;
        public BindableValue<SKColor> StrokeCol;
        public BindableValue<SKColor> FillCol;
        public BindableValue<bool> UseFill;
        public BindableValue<bool> UseStroke;
        public BindableValue<float> XDispersion;
        public BindableValue<float> YDispersion;
        public BindableValue<float> RotationDispersion;
 
        public BindableValue<float> ScaleDispersion;
        public BindableValue<float> OpacityDispersion;
        public BindableValue<SKBlendMode> BlendMode; 
        public DrawingConfigService()
        {
            S_Width = new BindableValue<float>(1);
            StrokeCol = new BindableValue<SKColor>(SKColors.Black);
            FillCol = new BindableValue<SKColor>(SKColors.White);
            UseFill = new BindableValue<bool>(true);
            UseStroke = new BindableValue<bool>(true);
            XDispersion = new BindableValue<float>(0);
            YDispersion = new BindableValue<float>(0);
            RotationDispersion = new BindableValue<float>(0);
 
            OpacityDispersion = new BindableValue<float>(0);
            BlendMode = new BindableValue<SKBlendMode>(SKBlendMode.DstOver);
            ScaleDispersion = new BindableValue<float>(0);
        }
        public SKPaint SPaint
        {
            get
            {
                return new SKPaint()
                {
                    BlendMode = BlendMode.Value,
                    Color = UseStroke.Value ? StrokeCol.Value : FillCol.Value,
                    IsAntialias = true,
                    Style = UseFill.Value && UseStroke.Value ? SKPaintStyle.StrokeAndFill : UseFill.Value ? SKPaintStyle.Fill : SKPaintStyle.Stroke,
                    StrokeWidth = S_Width.Value
                };
            }
        }
        

    }
}
