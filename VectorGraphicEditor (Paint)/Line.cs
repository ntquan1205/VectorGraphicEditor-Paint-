using System.Drawing;

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
        }
    }
}