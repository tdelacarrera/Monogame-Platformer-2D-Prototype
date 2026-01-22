using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame

{
    public class Game1 : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Camera Camera;
        private Player player;
        private List<Coin> coins;
        private Level level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = Globals.ScreenW;
            graphics.PreferredBackBufferHeight = Globals.ScreenH;
            graphics.ApplyChanges();

            level = new Level("Data/Level1/", "textureAtlas", GraphicsDevice);
            Dictionary<Vector2, int> loadCoins = CoinLoader.LoadEntities("Data/Level1/entities.csv", 108);
            coins = new List<Coin>();

            foreach (var coinsToLoad in loadCoins)
            {
                coins.Add(new Coin(coinsToLoad.Key * Globals.TileSize, Content.Load<Texture2D>("coin")));
            }

            level.LoadContent(Content);
            Camera = new Camera(level.Dimensions, GraphicsDevice.Viewport);
            player = new Player(Vector2.Zero, Content.Load<Texture2D>("player"));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.UpdateVelocity(deltaTime);
            player.UpdatePosition(deltaTime, level);

            //pickup coin
            foreach (Coin coin in coins)
            {
                if (player.GetBounds().Intersects(coin.GetBounds()))
                {
                    coin.IsPicked = true;
                }
            }

            //remove coin
            for (int i = coins.Count - 1; i >= 0; i--)
            {
                if (coins[i].IsPicked)
                {
                    coins.RemoveAt(i);
                }
            }

            Camera.Update(player.Position);
            Camera.UpdateViewMatrix();
            
            InputController.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: Camera.TransformMatrix, samplerState:  SamplerState.PointClamp);

            level.Draw(_spriteBatch);

            foreach (var coin in coins)
            {
                coin.Draw(_spriteBatch);
            }

            player.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
