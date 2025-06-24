using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Engine
{
    public interface ILoadable
    {
        void LoadContent(ContentManager content);
    }
}