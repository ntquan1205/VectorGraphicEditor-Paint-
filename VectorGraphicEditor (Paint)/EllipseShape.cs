using System.Drawing;

namespace VectorGraphicEditor__Paint_
{
    public class EllipseShape : Shape
    {
        public Rectangle Bounds { get; set; }

        public EllipseShape(Rectangle bounds, Color color, float width)
        {
            Bounds = bounds;
            Color = color;
            PenWidth = width;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, PenWidth))
            {
                g.DrawEllipse(pen, Bounds);
            }
        }
    }
}