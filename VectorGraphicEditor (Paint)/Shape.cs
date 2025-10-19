using System.Drawing;

namespace VectorGraphicEditor__Paint_
{
    public abstract class Shape
    {
        public Color Color { get; set; }
        public float PenWidth { get; set; }
        public abstract void Draw(Graphics g);
    }
}
