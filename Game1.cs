using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameStateManager gameStateManager;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Globals.CurrentState = Globals.Scenes.Game;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = Globals.ScreenW;
            graphics.PreferredBackBufferHeight = Globals.ScreenH;
            graphics.ApplyChanges();
            gameStateManager = new GameStateManager();
            Globals.GraphicsDevice = graphics.GraphicsDevice;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameStateManager.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            gameStateManager.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            gameStateManager.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}
