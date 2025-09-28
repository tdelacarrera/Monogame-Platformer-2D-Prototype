using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public class Coin : IPickable
    {
        public Texture2D Texture { get; set; }
        public string Group { get; set; }
        public Vector2 Position { get; set; }
        public bool IsPicked { get; set; } = false;

        public Coin(Vector2 position)
        {
            Position = position;
        }
        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("coin");
            Group = "coin";
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void Pickup()
        {
            IsPicked = true;
        }
        public Rectangle GetBounds()
        {
            if (Texture == null)
                return Rectangle.Empty;

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
