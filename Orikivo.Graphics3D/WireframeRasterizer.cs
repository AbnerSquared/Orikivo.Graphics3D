using System.Drawing;

namespace Orikivo.Graphics3D
{
    public class WireframeRasterizer : Rasterizer
    {
        public override Color[,] Render(in Model model, Camera camera, Color color)
        {
            var frame = new Color[camera.Height, camera.Width];

            for (int y = 0; y < camera.Height; y++)
            for (int x = 0; x < camera.Width; x++)
                frame[y, x] = camera.BackgroundColor;

            var projector = MatrixF.CreateProjector(camera.Near, camera.Far, camera.Fov, camera.Width / (float) camera.Height);

            foreach (Triangle triangle in model.Mesh.Triangles)
            {
                Triangle t = ApplyTransform(triangle, model.Transform);
                Triangle p = ApplyProjection(t, projector, camera.Width, camera.Height);

                foreach (Point v in camera.Render(p))
                    frame.SetValue(color, v.X, v.Y);
            }

            return frame;
        }
    }
}
