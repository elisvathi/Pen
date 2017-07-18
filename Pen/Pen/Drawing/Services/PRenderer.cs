using Pen.Geometry;
using Pen.Layering;
using Pen.LibraryExtensions;
using Pen.MathExtenions;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen.Drawing.Services
{
    public class PRenderer
    {
        //private Random rnd = new Random();
        private LayerManager _layerManager;
        private DrawingConfigService _BrushInfoService;
        public PRenderer(LayerManager lm, DrawingConfigService serv) { _layerManager = lm; _BrushInfoService = serv; }
        private SKCanvas ActiveCanvas { get { return _layerManager.CanvasToDraw; } }
        public void DrawLine(PVector start, PVector end)
        {
            DrawLine(start, end, _BrushInfoService.SPaint);
        }
        public void DrawLine(PVector start, PVector end, SKPaint p)
        {
            ActiveCanvas.DrawLine(start.X.ToFloat(), start.Y.ToFloat(), end.X.ToFloat(), end.Y.ToFloat(), p);
        }

      

        private PVector GetNewDrawingPoint(PVector p, PVector next, int n)
        {
            var rnd = new Random(n*10);
            var rndx2 = new Random(n * 452);
            var rndy = new Random(n * 70);
            var rndy2 = new Random(n * 750);
            PVector direction = PVector.Sub(next, p);
            PVector newPosition = direction.Copy();
            newPosition.SetMag((GetRandomized(rnd, rndx2) - 0.5) * _BrushInfoService.XDispersion);
            PVector ymov = direction.Copy();
            ymov.RotateDegrees(90);
            ymov.SetMag((GetRandomized(rndy, rndy2) - 0.5) * _BrushInfoService.YDispersion);
            newPosition.Add(ymov);
            newPosition.Add(p);
            return newPosition;
        }
        private float GetNewOpacity(int n)
        {
            var rnd = new Random(n*10);
            float actualOpacity = _BrushInfoService.StrokeColor.Alpha;
            float opacityDispersion = _BrushInfoService.OpacityDispersion;
            float newOpacity = Clamp((rnd.NextDouble().ToFloat() - 0.5F) * opacityDispersion + actualOpacity, 0, 255);
            return newOpacity;
        }
        private float GetNewScale(int n)
        {
            var rnd = new Random(n*1520);
            var rnd2 = new Random(n * 242);
            float disp = _BrushInfoService.ScaleDispersion;
            float koef = GetRandomized(rnd, rnd2).ToFloat();
            var actualScale = _BrushInfoService.S_Width;
            float minimum = 0;
            float maximum = actualScale * 2;
            float consideration = disp.ToDouble().Map(0, 100, 0, 1).ToFloat();
            consideration *= koef;
            double newVal = 1- consideration;
            var retVal = newVal.Map(0, 1, minimum, maximum);
            return retVal.ToFloat();
            //float scaleDifference = (rnd.NextDouble().ToFloat() - 0.5F) * newScale;
            //newScale.ToDouble().Map(-100, 100, -1, 1);
            //newScale = _BrushInfoService.S_Width + _BrushInfoService.S_Width*newScale ;
            /*return d.ToFloat()*/;
        }
        private double GetRandomized(Random r1, Random r2)
        {
            return r1.NextDouble() * r2.NextDouble();
        }
        private SKColor GetNewColor(int n)
        {
            var rnd = new Random(n*10);
            
            _BrushInfoService.StrokeColor.ToHsl(out float h, out float s, out float v);
            h = (h + _BrushInfoService.HueDispersion * (rnd.NextDouble().ToFloat() - 0.5F)).ToDouble().FixAngleDegrees().ToFloat();
            s = Clamp(s + _BrushInfoService.SaturationDispersion * (rnd.NextDouble().ToFloat() - 0.5F), 0, 100);
            v = Clamp(v + _BrushInfoService.SaturationDispersion * (rnd.NextDouble().ToFloat() - 0.5F), 0, 100);
            SKColor col = SKColor.FromHsl(h, s, v);
            return col;
        }
        private float GetNewHardness(int n)
        {
            var rnd = new Random(n*10);
            float hardness = _BrushInfoService.Hardness;
            hardness += (rnd.NextDouble() - 0.5F).ToFloat() * _BrushInfoService.HardnessDispersion;
            hardness = Clamp(hardness, 0, 1);
            return hardness;
        }


        private SKPaint GetRadialPaint(SKColor col, PVector position, float radius, float alpha, float hardness)
        {
            var shad = GetRadialGradient(col, position, radius, alpha, hardness);
            return new SKPaint()
            {
                Style = SKPaintStyle.Fill,
                Shader = shad,
                BlendMode = _BrushInfoService.BlendMode
            };
        }
        private SKShader GetRadialGradient(SKColor col, PVector position, float radius, float alpha, float hardness)
        {
            var cols = new SKColor[3];
            cols[0] = new SKColor(col.Red, col.Green, col.Blue, Convert(alpha));
            cols[1] = cols[0];
            cols[2] = new SKColor(col.Red, col.Green, col.Blue, 0);
            var positions = new float[3] { 0, hardness, 1 };
            var p = position.ToSKPoint();
            return SKShader.CreateRadialGradient(p, radius, cols, positions, SKShaderTileMode.Clamp);
        }

        public void DrawPoint(PVector p, PVector next, SKCanvas canv, int n, float koef=1)
        {
            var npos = GetNewDrawingPoint(p, next,n);
            var newPosition = PVector.Sub(npos, p);
            newPosition.Mult(koef);
            newPosition.Add(p);
            
            var newOpacity = GetNewOpacity(n) * koef;
            var newScale = GetNewScale(n)*koef;
            var col = GetNewColor(n);
            var hardness = GetNewHardness(n);
            SKPaint shad = GetRadialPaint(col, newPosition, newScale, newOpacity, hardness);
            canv.DrawCircle(newPosition.X.ToFloat(), newPosition.Y.ToFloat(), newScale, shad);
        }

        public void DrawCurve(PCurve curve, SKCanvas canv) {
            int drawnPoints = 0;
            var step = _BrushInfoService.XSpacing;
            var div = curve.DivideLength(step);
           
            float len = 0;
            for(int i = 0;i< div.Count - 1; i++)
            {

                DrawPoint(div[i], div[i + 1], canv, drawnPoints, CurvePositionKoeficient(len, curve.Length));
                drawnPoints++;
                len += PVector.DistanceBetween(div[i], div[i + 1]).ToFloat();
            }
            PVector vec = PVector.Sub(div.Last(), div.Count>1?div[div.Count - 2]:PVector.Add(div.Last(), new PVector(0,1)));
            vec.Add(div.Last());
            DrawPoint(div.Last(), vec, canv, drawnPoints, CurvePositionKoeficient(curve.Length, curve.Length));
            drawnPoints++;
        }
       
        
        public void DrawCurve(PCurve curve) {
            DrawCurve(curve, ActiveCanvas);
        }

        float CurvePositionKoeficient(float pos, float totalLength)
        {
            float startingLength = _BrushInfoService.StartingLength;
            float endingLength = _BrushInfoService.EndingLength;
            if (totalLength < startingLength + endingLength)
            {
                float startingDist = (startingLength / (startingLength + endingLength)) * totalLength;
                float endingDist = (endingLength / (startingLength + endingLength)) * totalLength;
                if (pos <= startingDist) { return pos.ToDouble().Map(0, startingDist, 0, startingDist / startingLength).ToFloat(); }
                else { return (totalLength - pos).ToDouble().Map(0, endingDist, 0, endingDist / endingLength).ToFloat(); }
            } else
            {
                if (pos <= startingLength) { return pos / startingLength; } else if (pos >= totalLength - endingLength) { return (totalLength - pos).ToDouble().Map(0, endingLength, 0, 1).ToFloat(); } else { return 1; }
            }
        }

        private float Clamp(float value, float start, float end)
        {
            if (value < start) { return start; } else if (value > end) { return end; }
            return value;
        }


        private byte Convert(float a)
        {
            return (byte)((int)a);
        }

    }
}
