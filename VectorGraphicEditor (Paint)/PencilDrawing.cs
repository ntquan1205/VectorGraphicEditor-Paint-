using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorGraphicEditor__Paint_
{
    public class PencilDrawing : Shape
    {
        private List<Point> Points { get; set; }

        public PencilDrawing(Color color, float width)
        {
            Points = new List<Point>();
            Color = color;
            PenWidth = width;
        }

        public void AddPoint(Point point)
        {
            Points.Add(point);
        }

        public override void Draw(Graphics g)
        {
            if (Points.Count < 2) return;

            using (Pen pen = new Pen(Color, PenWidth))
            {
                for (int i = 1; i < Points.Count; i++)
                {
                    g.DrawLine(pen, Points[i - 1], Points[i]);
                }
            }

            DrawSelection(g);
        }

        public override bool Contains(Point point)
        {
            for (int i = 1; i < Points.Count; i++)
            {
                if (DistanceToLine(point, Points[i - 1], Points[i]) < 10)
                    return true;
            }
            return false;
        }

        public override void Move(int deltaX, int deltaY)
        {
            for (int i = 0; i < Points.Count; i++)
            {
                Points[i] = new Point(Points[i].X + deltaX, Points[i].Y + deltaY);
            }
        }

        public override GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            if (Points.Count > 1)
            {
                path.AddLines(Points.ToArray());
            }
            return path;
        }

        private float DistanceToLine(Point point, Point lineStart, Point lineEnd)
        {
            float A = point.X - lineStart.X;
            float B = point.Y - lineStart.Y;
            float C = lineEnd.X - lineStart.X;
            float D = lineEnd.Y - lineStart.Y;

            float dot = A * C + B * D;
            float len_sq = C * C + D * D;
            float param = (len_sq != 0) ? dot / len_sq : -1;

            float xx, yy;

            if (param < 0)
            {
                xx = lineStart.X;
                yy = lineStart.Y;
            }
            else if (param > 1)
            {
                xx = lineEnd.X;
                yy = lineEnd.Y;
            }
            else
            {
                xx = lineStart.X + param * C;
                yy = lineStart.Y + param * D;
            }

            float dx = point.X - xx;
            float dy = point.Y - yy;
            return (float)System.Math.Sqrt(dx * dx + dy * dy);
        }
    }
}