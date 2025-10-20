using System.Drawing;

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
    }
}
