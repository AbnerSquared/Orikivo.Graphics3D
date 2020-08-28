namespace Orikivo.Graphics3D
{
    public class Model
    {
        public Model(Mesh mesh, Transform transform = null)
        {
            Mesh = mesh;
            Transform = transform ?? Transform.Default;
        }

        public Mesh Mesh { get; }

        public Transform Transform { get; set; }
    }
}
