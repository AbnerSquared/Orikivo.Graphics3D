using System.Collections.Generic;

namespace Orikivo.Graphics3D
{
    public readonly struct Mesh
    {
        public static Mesh Cube
        {
            get
            {
                var p1 = new Vector3(0, 0, 0);
                var p2 = new Vector3(0, 1, 0);
                var p3 = new Vector3(1, 1, 0);
                var p4 = new Vector3(1, 0, 0);
                var p5 = new Vector3(0, 0, 1);
                var p6 = new Vector3(0, 1, 1);
                var p7 = new Vector3(1, 1, 1);
                var p8 = new Vector3(1, 0, 1);

                Vector3[] southUpper = { p1, p2, p3 };
                Vector3[] southLower = { p1, p3, p4 };
                Vector3[] eastUpper = { p4, p3, p7 };
                Vector3[] eastLower = { p4, p7, p8 };
                Vector3[] northUpper = { p8, p7, p6 };
                Vector3[] northLower = { p8, p6, p5 };
                Vector3[] westUpper = { p5, p6, p2 };
                Vector3[] westLower = { p5, p2, p1 };
                Vector3[] topUpper = { p2, p6, p7 };
                Vector3[] topLower = { p2, p7, p3 };
                Vector3[] bottomUpper = { p8, p5, p1 };
                Vector3[] bottomLower = { p8, p1, p4 };

                var triangles = new List<Triangle>
                {
                    new Triangle(southUpper),
                    new Triangle(southLower),
                    new Triangle(eastUpper),
                    new Triangle(eastLower),
                    new Triangle(northUpper),
                    new Triangle(northLower),
                    new Triangle(westUpper),
                    new Triangle(westLower),
                    new Triangle(topUpper),
                    new Triangle(topLower),
                    new Triangle(bottomUpper),
                    new Triangle(bottomLower)
                };

                return new Mesh(triangles);
            }
        }

        public Mesh(IEnumerable<Triangle> triangles)
        {
            Triangles = new List<Triangle>(triangles);
        }

        public List<Triangle> Triangles { get; }
    }
}
