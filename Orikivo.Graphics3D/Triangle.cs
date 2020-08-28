namespace Orikivo.Graphics3D
{
    public readonly struct Triangle
    {
        public Triangle(Vector3[] points)
        {
            Points = points;
        }

        public Triangle(Vector3 a, Vector3 b, Vector3 c)
        {
            Points = new[] { a, b, c };
        }

        public Vector3[] Points { get; }

        public Vector3 A
        {
            get => Points[0];
            set => Points[0] = value;
        }

        public Vector3 B
        {
            get => Points[1];
            set => Points[1] = value;
        }

        public Vector3 C
        {
            get => Points[2];
            set => Points[2] = value;
        }

        public override string ToString()
            => $"(A{A.ToString()}, B{B.ToString()}, C{C.ToString()})";
    }
}
