using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorGraphicEditor__Paint_
{
    public abstract class Shape
    {
        public Color Color { get; set; }
        public float PenWidth { get; set; }
        public bool IsSelected { get; set; }
        public abstract void Draw(Graphics g);
        public abstract bool Contains(Point point);
        public abstract void Move(int deltaX, int deltaY);
        public abstract GraphicsPath GetPath();

        protected virtual void DrawSelection(Graphics g)
        {
            if (IsSelected)
            {
                using (var pen = new Pen(Color.Blue, 1) { DashStyle = DashStyle.Dash })
                {
                    var path = GetPath();
                    g.DrawPath(pen, path);
                }
            }
        }
    }
}