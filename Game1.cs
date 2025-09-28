using System.Collections.Generic;
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
        List<IPickable> pickables;
        private Level level;

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
            Globals.GraphicsDevice = graphics.GraphicsDevice;

            level = new Level("Data/Level1/", "textureAtlas");
            pickables = new List<IPickable>();
            Dictionary<Vector2, int> loadPickables = EntityLoader.LoadEntities("Data/Level1/entities.csv");
            foreach (var pickableToLoad in loadPickables)
            {
                if (pickableToLoad.Value != -1)
                {
                    if (pickableToLoad.Value == 108)
                    {
                        pickables.Add(new Coin(pickableToLoad.Key * 16));
                    }
                }
            }
            level.LoadContent(Content);
            Camera = new Camera(level.Dimensions);
            player = new Player(Vector2.Zero, level);
            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(Content);
            foreach (var entity in pickables)
            {
                entity.LoadContent(Content);
            }
    
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
            player.Update(gameTime);
            Globals.Update(gameTime);
            InputController.Update();
            player.DetectPickables(pickables);
            for (int i = pickables.Count - 1; i >= 0; i--)
            {
                pickables[i].Update(gameTime);
                if (pickables[i].IsPicked)
                {
                    pickables.RemoveAt(i);
                }
            }
            Camera.Update(player.Position);
            Camera.UpdateViewMatrix();

        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: Camera.TransformMatrix, samplerState:  SamplerState.PointClamp);
            level.Draw(_spriteBatch);
            foreach (var pickable in pickables)
            {
                pickable.Draw(_spriteBatch);
            }
            player.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
