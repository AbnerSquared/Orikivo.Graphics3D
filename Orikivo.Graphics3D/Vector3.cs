using System;
using static System.MathF;

namespace Orikivo.Graphics3D
{
    public struct Vector3
    {
        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);

        public static Vector3 Subtract(Vector3 a, Vector3 b)
        {
            Vector3 v = Zero;
            v.X = a.X - b.X;
            v.Y = a.Y - b.Y;
            v.Z = a.Z - b.Z;

            return v;
        }

        public static Vector3 Multiply(Vector3 v, MatrixF m)
        {
            Vector3 n = Zero;

            n.X = (v.X * m[0, 0]) + (v.Y * m[1, 0]) + (v.Z * m[2, 0]) + m[3, 0];
            n.Y = (v.X * m[0, 1]) + (v.Y * m[1, 1]) + (v.Z * m[2, 1]) + m[3, 1];
            n.Z = (v.X * m[0, 2]) + (v.Y * m[1, 2]) + (v.Z * m[2, 2]) + m[3, 2];

            float w = (v.X * m[0, 3]) + (v.Y * m[1, 3]) + (v.Z * m[2, 3]) + m[3, 3];

            if (Math.Abs(w) > 0.001f)
            {
                n.X /= w;
                n.Y /= w;
                n.Z /= w;
            }

            return n;
        }

        public static Vector3 Multiply(Vector3 a, Vector3 b)
        {
            Vector3 c = Zero;

            c.X = a.X * b.X;
            c.Y = a.Y * b.Y;
            c.Z = a.Z * b.Z;

            return c;
        }

        public static Vector3 Normal(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 u = Subtract(b, a);
            Vector3 v = Subtract(c, a);
            Vector3 n = CrossProduct(u, v);

            n = Normalize(n);

            return n;
        }

        public static Vector3 Normalize(Vector3 p)
        {
            Vector3 n = p;

            float len = Sqrt(Dist(p));

            n.X /= len;
            n.Y /= len;
            n.Z /= len;

            return n;
        }

        public static float DotProduct(Vector3 a, Vector3 b)
            => (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);

        public static float Dist(Vector3 v)
            => (v.X * v.X) + (v.Y * v.Y) + (v.Z * v.Z);

        public static Vector3 CrossProduct(Vector3 a, Vector3 b)
        {
            Vector3 n = Zero;
            n.X = (a.Y * b.Z) - (a.Z - b.Y);
            n.Y = (a.Z * b.X) - (a.X * b.Z);
            n.Z = (a.X * b.Y) - (a.Y * b.X);

            return n;
        }
        public static Vector3 Transform(in Vector3 p, Transform transform)
            => Transform(p, transform.Position, transform.Rotation, transform.Scale);

        public static Vector3 Transform(in Vector3 p, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            Vector3 v = p;

            v = Multiply(v, scale);

            var rotate = new RotationMatrix(rotation);

            Vector3 x = Multiply(v, rotate.X);
            Vector3 xy = Multiply(x, rotate.Y);
            Vector3 xyz = Multiply(xy, rotate.Z);
            v = xyz;

            v.Offset(position);

            return v;
        }

        public static Vector3 Project(in Vector3 t, MatrixF projector, int width, int height)
        {
            Vector3 p = Multiply(t, projector);
            Vector3 n = Zero;

            n.X = Utils.RangeConvert(-1, 1, 0, width - 1, p.X);
            n.Y = Utils.RangeConvert(-1, 1, 0, height - 1, p.Y);
            n.Z = p.Z;

            return n;
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public void Offset(Vector3 offset)
        {
            X += offset.X;
            Y += offset.Y;
            Z += offset.Z;
        }

        public void Modulo(float max)
        {
            X %= max;
            Y %= max;
            Z %= max;
        }

        public override string ToString()
            => $"({X}, {Y}, {Z})";
    }
}
