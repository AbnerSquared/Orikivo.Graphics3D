using System.Collections.Generic;
using System.Drawing;

namespace Orikivo.Graphics3D
{
    public class Scene
    {
        public Scene(int width, int height, float fov, float near, float far, Color backgroundColor, Model model, Rasterizer rasterizer)
        {
            Camera = new Camera(width, height, fov, near, far, backgroundColor);
            Rasterizer = rasterizer;
            Model = model;
        }

        public Scene(Camera camera, Model model, Rasterizer rasterizer)
        {
            Camera = camera;
            Rasterizer = rasterizer;
            Model = model;
        }

        public Camera Camera { get; set; }

        public Rasterizer Rasterizer { get; }

        public Model Model { get; set; }

        public List<Color[,]> GetFrames(long ticks, Color color, Vector3? position = null,
            Vector3? rotation = null, Vector3? velocity = null)
        {
            var frames = new List<Color[,]>();

            Model.Transform.Position.Offset(position.GetValueOrDefault(Vector3.Zero));
            Vector3 pos = Vector3.Zero;
            Vector3 rot = Vector3.Zero;

            pos.Offset(position.GetValueOrDefault(Vector3.Zero));

            for (long t = 0; t < ticks; t++)
            {
                rot.Offset(rotation.GetValueOrDefault(Vector3.Zero));
                rot.Modulo(360.0f);

                pos.Offset(velocity.GetValueOrDefault(Vector3.Zero));

                frames.Add(GetFrame(Model.Mesh, pos, rot, color));
            }

            return frames;
        }

        public Color[,] GetFrame(Mesh mesh, Transform transform, Color color)
            => GetFrame(new Model(mesh, transform), color);

        public Color[,] GetFrame(Mesh mesh, Vector3 position, Vector3 rotation, Color color)
            => GetFrame(new Model(mesh, new Transform(position, rotation)), color);

        public Color[,] GetFrame(Model model, Color color)
            => Rasterizer.Render(model, Camera, color);
    }
}
