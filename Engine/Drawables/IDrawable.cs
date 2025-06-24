using Microsoft.Xna.Framework.Graphics;

namespace Engine.Drawing;

public interface IDrawable
{
    void Draw(SpriteBatch spriteBatch);
}