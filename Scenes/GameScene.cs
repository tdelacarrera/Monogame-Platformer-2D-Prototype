using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Monogame.Entities;
using Monogame.Managers;
using Monogame.World;


namespace Monogame
{
    public class GameScene : Scene
    {
        private ViewportManager viewportManager;
        private Level level;
        private EntityManager entityManager;
        private Player player;
        private Song song;

        public override void LoadContent(ContentManager content)
        {
            level = new Level("../../../Data/Level1/", "textureAtlas");
            entityManager = new EntityManager("../../../Data/Level1/");
            entityManager.LoadContent(content);
            level.LoadContent(content);
            //song = content.Load<Song>("Sounds/CaveLoop");
            //MediaPlayer.Play(song);
            //MediaPlayer.IsRepeating = true;
            viewportManager = new ViewportManager(level.Dimensions);
            player = new Player(Vector2.Zero,level);
            player.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            Globals.Update(gameTime);
            InputManager.Update();
            viewportManager.Update(player.Position);
            player.Update(gameTime);
            entityManager.Update(gameTime, player);
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.GraphicsDevice.Clear(Color.CornflowerBlue);
            spritebatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: viewportManager.TransformMatrix);
            level.Draw(spritebatch);
            player.Draw(spritebatch);
            entityManager.Draw(spritebatch);
            spritebatch.End();

        }
    }
}
