using Pen.LibraryExtensions;
using Pen.UI.CustomControls;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pen.Drawing.Services
{
    public class BindableColor
    {
  
        byte _red;
        byte _green;
        byte _blue;
        byte _alpha;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string name= "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public SKColor Color { get { return new SKColor(_red, _green, _blue, _alpha); } }
        public BindableColor(SKColor c)
        {
            _red = c.Red;
            _green = c.Green;
            _blue = c.Blue;
            _alpha = c.Alpha;
        }
      

        

        public BindableColor():this(SKColors.Black)
        {

        }
        public float Hue {

            get { return Color.Hue; }
            set
            {
                Color.ToHsl(out float h, out float s, out float v);
                var d = SKColor.FromHsv(value, s, v);
                UpdateRGB(value, s, v);
                OnPropertyChanged();
            }
        }
        public float Saturation
        {
            get { return Color.GetSaturation(); }
            set
            {
                Color.ToHsl(out float h, out float s, out float v);
                
                UpdateRGB(h,value, v);
                OnPropertyChanged();
            }
        } 
        public float Value
        {
            get { return Color.GetValue(); }
            set
            {
                Color.ToHsl(out float h, out float s, out float v);
                
                UpdateRGB(h, s, value);
                OnPropertyChanged();
            }
        }

       public float Alpha
        {
            get { return ConvertToFloat(Color.Alpha); }
            set { _alpha = ConvertToByte(value); OnPropertyChanged(); }
        }

        private byte ConvertToByte(float v)
        {
            return (byte)((int)v);
        }
        private float ConvertToFloat(byte b)
        {
            return (float)b;
        }

        

        private void UpdateRGB(float h, float s, float v)
        {
            var col = SKColor.FromHsl(h, s, v);
            _red = col.Red;
            _green = col.Green;
            _blue = col.Blue;
  
        }
       

    }
}
