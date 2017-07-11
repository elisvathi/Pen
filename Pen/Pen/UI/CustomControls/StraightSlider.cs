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
    public class StraightSlider : AbstractSlider
    {
        float _sliderWidth = 400;
        float _sliderHeight = 100;
        PVector StartingPoint
        {
            get
            {
                var vec = Center.Copy();
                vec.X -= _sliderWidth / 2;
                return vec;
            }
        }
        PVector EndingPoint
        {
            get
            {
                var vec = StartingPoint.Copy();
                vec.X += _sliderWidth / 2;
                return vec;
            }
        }
        
        public StraightSlider(DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(dt, st)
        {
        }

        public float SliderWidth { get => _sliderWidth; set => _sliderWidth = value; }
        public float SliderHeight { get => _sliderHeight; set => _sliderHeight = value; }
        protected override SKShader GetBackGroundShader
        {
            get
            {
                return SKShader.CreateLinearGradient(StartingPoint.ToSKPoint(), EndingPoint.ToSKPoint(), GradientColors, GradientPositions, SKShaderTileMode.Repeat);
            }
        }
        protected override PVector HandlerPosition
        {
            get
            {
                PVector vec = PVector.Sub(EndingPoint, StartingPoint);
                vec.Mult(Value);
                vec.Add(StartingPoint);
                return vec;
            }
        }
        protected override float[] GradientPositions
        {
            get
            {
                var sz = GradientColors.Length;
                var fl = new float[sz];
                for (int i = 0; i < sz; i++)
                {
                    fl[i] = (float)i / (float)sz;
                }
                return fl;
            }
        }


        protected override void DrawBackground(SKCanvas canv, SKImageInfo info)
        {
            var rect = new SKRect(-_sliderWidth / 2, -_sliderHeight / 2, _sliderWidth / 2, _sliderHeight / 2);
            canv.DrawRect(rect, FillPaint);
            canv.DrawRect(rect, StrokePaint);

        }

        protected override bool IsInside(PTouch t)
        {
            var vec = PVector.Sub(t.Position, Center);
            return Math.Abs(vec.X) <= _sliderWidth / 2 && Math.Abs(vec.Y) <= Height / 2;
        }

        protected override float TouchValue(PTouch t)
        {
            if (IsInside(t))
            {
                var vec = PVector.Sub(t.Position, StartingPoint);
                return vec.X.ToFloat() / _sliderWidth;
            }
            else { return 0; }
        }
    }
}
