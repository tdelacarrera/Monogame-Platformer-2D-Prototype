using System.Collections.Generic;
using Engine;
using Engine.Entity;
using Engine.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Entities;
using Monogame.Managers;
using Monogame.World;

namespace Monogame
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private Camera Camera;
        private Player player;

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

            level = new Level("../../../Data/Level1/", "textureAtlas");
            Dictionary<Vector2, int> entities = EntityLoader.LoadEntities("../../../Data/Level1/entities.csv");
            foreach (var entity in entities)
            {
                if (entity.Value != -1)
                {
                    if (entity.Value == 108)
                    {
                        //TODO: FIX SIZE
                        Coin coin = new Coin(entity.Key * 16);
                        GameObjectManager.Add(coin);
                    }
                }
            }
            level.LoadContent(Content);
            Camera = new Camera(level.Dimensions);
            player = new Player(Vector2.Zero, level);
            GameObjectManager.Add(player);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (ILoadable loadable in GameObjectManager.Loadables)
            {
                loadable.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (IUpdatable updatable in GameObjectManager.Updatables)
            {
                updatable.Update(gameTime);
            }
            base.Update(gameTime);

            Globals.Update(gameTime);
            InputController.Update();
            player.DetectPickables();
            Camera.Update(player.Position);

        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(transformMatrix: Camera.TransformMatrix, samplerState:  SamplerState.PointClamp);
            foreach (IRenderable renderable in GameObjectManager.Renderables)
            {
                renderable.Draw(_spriteBatch);
            }
            level.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
