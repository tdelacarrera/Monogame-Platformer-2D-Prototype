using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//TODO: REMOVE GLOBALS

namespace Monogame
{
    public static class Globals
    {
        public static float DeltaTime { get; set; }
        public static int ScreenW { get; set; } = 1200;
        public static int ScreenH { get; set; } = 600;

        public static bool Exit { get; set; } = false;

        public enum Scenes { Menu, Game, Settings }

        public static GraphicsDevice GraphicsDevice { get; set; }
        public static Scenes CurrentState { get; set; } = Scenes.Menu;

        public static void Update(GameTime gameTime)
        {
            DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
