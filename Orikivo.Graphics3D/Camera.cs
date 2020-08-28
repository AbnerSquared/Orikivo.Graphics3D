using System;
using System.Collections.Generic;
using System.Drawing;
using Point = System.Drawing.Point;

namespace Orikivo.Graphics3D
{
    public class Camera
    {
        public Camera(int width, int height, float fov, float near, float far, Color backgroundColor)
        {
            Width = width;
            Height = height;
            Fov = fov;
            Near = near;
            Far = far;
            Position = Vector3.Zero;
            Rotation = Vector3.Zero;
            BackgroundColor = backgroundColor;
        }

        public Color BackgroundColor { get; set; }

        public Vector3 Position { get; set; }

        public Vector3 Rotation { get; set; }

        public float Fov { get; set; }

        public float Far { get; set; }

        public float Near { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Contains(Point p)
            => Contains(p.X, p.Y);

        public bool Contains(int x, int y)
            => Utils.RangeContains(0, Width, x, true, false) &&
               Utils.RangeContains(0, Height, y, true, false);

        public List<Point> Render(Point a, Point b)
            => Render(a.X, a.Y, b.X, b.Y);

        public List<Point> Render(int x1, int y1, int x2, int y2)
        {
            var points = new List<Point>();
            int x, y;

            int dx = x2 - x1;
            int dy = y2 - y1;
            int run = Math.Abs(dx);
            int rise = Math.Abs(dy);

            int px = 2 * rise - run;
            int py = 2 * run - rise;

            bool both = (dx < 0 && dy < 0) || (dx > 0 && dy > 0);

            if (rise <= run)
            {
                x = dx >= 0 ? x1 : x2;
                y = dx >= 0 ? y1 : y2;
                int endX = dx >= 0 ? x2 : x1;

                if (Contains(x, y))
                    points.Add(new Point(x, y));

                while (x < endX)
                {
                    x++;

                    if (px < 0)
                    {
                        px += 2 * rise;
                    }
                    else
                    {
                        y += both ? 1 : -1;
                        px += 2 * (rise - run);
                    }

                    if (Contains(x, y))
                        points.Add(new Point(x, y));
                }
            }
            else
            {
                x = dy >= 0 ? x1 : x2;
                y = dy >= 0 ? y1 : y2;
                int endY = dy >= 0 ? y2 : y1;

                if (Contains(x, y))
                    points.Add(new Point(x, y));

                while (y < endY)
                {
                    y++;

                    if (py <= 0)
                    {
                        py += 2 * run;
                    }
                    else
                    {
                        x += both ? 1 : -1;
                        py += 2 * (run - rise);
                    }

                    if (Contains(x, y))
                        points.Add(new Point(x, y));
                }
            }

            return points;
        }

        public List<Point> Render(Triangle t)
        {
            var a = new Point((int)MathF.Round(t.A.X), (int)MathF.Round(t.A.Y));
            var b = new Point((int)MathF.Round(t.B.X), (int)MathF.Round(t.B.Y));
            var c = new Point((int)MathF.Round(t.C.X), (int)MathF.Round(t.C.Y));

            List<Point> points = Render(a.X, a.Y, b.X, b.Y);
            points.AddRange(Render(b.X, b.Y, c.X, c.Y));
            points.AddRange(Render(c.X, c.Y, a.X, a.Y));

            return points;
        }
    }
}
