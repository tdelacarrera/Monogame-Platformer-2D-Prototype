using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public interface ICamera
    {
        float zoom { get; set; }
        Matrix TransformMatrix { get; }
        Vector2 position { get; set; }
        Viewport viewport { get; set; }
        float lerpSpeed { get; set; }
        Vector2 Dimensions { get; set; }

        void Update(Vector2 targetPosition);
        void UpdateViewMatrix();
    }
}
