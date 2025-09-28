using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public interface IGameEntity
    {
        Texture2D Texture { get; set; }
        string Group { get; set; }
        Vector2 Position { get; set; }
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
        public Rectangle GetBounds();
    }
}
