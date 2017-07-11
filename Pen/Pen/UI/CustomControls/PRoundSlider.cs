using Pen.UI.MainCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Gestures;
using SkiaSharp.Views.Forms;
using Pen.Geometry;
using SkiaSharp;
using Pen.MathExtenions;
using Pen.LibraryExtensions;
using static Pen.UI.CustomControls.AbstractSlider;

namespace Pen.UI.CustomControls
{
    public class PRoundSlider : TouchCanvas
    {
        
        public event SliderValue SliderValueChanged;
        protected float startAngle;
        protected float endAngle;
        protected float actualValue;
        protected float minRadius;
        protected float maxRadius;
        private float MidRadius { get { return SliderWidth / 2 + minRadius; } }
        private PVector Center { get { return new PVector(maxRadius / 2, maxRadius / 2); } }
        private PVector Position { get { return new PVector(X, Y); } }
        private PVector AbsoluteCenter { get { return PVector.Add(Center, Position); } }
        private float AngleDifference { get { return endAngle - startAngle; } }
        protected virtual float ActualValue { get { return actualValue; } set { actualValue = value; Redraw(); } }
        private float SliderWidth { get { return maxRadius - minRadius; } set { var temp = MidRadius; maxRadius = temp + value / 2; minRadius = temp - value / 2; Redraw(); } }
        private PVector HandlerPosition
        {
            get
            {
                var v = new PVector(0, 1);
                v.SetMag(MidRadius);
                v.RotateDegrees(ActualAngle);
                v.Add(Center);
                return v;
            }
        }
        float ActualAngle
        {
            get
            {
                return startAngle + (endAngle - startAngle) * actualValue;
            }
        }
        public PRoundSlider(DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(dt, st)
        {
            startAngle = 90;
            endAngle = 180;
            actualValue = 0;
        }
        protected virtual SKColor[] BackgroundGradientColors
        {
            get
            {
                return new SKColor[] {
                SKColors.Black,
                SKColors.White
            };
            }
        }
        protected virtual SKShader ControlShader
        {
            get
            {
                var positions = new float[BackgroundGradientColors.Length];
                for (var i = 0; i < positions.Length; i++)
                {
                    var ang = AngleDifference * i + startAngle;
                    positions[i] = ang / 360;
                }
                return SKShader.CreateSweepGradient(Center.ToSKPoint(), BackgroundGradientColors, positions);
            }
        }
        protected void UpdateValue(float val)
        {
            ActualValue = val;
        }
        protected override void DrawCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            var canv = e.Surface.Canvas;
            var stroke = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 5,
                Color = SKColors.Black
            };
            var fill = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Shader = ControlShader
            };

            DrawHandler(canv);
        }
        private void DrawHandler(SKCanvas canv)
        {
            var stroke = new SKPaint()
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 1,
                Color = SKColors.Black
            };
            var fill = new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.White
            };
            canv.DrawCircle(HandlerPosition.X.ToFloat(), HandlerPosition.Y.ToFloat(), SliderWidth * 0.8F, fill);
            canv.DrawCircle(HandlerPosition.X.ToFloat(), HandlerPosition.Y.ToFloat(), SliderWidth * 0.8F, stroke);
        }
        protected override void CancelTouch(PTouch args)
        {
            throw new NotImplementedException();
        }
        protected override void EndTouch(PTouch args)
        {
            throw new NotImplementedException();
        }
        protected override void ExitTouch(PTouch args)
        {
            throw new NotImplementedException();
        }
        protected override void MoveTouch(PTouch args)
        {
            throw new NotImplementedException();
        }
        protected override void StartTouch(PTouch args)
        {
            if (Contains(args))
            {
                SliderValueChanged?.Invoke(ValueFromPoint(args.Position));
            }
        }
        private bool Contains(PTouch args)
        {
            var vec = PVector.Sub(args.Position, AbsoluteCenter);
            return vec.Mag < maxRadius && vec.Mag < minRadius && ContainsRadius(PositionRadius(args.Position));
        }
        private bool ContainsRadius(float v)
        {
            return v > startAngle && v < endAngle;
        }
        private float ValueFromAngle(float ang)
        {
            return (ang - startAngle) / AngleDifference;
        }
        private float ValueFromPoint(PVector vec)
        {
            return ValueFromAngle(PositionRadius(vec));
        }
        private float PositionRadius(PVector position)
        {
            return PVector.Sub(position, AbsoluteCenter).AngleDegrees.ToFloat();
        }

        #region doubleTouches
        protected override void PanCanvas(MoveEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void RotateCanvas(RotateEventArgs args)
        {
            throw new NotImplementedException();
        }

        protected override void ScaleCanvas(ScaleEventArgs args)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
