using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabsCSharp
{
    class GraphBuilder
    {
        Graphics g;
        Bitmap buffer;
        Font font = new Font(FontFamily.GenericSansSerif, 10f);
        SolidBrush brush = new SolidBrush(Color.Black);
        Pen axisPen = new Pen(Color.Black, 3);
        Pen graphPen = new Pen(Color.Red, 2.5f);
        public int Width { get; set; }
        public int Height { get; set; }
        public int CellSize { get; set; }

        public GraphBuilder(int width, int height, int cellSize)
        {
            Width = width;
            Height = height;
            CellSize = cellSize;
            buffer = new Bitmap(width, height);
            g = Graphics.FromImage(buffer);
        }

        private void DrawNumbers()
        {
            Font digitFont = new Font(font, FontStyle.Bold);
            int numbersCountX = Width / CellSize * 2;
            int numbersCountY = Height / CellSize * 2;

            //draw negative numbers
            for (int i = numbersCountX / 2; i >= 1; i--)
            {
                g.DrawString($"{-i}", digitFont, brush, (Width / 2) - (i * CellSize * 2) - (CellSize - 5f), Height / 2 - 19.5f);
            }
            for (int i = 1; i != numbersCountX / 2; i++)
            {
                g.DrawString($"{i}", digitFont, brush, (Width / 2) + (i * CellSize * 2) - (CellSize - 9f), Height / 2 - 19.5f);
            }

            //draw positive numbers
            for (int i = numbersCountY / 2; i >= 1; i--)
            {
                g.DrawString($"{i}", digitFont, brush, Width / 2 + 9f, (Height / 2) - (i * CellSize * 2) - (CellSize - 9f));
            }
            for (int i = 1; i != numbersCountY / 2; i++)
            {
                g.DrawString($"{-i}", digitFont, brush, Width / 2 + 9f, (Height / 2) + (i * CellSize * 2) - (CellSize - 6.5f));
            }
        }
        private void DrawAxes()
        {
            //center
            g.FillEllipse(brush, Width / 2 - 4, Height / 2 - 4, 8, 8);
            g.DrawString("o", font, brush, Width / 2 + 2.5f, Height / 2 - 17.5f);
            //x axis
            g.DrawLine(axisPen, new Point(0, Height / 2), new Point(Width, Height / 2));
            g.DrawString("x", font, brush, Width - 13.75f, Height / 2);
            //y axis
            g.DrawLine(axisPen, new Point(Width / 2, 0), new Point(Width / 2, Height));
            g.DrawString("y", font, brush, (Width - 27.5f) / 2, -2.5f);
        }
        private void DrawGrid()
        {
            for (int i = 0; i <= Width; i += CellSize)
            {
                for (int j = 0; j <= Height; j += CellSize)
                {
                    g.DrawLine(new Pen(Color.LightGray, 1), new Point(i, j), new Point(i, Height));
                }
                g.DrawLine(new Pen(Color.LightGray, 1), new Point(0, i), new Point(Width, i));
            }
        }
        public Image BuildGraph(string expression)
        {
            Reset();
            List<PointF> points = new List<PointF>();
            Argument x = new Argument("x");
            Argument y = new Argument(expression, x);

            for (float i = Width / 15 / 2 * -1; i < Width / 15; i += 0.1f)
            {
                x.setArgumentValue(i);
                PointF point = new PointF(i * 30 + Width / 2, (float)(Math.Round((double)(y.getArgumentValue() * -30 + Height / 2.0), 5)));
                if (point.Y.Equals(float.NaN) || point.Y < -200) continue;
                points.Add(point);
            }

            g.DrawLines(graphPen, points.ToArray());

            return buffer;
        }
        public Image Reset()
        {
            g.Clear(Color.White);
            DrawGrid();
            DrawAxes();
            DrawNumbers();

            return buffer;
        }
    }


}
