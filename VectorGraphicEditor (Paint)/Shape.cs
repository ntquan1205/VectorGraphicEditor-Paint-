using System.Drawing;
using System.Drawing.Drawing2D;

namespace VectorGraphicEditor__Paint_
{
    public abstract class Shape
    {
        private Color _color;
        private float _penWidth;
        private bool _isSelected;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public float PenWidth
        {
            get { return _penWidth; }
            set { _penWidth = value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }
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