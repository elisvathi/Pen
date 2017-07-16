using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pen.Gestures;
using Pen.Drawing.Services;

namespace Pen.UI.CustomControls
{
    public enum ColorProperty
    {
        Hue,
        Saturation,
        Value,
        Alpha,
        Red,
        Green,
        Blue
    }
    public class ColorControl : SliderController<SKColor>
    {
        protected bool IsStroke = true;
        protected  ColorProperty PropType { get; set; }
        protected BindableValue<SKColor> ActiveProperty { get { return IsStroke ? Properties[0] : Properties[1]; } }
        public ColorControl(DrawingConfigService serv, DoubleTouchGestureRecognizer dt, SingleTouchGestureRecognizer st) : base(dt, st)
        {
            InitializeValue(SKColor.Empty);
            InitializeValue(SKColor.Empty);
           
        }
        protected override void UpdateProperty(float requestValue)
        {
            UpdateProperty(requestValue, ActiveProperty);
        }
        protected override void UpdateProperty(float requestValue, BindableValue<SKColor> prop)
        {
            prop.Value.ToHsv(out float h, out float s, out float v);
            var a = prop.Value.Alpha;
            var r = prop.Value.Red;
            var g = prop.Value.Green;
            var b = prop.Value.Blue;
            switch (PropType)
            {

                case ColorProperty.Hue:
                    {
                        var newVal = requestValue * 360;
                        prop.RequestExplicitChange(SKColor.FromHsv(newVal, s, v, a));
                        break;
                    }
                case ColorProperty.Saturation:
                    {
                        prop.RequestExplicitChange(SKColor.FromHsv(h, requestValue, v, a));
                        break;
                    }
                case ColorProperty.Value:
                    {
                        prop.RequestExplicitChange(SKColor.FromHsv(h, s, requestValue, a));
                        break;
                    }
                case ColorProperty.Alpha:

                    {
                        var ret = GetForRgb(requestValue);
                        prop.RequestExplicitChange(SKColor.FromHsv(h, s, v, ret));
                        break;
                    }
                case ColorProperty.Red:
                    {
                        var ret = GetForRgb(requestValue);
                        prop.RequestExplicitChange(new SKColor(ret, g, b, a));
                        break;
                    }
                case ColorProperty.Green:
                    {
                        var ret = GetForRgb(requestValue);
                        prop.RequestExplicitChange(new SKColor(r, ret, b, a));
                        break;
                    }
                case ColorProperty.Blue:
                    {
                        var ret = GetForRgb(requestValue);
                        prop.RequestExplicitChange(new SKColor(r, g, ret, a));
                        break;
                    }
                default: { break; }
            }
        }
        byte GetForRgb(float val)
        {
            var byt = (int)(255 * val);
            var ret = (byte)byt;
            return ret;

        }
        protected override void OnValueChanged(BindableValue<SKColor> prop)
        {
            prop.Value.ToHsv(out float h, out float s, out float v);
            var a = prop.Value.Alpha;
            var r = prop.Value.Red;
            var g = prop.Value.Green;
            var b = prop.Value.Blue;
            switch (PropType)
            {
                case ColorProperty.Alpha:
                    {
                        UpdateValue(a / 255);
                        break;
                    }
                case ColorProperty.Red:
                    {
                        UpdateValue(r / 255);
                        break;
                    }
                case ColorProperty.Green:
                    {
                        UpdateValue(g / 255);
                        break;
                    }
                case ColorProperty.Blue:
                    {
                        UpdateValue(b / 255);
                        break;
                    }
                case ColorProperty.Hue:
                    {
                        UpdateValue(h / 360);
                        break;
                    }
                case ColorProperty.Saturation:
                    {
                        UpdateValue(s);
                        break;
                    }
                case ColorProperty.Value:
                    {
                        UpdateValue(v);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        protected override SKColor[] BackgroundGradientColors {
            get
            {
                var prop = ActiveProperty;
                prop.Value.ToHsv(out float h, out float s, out float v);
                var a = prop.Value.Alpha;
                var r = prop.Value.Red;
                var g = prop.Value.Green;
                var b = prop.Value.Blue;
                switch (PropType)
                {
                    case ColorProperty.Alpha:
                        {
                            return new SKColor[] { new SKColor(r, g, b, 0), new SKColor(r, g, b,255)};
                            
                        }
                    case ColorProperty.Value:
                        {
                            return new SKColor[] { SKColor.FromHsv(h, s, 0), SKColor.FromHsv(h, s, 1) };
                        }
                    case ColorProperty.Saturation:
                        {
                            return new SKColor[] { SKColor.FromHsv(h, 0, v), SKColor.FromHsv(h, 1, v) };
                        }
                    case ColorProperty.Hue: {
                            int nr = 100;
                            var retVal = new SKColor[nr];
                            for (int i = 0; i < nr; i++) {
                                retVal[i] = SKColor.FromHsv(i / 100 * 360, 1, 1);
                            }
                            return retVal;
                        }
                    case ColorProperty.Red:
                        {
                            return new SKColor[] { new SKColor(0, g, b), new SKColor(255, g, b) };
                        }
                    case ColorProperty.Green:
                        {
                            return new SKColor[]
                            {
                                new SKColor(r,0, b), new SKColor(r,255,b)
                            };
                        }
                    case ColorProperty.Blue: {
                            return new SKColor[] { new SKColor(r, g, 0), new SKColor(r, g, 255) };
                        }
                    default: {
                            return new SKColor[] { SKColors.Black, SKColors.White };
                        }
                }
            }
        }
        public void ToStroke() {
            IsStroke = true;
        }
        public void ToFill() {
            IsStroke = false;
        }
    }
}
