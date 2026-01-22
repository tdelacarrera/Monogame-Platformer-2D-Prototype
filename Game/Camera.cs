using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public class Camera
    {
        private float Zoom { get; set; } = 2.8f;
        public Matrix TransformMatrix { get; private set; }
        private Vector2 Position { get; set; }
        private Viewport Viewport { get; set; }
        private float LerpSpeed { get; set; } = 0.1f;
        private Vector2 Dimensions { get; set; }

        public Camera(Vector2 dimensions, Viewport viewport)
        {
            Viewport = viewport;
            Dimensions = dimensions;
        }

        public void Update(Vector2 targetPosition)
        {
            Vector2 p = Vector2.Lerp(Position, targetPosition, LerpSpeed);

            p.X = MathHelper.Clamp(p.X, Viewport.Width / (2 * Zoom), Dimensions.X - Viewport.Width / (2 * Zoom));
            p.Y = MathHelper.Clamp(p.Y, Viewport.Height / (2 * Zoom), Dimensions.Y - Viewport.Height / (2 * Zoom));

            Position = p;
        }
        
        public void UpdateViewMatrix()
        {
            TransformMatrix =
                Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateScale(Zoom) *
                Matrix.CreateTranslation(new Vector3(Viewport.Width / 2, Viewport.Height / 2, 0));
        }
    }
}
