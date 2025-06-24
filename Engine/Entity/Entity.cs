using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Entity
{
    public abstract class Entity : Engine.Drawing.IDrawable, Engine.IUpdatable
{
    protected Texture2D texture;
    public Vector2 Position { get; set; }
    public string Group { get; set; } = "empty";

    public Rectangle Bounds
    {
        get { return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height); }
    }

    public abstract void LoadContent(ContentManager content);

    public abstract void Update(GameTime gameTime);

    public abstract void Draw(SpriteBatch gameTime);
}
}
