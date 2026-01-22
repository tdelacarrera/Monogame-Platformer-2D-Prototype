using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public class Coin
    {
        private Texture2D Texture { get; set; }
        private Vector2 Position { get; set; }
        public bool IsPicked { get; set; } = false;

        public Coin(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height
            );
        }

        public void Remove()
        {
            throw new System.NotImplementedException();
        }
    }
}
