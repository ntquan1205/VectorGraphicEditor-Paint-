using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorGraphicEditor__Paint_
{
    public class Line : Shape
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Line(Point start, Point end, Color color, float width)
        {
            StartPoint = start;
            EndPoint = end;
            Color = color;
            PenWidth = width;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, PenWidth))
            {
                g.DrawLine(pen, StartPoint, EndPoint);
            }

            DrawSelection(g);
        }

        public override bool Contains(Point point)
        {
            return DistanceToLine(point, StartPoint, EndPoint) < 10;
        }

        public override void Move(int deltaX, int deltaY)
        {
            StartPoint = new Point(StartPoint.X + deltaX, StartPoint.Y + deltaY);
            EndPoint = new Point(EndPoint.X + deltaX, EndPoint.Y + deltaY);
        }

        public override GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            path.AddLine(StartPoint, EndPoint);
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
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }
    }
}