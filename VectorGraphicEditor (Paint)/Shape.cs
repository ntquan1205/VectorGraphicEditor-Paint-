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

        protected void DrawSelection(Graphics g)
        {
            if (IsSelected)
            {
                using (Pen selectionPen = new Pen(Color.Blue, 1))
                {
                    selectionPen.DashStyle = DashStyle.Dash;
                    selectionPen.DashPattern = new float[] { 3, 3 };
                    var path = GetPath();
                    g.DrawPath(selectionPen, path);
                }
            }
        }
    }
}