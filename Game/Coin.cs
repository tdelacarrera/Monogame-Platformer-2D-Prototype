using Engine;
using Engine.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.Entities
{
    public class Coin : Entity, IPickable
    {

        public Coin(Vector2 position)
        {
            Position = position;
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("coin");
            Group = "coin";
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public void Pickup()
        {
            GameObjectManager.Remove(this);
        }
    }
}
