using Ninject;
using Pen.Geometry;
using Pen.LibraryExtensions;
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

        public float S_Width { get; set; }
        public float Hardness { get; set; }
        [Inject, Named("StrokeColor")]
        public BindableColor SBindableColor { get; set; }
        public SKColor StrokeColor { get { return SBindableColor.Color; } }

        [Inject, Named("FillColor")]
        public BindableColor FBindableColor { get; set; }
        public SKColor FillColor { get { return FBindableColor.Color; } }

        private float _xspacing;
        public float XSpacing { get { return _xspacing; } set { if (value < 0) { _xspacing = 1; } else { _xspacing = value; } } }
        public float XDispersion { get; set; }
        public float YDispersion { get; set; }
        public float RotationDispersion { get; set; }
        public float HardnessDispersion { get; set; }

        public float HueDispersion { get; set; }
        public float SaturationDispersion { get; set; }
        public float LightnessDispersion { get; set; }

        public float ScaleDispersion { get; set; }
        public float OpacityDispersion { get;set;}
        public SKBlendMode BlendMode { get; set; }
        public DrawingConfigService()
        {
            S_Width = 5;
            Hardness = 0.5F;


            XDispersion = 0;
            YDispersion = 0;
            RotationDispersion = 0;

            OpacityDispersion = 0;
            BlendMode = SKBlendMode.DstOver;
            ScaleDispersion = 0;
            XSpacing = 10;
        }
        public SKPaint SPaint
        {
            get
            {
                return new SKPaint()
                {
                    //BlendMode = BlendMode.Value,
                    Color = StrokeColor,
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = S_Width
                };
            }
        }
        public SKPaint FPaint
        {
            get
            {
                return new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = FillColor,
                    IsAntialias = true
                };
            }
        }

        public SKShader GetSimpleBrush(SKPoint p, float rad)
        {
            return SKShader.CreateRadialGradient(p, rad, GetGradientColors, GetGradientPositions, SKShaderTileMode.Clamp);
        }
        public SKShader GetSimpleBrush(SKPoint p)
        {
            return GetSimpleBrush(p, S_Width);
        }
        public SKPaint GetBrush(PVector p)
        {
            return new SKPaint() { Style = SKPaintStyle.Fill, Shader = GetSimpleBrush(p.ToSKPoint()) };
        }

        private SKColor[] GetGradientColors
        {
            get
            {
                return new SKColor[3] { new SKColor(StrokeColor.Red, StrokeColor.Green, StrokeColor.Blue, StrokeColor.Alpha), new SKColor(StrokeColor.Red, StrokeColor.Green, StrokeColor.Blue, StrokeColor.Alpha), new SKColor(StrokeColor.Red, StrokeColor.Green, StrokeColor.Blue, 0) };

            }

        }
        
        private byte GetAlphaPercentage(float perc, byte a)
        {
            var t = (int)a;
            var b = (float)t;
            var p = b * perc;
            var r = (int)b;
            return (byte)r;
        }
        private float[] GetGradientPositions
        {
            get
            {
                return new float[3] { 0, Hardness, 1 };
            }
        }
    }
}
