using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Rendering;

namespace Engine.Entity
{
    public abstract class Entity : GameObject, IRenderable, IUpdatable, ILoadable
    {
        protected Texture2D texture;
        public string Group { get; set; } = "empty";

        public Rectangle Bounds
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height); }
        }
    }
    
}
