using Pen.UI.MainCanvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Gestures;
using SkiaSharp.Views.Forms;
using SkiaSharp;
using Pen.Geometry;
using Pen.MathExtenions;

namespace Pen.UI.CustomControls
{
    public abstract class AbstractSlider : TouchCanvas
    {
        public delegate void SliderValue(float value);
        public event SliderValue SliderValueChanged;
        private bool startedTouching = false;
        private float _prevValue;
        private float _value = 0;
        protected virtual float _handlerSize { get; set; } = 20;
        protected abstract PVector HandlerPosition { get; }
        protected float Value { get => _value; set { _value = value; SliderValueChanged?.Invoke(_value); Redraw(); } }

        public AbstractSlider(DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(dt, st)
        {
        }
        protected abstract bool IsInside(PTouch t);
        protected abstract float TouchValue(PTouch t);
        protected override void DrawCanvas(object sender, SKPaintSurfaceEventArgs e) {
            var canv = e.Surface.Canvas;
            var info = e.Info;
            DrawBackground(canv, info);
            DrawHadler(canv, info, _handlerSize);
        }
        private void DrawHadler(SKCanvas canv, SKImageInfo info, float handlerSize)
        {
            
            canv.DrawCircle(HandlerPosition.X.ToFloat(), HandlerPosition.Y.ToFloat(), _handlerSize, HandlerFillPaint);
            canv.DrawCircle(HandlerPosition.X.ToFloat(), HandlerPosition.Y.ToFloat(), _handlerSize, StrokePaint);
        }
        protected abstract void DrawBackground(SKCanvas canv, SKImageInfo info);
        protected abstract SKShader GetBackGroundShader { get; }
        protected virtual SKColor[] GradientColors { get {
                return new SKColor[] { SKColors.Black, SKColors.White };
            } }
        protected abstract float[] GradientPositions { get; }
        protected SKPaint FillPaint { get { return new SKPaint() { Style = SKPaintStyle.Fill, Shader = GetBackGroundShader }; } }
        protected  SKPaint StrokePaint { get {
                return new SKPaint()
                {
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 1,
                    Color = SKColors.Black
                };
            } }
        private  SKPaint HandlerFillPaint { get {
                return new SKPaint()
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColors.White
                };
            } }
        protected void ChangeValueExplicitely(float val)
        {
            _value = val;
            Redraw();
        }
        #region SingleTouches
        protected override void CancelTouch(PTouch args)
        {
            ExitTouch(args);
        }
        protected override void EndTouch(PTouch args)
        {
            if (IsInside(args) && startedTouching)
            {
                startedTouching = false;
                _prevValue = Value;
            }
        }
        protected override void MoveTouch(PTouch args)
        {
            if (startedTouching)
            {
                if (IsInside(args)) {
                    Value = TouchValue(args);
                }
                else
                {
                    ExitTouch(args);
                }
            }
        }
        protected override void ExitTouch(PTouch args)
        {
            if (startedTouching)
            {
                startedTouching = false;
                Value = _prevValue;
            }
        }
        protected override void StartTouch(PTouch args)
        {
            if (IsInside(args))
            {
                startedTouching = true;
                _prevValue = Value;
                Value = TouchValue(args);
            }
        }
       
        #endregion
        #region DoubleTouches
        protected override void PanCanvas(MoveEventArgs args)
        {
        }
        protected override void ScaleCanvas(ScaleEventArgs args)
        {

        }
        protected override void RotateCanvas(RotateEventArgs args)
        {

        }
        #endregion
    }
}
