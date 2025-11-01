using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorGraphicEditor__Paint_
{
    public class EllipseShape : Shape
    {
        private Rectangle Bounds { get; set; }

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
            DrawSelection(g);
        }

        public override bool Contains(Point point)
        {
            Rectangle expandedBounds = new Rectangle(
                Bounds.X - 5,
                Bounds.Y - 5,
                Bounds.Width + 10,
                Bounds.Height + 10);
            return expandedBounds.Contains(point);
        }

        public override void Move(int deltaX, int deltaY)
        {
            Bounds = new Rectangle(
                Bounds.X + deltaX,
                Bounds.Y + deltaY,
                Bounds.Width,
                Bounds.Height);
        }

        public override GraphicsPath GetPath()
        {
            var path = new GraphicsPath();
            path.AddEllipse(Bounds);
            return path;
        }
    }
}