using System.Drawing;

namespace VectorGraphicEditor__Paint_
{
    public class FillArea : Shape
    {
        public Point FillPoint { get; set; }
        public Bitmap CanvasBitmap { get; set; }

        public FillArea(Point fillPoint, Color color, Bitmap canvasBitmap)
        {
            FillPoint = fillPoint;
            Color = color;
            CanvasBitmap = canvasBitmap;
            PenWidth = 1; // Для заливки ширина не важна
        }

        public override void Draw(Graphics g)
        {
            // Заливка уже применена к bitmap, поэтому просто рисуем bitmap
            g.DrawImage(CanvasBitmap, 0, 0);
        }

        public override bool Contains(Point point)
        {
            // Для заливки проверяем цвет пикселя
            if (point.X >= 0 && point.X < CanvasBitmap.Width &&
                point.Y >= 0 && point.Y < CanvasBitmap.Height)
            {
                return CanvasBitmap.GetPixel(point.X, point.Y) == Color;
            }
            return false;
        }

        public override void Move(int deltaX, int deltaY)
        {
            // Заливку нельзя перемещать
            return;
        }
    }
}