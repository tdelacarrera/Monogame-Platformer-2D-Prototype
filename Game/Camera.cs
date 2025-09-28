using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public class Camera : ICamera
    {
        public float zoom { get; set; } = 2.8f;
        public Matrix TransformMatrix { get; private set; }
        public Vector2 position { get; set; }
        public Viewport viewport { get; set; }
        public float lerpSpeed { get; set; } = 0.1f;
        public Vector2 Dimensions { get; set; }

        public Camera(Vector2 dimensions)
        {
            viewport = Globals.GraphicsDevice.Viewport;
            Dimensions = dimensions;
        }

        public void Update(Vector2 targetPosition)
        {
            Vector2 p = Vector2.Lerp(position, targetPosition, lerpSpeed);

            p.X = MathHelper.Clamp(p.X, viewport.Width / (2 * zoom), Dimensions.X - viewport.Width / (2 * zoom));
            p.Y = MathHelper.Clamp(p.Y, viewport.Height / (2 * zoom), Dimensions.Y - viewport.Height / (2 * zoom));

            position = p;
        }
        

        public void UpdateViewMatrix()
        {
            TransformMatrix =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateScale(zoom) *
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
    }
}
