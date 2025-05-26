using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.Managers
{

    public class ViewportManager
    {
        private float zoom = 2.8f;
        public Matrix TransformMatrix { get; protected set; }
        private Vector2 position;
        private Viewport viewport;
        private float lerpSpeed = 0.1f;
        private Vector2 Dimensions;

        public ViewportManager(Vector2 dimensions)
        {
            viewport = Globals.GraphicsDevice.Viewport;
            Dimensions = dimensions;
        }

        public void Update(Vector2 targetPosition)
        {
            position = Vector2.Lerp(position, targetPosition, lerpSpeed);
            position.X = MathHelper.Clamp(position.X, viewport.Width / (2 * zoom), Dimensions.X - viewport.Width / (2 * zoom));
            position.Y = MathHelper.Clamp(position.Y, viewport.Height / (2 * zoom), Dimensions.Y - viewport.Height / (2 * zoom));
            UpdateViewMatrix();
        }
        private void UpdateViewMatrix()
        {
            TransformMatrix =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateScale(zoom) *
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));

        }
    }
}
