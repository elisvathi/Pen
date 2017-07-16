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

        public void DrawPath(SKPath path)
        {

        }
        public void DrawPath(List<PVector> points, SKCanvas canv)
        {
            int drawnPoints = 0;
            var requiredLength = _BrushInfoService.XSpacing;
            var actualPoint = points[0];
            float actualLength = 0;

            for (int i = 1; i < points.Count; i++)
            {
                var vecDist = PVector.DistanceBetween(points[i], actualPoint);
                if (vecDist + actualLength < requiredLength)
                {
                    if (i < points.Count - 1) {
                        actualLength += vecDist.ToFloat();
                        actualPoint = points[i];
                    }
                    else
                    {
                        DrawPoint(points[i], PVector.Add(PVector.Sub(points[i], points[i - 1]), points[i]), canv, drawnPoints);
                        drawnPoints++;
                    }

                }
                else if (vecDist + actualLength > requiredLength)
                {
                    float d = requiredLength - actualLength;
                    PVector p = PVector.Sub(points[i], points[i - 1]);
                    while (d <= vecDist)
                    {
                        var p1 = p.Copy();
                        p1.SetMag(d);
                        p1.Add(actualPoint);
                        DrawPoint(p1, points[i], canv, drawnPoints);
                        drawnPoints++;
                        d += requiredLength;
                    }
                    p.SetMag(d);
                    p.Add(points[i - 1]);
                    actualPoint = p;
                    actualLength = vecDist.ToFloat() - d;

                }
                else {
                    if (i < points.Count - 1) { DrawPoint(points[i], points[i + 1], canv, drawnPoints); drawnPoints++; } else
                    {
                        DrawPoint(points[i], PVector.Add(PVector.Sub(points[i], points[i - 1]), points[i]), canv, drawnPoints); drawnPoints++;
                    }
                    actualPoint = points[i];
                    actualLength = 0;
                }
            }
        }

        public static SKPath GetPath(List<PVector> points)
        {
            var d = new List<SKPoint>();
            foreach (var t in points)
            {
                d.Add(t.ToSKPoint());
            }
            return GetPath(d.ToArray());
        }

        public static SKPath GetPath(SKPoint[] points)
        {
            var retVal = new SKPath();
            for (int i = 0; i < points.Length; i += 2) {
                if (i + 2 < points.Length)
                {
                    retVal.CubicTo(points[i], points[i + 1], points[i + 2]);
                }
                else if (i + 1 < points.Length)
                {
                    retVal.ConicTo(points[i], points[i + 1], 0.33F);
                }


            }
            return retVal;
        }
        public static List<PVector> GetPointsFromPath(SKPath path)
        {
            var retVal = new List<PVector>();
            var it = path.CreateRawIterator();
           
            
            return retVal;
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
                BlendMode = SKBlendMode.Multiply
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

        public void DrawPoint(PVector p, PVector next, SKCanvas canv, int n)
        {
            var newPosition = GetNewDrawingPoint(p, next,n);
            var newOpacity = GetNewOpacity(n);
            var newScale = GetNewScale(n);
            var col = GetNewColor(n);
            var hardness = GetNewHardness(n);
            SKPaint shad = GetRadialPaint(col, newPosition, newScale, newOpacity, hardness);
            canv.DrawCircle(newPosition.X.ToFloat(), newPosition.Y.ToFloat(), newScale, shad);
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
