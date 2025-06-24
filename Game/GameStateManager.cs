using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Engine.Rendering;
using Engine;


namespace Monogame
{
    public partial class GameStateManager : IRenderable, IUpdatable
    {
        private MenuScene menuScene = new MenuScene();
        private GameScene gameScene = new GameScene();
        public void LoadContent(ContentManager Content)
        {
            menuScene.LoadContent(Content);
            gameScene.LoadContent(Content);
        }

        public void Update(GameTime gameTime)
        {
            switch (Globals.CurrentState)
            {
                case Globals.Scenes.Menu:
                    menuScene.Update(gameTime);
                    break;
                case Globals.Scenes.Game:
                    gameScene.Update(gameTime);
                    break;
                case Globals.Scenes.Settings:
                    break;
            }
        }
        public void Draw(SpriteBatch spritebatch)
        {
            switch (Globals.CurrentState)
            {
                case Globals.Scenes.Menu:
                    menuScene.Draw(spritebatch);
                    break;
                case Globals.Scenes.Game:
                    gameScene.Draw(spritebatch);
                    break;
                case Globals.Scenes.Settings:
                    break;
            }
        }
    }
}
