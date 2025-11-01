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
                var path = GetPath();

                using (Pen outerSelectionPen = new Pen(Color.Blue, 3))
                {
                    outerSelectionPen.DashStyle = DashStyle.Dash;
                    outerSelectionPen.DashPattern = new float[] { 5, 5 };
                    g.DrawPath(outerSelectionPen, path);
                }

                using (Pen innerSelectionPen = new Pen(Color.White, 1))
                {
                    innerSelectionPen.DashStyle = DashStyle.Dash;
                    innerSelectionPen.DashPattern = new float[] { 5, 5 };
                    g.DrawPath(innerSelectionPen, path);
                }

            }
        }

    
    }
}