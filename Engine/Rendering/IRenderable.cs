using Microsoft.Xna.Framework.Graphics;

namespace Engine.Rendering;

public interface IRenderable
{
    void Draw(SpriteBatch spriteBatch);
}