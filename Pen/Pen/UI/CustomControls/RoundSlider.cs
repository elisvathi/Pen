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
            EndAngle = 270;
            SliderWidth = 30;
        }

        public float Radius { get => _radius; set => _radius = value; }
        public float StartAngle { get => _startAngle; set => _startAngle = value; }
        public float EndAngle { get => _endAngle; set => _endAngle = value; }
        public float SliderWidth { get => _sliderWidth; set => _sliderWidth = value; }
        public float StartRadius { get => Radius - SliderWidth / 2; }
        public float EndRadius { get => StartRadius + SliderWidth; }
        float AngleDifference { get { return EndAngle - StartAngle; } }
        PVector StartingPoint { get { var vec = new PVector(0, 1); vec.RotateDegrees(StartAngle); vec.SetMag(Radius); vec.Add(Center); return vec; } }
        PVector EndingPoint { get { var vec = new PVector(0, 1); vec.RotateDegrees(EndAngle); vec.SetMag(Radius); vec.Add(Center); return vec; } }

        protected override PVector HandlerPosition {
            get
            {
                var vec = PVector.Sub(StartingPoint, Center);
                vec.RotateDegrees(Value * AngleDifference);
                vec.Add(Center);
                return vec;
            }
        }

        protected override SKShader GetBackGroundShader => SKShader.CreateSweepGradient(Center.ToSKPoint(), GradientColors, GradientPositions);

        protected override float[] GradientPositions { get
            {
                var fl = new float[GradientColors.Length];
                for (int i = 0; i < fl.Length; i++) {
                    float pos = i / fl.Length;
                    var d = pos.ToDouble().Map(0,1,StartAngle/360, EndAngle/360);
                    fl[i] = d.ToFloat();
                }
                return fl;
            } }

        protected override void DrawBackground(SKCanvas canv, SKImageInfo info)
        {
            
            var rectOut = GetBounds;
            rectOut.Offset(SliderWidth / 2, SliderWidth / 2);
            var rectIn = GetBounds;
            Offset(ref rectIn, -SliderWidth / 2);
            Offset(ref rectOut, SliderWidth / 2);
            var rectMid = GetBounds;
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
        private void Offset(ref SKRect rectIn, float val) {
            rectIn.Bottom += val;
            rectIn.Top -= val;
            rectIn.Left -= val;
            rectIn.Right += val;
            
        }
        private SKRect GetBounds { get {
                var left = Center.X - Radius;
                var right = Center.X + Radius;
                var top = Center.Y - Radius;
                var bottom = Center.Y + Radius;
                return new SKRect(left.ToFloat(), top.ToFloat(), right.ToFloat(), bottom.ToFloat());
            } }

        

        protected override bool IsInside(PTouch t)
        {
            var vec = PVector.Sub(t.Position, Center);
            return vec.Mag <= EndRadius && vec.Mag >= StartRadius && vec.AngleDegrees >= StartAngle && vec.AngleDegrees <= EndAngle;
        }

        protected override float TouchValue(PTouch t)
        {
            if (IsInside(t))
            {

                var vec = PVector.Sub(t.Position, Center);
                return (vec.AngleDegrees.ToFloat() - StartAngle) / AngleDifference;
            }
            { return 0; }
        }
    }
}
