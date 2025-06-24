using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Engine.Rendering;

namespace Engine.Entity
{
    public abstract class GameObject : IRenderable, IUpdatable
{
    public Vector2 Position { get; set; }

    public abstract void LoadContent(ContentManager content);

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch gameTime);
}
}
