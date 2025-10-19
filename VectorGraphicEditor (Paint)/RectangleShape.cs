using System.Drawing;

namespace VectorGraphicEditor__Paint_
{
    public class RectangleShape : Shape
    {
        public Rectangle Bounds { get; set; }

        public RectangleShape(Rectangle bounds, Color color, float width)
        {
            Bounds = bounds;
            Color = color;
            PenWidth = width;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color, PenWidth))
            {
                g.DrawRectangle(pen, Bounds);
            }
        }
    }
}