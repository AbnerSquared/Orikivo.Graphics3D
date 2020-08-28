using System.Drawing;

namespace Orikivo.Graphics3D
{
    public abstract class Rasterizer
    {
        public abstract Color[,] Render(in Model model, Camera camera, Color color);

        protected virtual Triangle ApplyTransform(in Triangle t, Transform transform)
        {
            var r = new Triangle(t.Points);

            r.A = Vector3.Transform(r.A, transform);
            r.B = Vector3.Transform(r.B, transform);
            r.C = Vector3.Transform(r.C, transform);

            return r;
        }

        protected virtual Triangle ApplyProjection(in Triangle t, MatrixF projector, int width, int height)
        {
            var r = new Triangle(t.Points);

            r.A = Vector3.Project(r.A, projector, width, height);
            r.B = Vector3.Project(r.B, projector, width, height);
            r.C = Vector3.Project(r.C, projector, width, height);

            return r;
        }
    }
}
