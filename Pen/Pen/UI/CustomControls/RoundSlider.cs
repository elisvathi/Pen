using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Geometry;
using Pen.Gestures;
using SkiaSharp;
using Pen.LibraryExtensions;
using Pen.MathExtenions;

namespace Pen.UI.CustomControls
{
    public class RoundSlider : AbstractSlider
    {
        float _radius;
        float _startAngle;
        float _endAngle;
        float _sliderWidth;
        public RoundSlider(DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(dt, st)
        {
            Radius = 100;
            StartAngle = 90;
            EndAngle = 180;
            SliderWidth = 30;
        }

        public float Radius { get => _radius; set => _radius = value; }
        public float StartAngle { get => _startAngle; set => _startAngle = value; }
        public float EndAngle { get => _endAngle; set => _endAngle = value; }
        public float SliderWidth { get => _sliderWidth; set => _sliderWidth = value; }
        public float StartRadius { get => Radius - SliderWidth / 2; }
        
        public float EndRadius { get => StartRadius + SliderWidth; }
        float AngleDifference { get { return EndAngle - StartAngle; } }
        float ActualAngle { get { return AngleDifference * Value; } }
        PVector StartingPoint { get { var vec = new PVector(0,1); vec.RotateDegrees(StartAngle.ConvertToPVectorAngle()); vec.SetMag(Radius); vec.Add(Center); return vec; } }
        PVector EndingPoint { get { var vec = new PVector(0,1); vec.RotateDegrees(EndAngle.ConvertToPVectorAngle()); vec.SetMag(Radius); vec.Add(Center); return vec; } }

        protected override PVector HandlerPosition {
            get
            {
                //var vec = new PVector(Radius, 0);

                //vec.RotateDegrees(StartAngle.ConvertToSkiaAngle());
                var vec = PVector.Sub(StartingPoint, Center);
                vec.RotateDegrees(ActualAngle);
                vec.Add(Center);
                return vec;
            }
        }
        protected override SKColor[] GradientColors
        {
            get
            {
                int n = 100;
                var cols = new SKColor[100];
                for (int i = 0; i < n; i++)
                {
                    float nn = (float)i / (float)n;
                    cols[i] = SKColor.FromHsv(nn * 360, 100, 100);
                }
                return cols;
            }
        }
        protected override SKShader GetBackGroundShader => SKShader.CreateSweepGradient(Center.ToSKPoint(), GradientColors, GradientPositions);
       
        protected override float[] GradientPositions { get
            {
                var fl = new float[GradientColors.Length];
                for (int i = 0; i < fl.Length; i++) {
                    float pos = (float)i / (float)fl.Length;
                    var d = pos.ToDouble().Map(0,1,StartAngle/360, EndAngle/360);
                    fl[i] = d.ToFloat();
                }
                return fl;
            } }

        protected override void DrawBackground(SKCanvas canv, SKImageInfo info)
        {
            base.DrawBackground(canv, info);
            var rectOut = GetBounds(EndRadius);
            
            var rectIn = GetBounds(StartRadius);
            
            var rectMid = GetBounds(Radius);
            var pin = new SKPath();
            pin.AddArc(rectIn, StartAngle, AngleDifference);
            var pout = new SKPath();
            pout.AddArc(rectOut, StartAngle, AngleDifference);
            var pmid = new SKPath();
            pmid.AddArc(rectMid, StartAngle, AngleDifference);
            var fill = FillPaint;
            var pt = new SKPaint() { Style = SKPaintStyle.Stroke, Shader = fill.Shader, StrokeCap = SKStrokeCap.Butt, StrokeWidth = SliderWidth };
            canv.DrawPath(pmid, pt);
            canv.DrawPath(pin, StrokePaint);
            canv.DrawPath(pout, StrokePaint);
            
            
        }
     
       

        private SKRect GetBounds(float radius) {
            var left = Center.X - radius;
            var right = Center.X + radius;
            var top = Center.Y - radius;
            var bottom = Center.Y + radius;
            return new SKRect(left.ToFloat(), top.ToFloat(), right.ToFloat(), bottom.ToFloat());
        }

        protected override bool IsInside(PTouch t)
        {
            var vec = PVector.Sub(t.Position, Center);
            System.Diagnostics.Debug.WriteLine("KENDI ESHTE: " + vec.AngleDegrees);
            System.Diagnostics.Debug.WriteLine("VLERA ESHTE: " + Value);
            var ang = GetTouchAngle(t);
            return vec.Mag <= EndRadius && vec.Mag >= StartRadius && ang >= _startAngle && ang <= _endAngle;
        }

        private float GetTouchAngle(PTouch t)
        {
            var vec = PVector.Sub(t.Position, Center);
            return vec.AngleDegrees.ConvertToSkiaAngle();
        }

        protected override float TouchValue(PTouch t)
        {
            if (IsInside(t))
            {

                
                return GetTouchAngle(t) / AngleDifference;
            }
            { return 0; }
        }
    }
}
