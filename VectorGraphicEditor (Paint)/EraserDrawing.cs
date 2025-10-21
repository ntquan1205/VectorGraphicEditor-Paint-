using System.Collections.Generic;
using System.Drawing;

namespace VectorGraphicEditor__Paint_
{
    public class EraserDrawing : Shape
    {
        public List<Point> Points { get; set; }
        public float EraserWidth { get; set; }

        public EraserDrawing(float width)
        {
            Points = new List<Point>();
            Color = Color.White; 
            EraserWidth = width;
            PenWidth = width;
        }

        public void AddPoint(Point point)
        {
            Points.Add(point);
        }

        public override void Draw(Graphics g)
        {
            if (Points.Count < 2) return;

            using (Pen erasePen = new Pen(Color, EraserWidth))
            {
                for (int i = 1; i < Points.Count; i++)
                {
                    g.DrawLine(erasePen, Points[i - 1], Points[i]);
                }
            }
        }

        public override bool Contains(Point point)
        {
            // Для ластика проверяем близость к любой из стертых линий
            for (int i = 1; i < Points.Count; i++)
            {
                if (DistanceToLine(point, Points[i - 1], Points[i]) < EraserWidth)
                    return true;
            }
            return false;
        }

        public override void Move(int deltaX, int deltaY)
        {
            // Ластик нельзя перемещать
            return;
        }

        private float DistanceToLine(Point point, Point lineStart, Point lineEnd)
        {
            float A = point.X - lineStart.X;
            float B = point.Y - lineStart.Y;
            float C = lineEnd.X - lineStart.X;
            float D = lineEnd.Y - lineStart.Y;

            float dot = A * C + B * D;
            float len_sq = C * C + D * D;
            float param = dot / len_sq;

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