using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public abstract class Scene
    {
        public abstract void LoadContent(ContentManager Content);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spritebatch);
    }
}
