using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Pen.UI.Helpers
{
   public class BackgroundImage
    {
        public SKBitmap bmp;
       public BackgroundImage()
        {
            var assembly = GetType().GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("Pen.bg.bmp");
            var skStream = new SKManagedStream(stream);
           bmp = SKBitmap.Decode(skStream);
            //shader = SKShader.CreateBitmap(bmp, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat);
        }
        public SKShader GetShader(SKBitmap b)
        {
            return SKShader.CreateBitmap(b, SKShaderTileMode.Repeat, SKShaderTileMode.Repeat);
        }
        public SKShader GetShader()
        {
            return GetShader(bmp);
        }
        public SKShader GetShader(int widht, int height)
        {
            var newbmp = new SKBitmap(widht, height);
            SKBitmap.Resize(newbmp, bmp, SKBitmapResizeMethod.Lanczos3);
            return GetShader(newbmp);
        }

      
    }
}
