using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VectorGraphicEditor__Paint_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Width = 800;
            this.Height = 600;
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            pic.Image = bm;

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        Bitmap bm;
        Graphics g;
        bool paint = false;
        Point px, py;
        Pen p = new Pen(Color.Black, 2); 
        int index; 
        int x, y, sX, sY, cX, cY;
        ColorDialog cd = new ColorDialog();
        Color new_color;

        private List<Shape> shapes = new List<Shape>();

        private Shape selectedShape = null;
        private bool isMoving = false;
        private Point previousMousePosition;

        private PencilDrawing currentPencilDrawing = null;

        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            paint = true;
            py = e.Location;
            cX = e.X;
            cY = e.Y;

            if (index == 0) 
            {
                SelectShape(e.Location);
                if (selectedShape != null)
                {
                    isMoving = true;
                    previousMousePosition = e.Location;
                }
            }
            else if (index == 1) 
            {
                currentPencilDrawing = new PencilDrawing(p.Color, p.Width);
                currentPencilDrawing.AddPoint(e.Location);
                shapes.Add(currentPencilDrawing);
            }

        }

        private void pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (paint)
            {
                if (index == 1 && currentPencilDrawing != null) 
                {
                    currentPencilDrawing.AddPoint(e.Location);
                    RedrawCanvas();
                }
            }

            if (isMoving && selectedShape != null)
            {
                int deltaX = e.X - previousMousePosition.X;
                int deltaY = e.Y - previousMousePosition.Y;

                selectedShape.Move(deltaX, deltaY);
                previousMousePosition = e.Location;

                RedrawCanvas();
            }

            x = e.X;
            y = e.Y;
            sX = e.X - cX;
            sY = e.Y - cY;

            pic.Refresh();
        }

        private void pic_MouseUp(object sender, MouseEventArgs e)
        {
            paint = false;
            isMoving = false;

            sX = x - cX;
            sY = y - cY;

            if (index == 3) 
            {
                if (Math.Abs(sX) > 5 && Math.Abs(sY) > 5) 
                {
                    shapes.Add(new EllipseShape(new Rectangle(cX, cY, sX, sY), p.Color, p.Width));
                    RedrawCanvas();
                }
            }
            else if (index == 4) 
            {
                if (Math.Abs(sX) > 5 && Math.Abs(sY) > 5) 
                {
                    shapes.Add(new RectangleShape(new Rectangle(cX, cY, sX, sY), p.Color, p.Width));
                    RedrawCanvas();
                }
            }
            else if (index == 5) 
            {
                if (Math.Abs(sX) > 5 || Math.Abs(sY) > 5) 
                {
                    shapes.Add(new Line(new Point(cX, cY), new Point(x, y), p.Color, p.Width));
                    RedrawCanvas();
                }
            }

            currentPencilDrawing = null;
        }

        private void SelectShape(Point location)
        {
            foreach (var shape in shapes)
            {
                shape.IsSelected = false;
            }

            selectedShape = null;

            for (int i = shapes.Count - 1; i >= 0; i--)
            {
                if (shapes[i].Contains(location))
                {
                    shapes[i].IsSelected = true;
                    selectedShape = shapes[i];
                    break;
                }
            }

            RedrawCanvas(); 
        }

        private void DeleteSelectedShape()
        {
            if (selectedShape != null)
            {
                shapes.Remove(selectedShape);
                selectedShape = null;
                RedrawCanvas();
            }
        }

        private void RedrawCanvas()
        {
            bm = new Bitmap(pic.Width, pic.Height);
            g = Graphics.FromImage(bm);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }

            pic.Image = bm;
            pic.Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedShape();
            }
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            index = 0;
            selectedShape = null;
            isMoving = false;
        }

        private void btn_pencil_Click(object sender, EventArgs e)
        {
            index = 1;
            selectedShape = null;
            isMoving = false;
        }

        private void btn_ellipse_Click(object sender, EventArgs e)
        {
            index = 3;
            selectedShape = null;
            isMoving = false;
        }

        private void btn_rect_Click(object sender, EventArgs e)
        {
            index = 4;
            selectedShape = null;
            isMoving = false;
        }

        private void btn_line_Click(object sender, EventArgs e)
        {
            index = 5;
            selectedShape = null;
            isMoving = false;
        }

        private void pic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }

            if (paint)
            {
                using (Pen previewPen = new Pen(p.Color, p.Width))
                {
                    if (index == 3) 
                    {
                        g.DrawEllipse(previewPen, cX, cY, sX, sY);
                    }
                    else if (index == 4)
                    {
                        g.DrawRectangle(previewPen, cX, cY, sX, sY);
                    }
                    else if (index == 5) 
                    {
                        g.DrawLine(previewPen, cX, cY, x, y);
                    }
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            shapes.Clear();
            selectedShape = null;
            RedrawCanvas();
        }

        private void btn_color_Click(object sender, EventArgs e)
        {
            if (cd.ShowDialog() == DialogResult.OK)
            {
                new_color = cd.Color;
                pic_color.BackColor = cd.Color;
                p.Color = cd.Color;
            }
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            DeleteSelectedShape();
        }
 
    }
}