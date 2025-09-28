using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame

{
    public class Level : ICollisionDetection
    {
        private RenderTarget2D renderTarget;
        private Texture2D textureAtlas;
        private List<Texture2D> textureRegions;
        private static Dictionary<Vector2, int> foreground;
        private static Dictionary<Vector2, int> midground;
        private string root;
        private string textureAtlasPath;
        public Vector2 Dimensions;
        public static readonly int TileSize = 16;

        public Level(string root, string textureAtlasPath)
        {
            this.root = root;
            this.textureAtlasPath = textureAtlasPath;
        }
        public void LoadContent(ContentManager content)
        {
            foreground = LevelLoader.LoadLevel(root + "foreground.csv");
            midground = LevelLoader.LoadLevel(root + "midground.csv");
            Dimensions = GetMapSize(foreground);
            textureAtlas = content.Load<Texture2D>(textureAtlasPath);
            textureRegions = TilemapTextureLoader.LoadTileTextures(textureAtlas, TileSize);
            renderTarget = new RenderTarget2D(Globals.GraphicsDevice, (int)Dimensions.X, (int)Dimensions.Y);
            DrawOnRenderTarget();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(renderTarget, Vector2.Zero, Color.White);
        }

        public void DrawOnRenderTarget()
        {
            SpriteBatch spriteBatch = new SpriteBatch(Globals.GraphicsDevice);
            Globals.GraphicsDevice.SetRenderTarget(renderTarget);
            Globals.GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            foreach (var tile in midground)
            {
                if (tile.Value != -1)
                {
                    Rectangle dest = new Rectangle((int)tile.Key.X * TileSize, (int)tile.Key.Y * TileSize, TileSize, TileSize);
                    Texture2D tileTexture = textureRegions[tile.Value];
                    spriteBatch.Draw(tileTexture, dest, Color.White);
                }
            }
            foreach (var tile in foreground)
            {
                if (tile.Value != -1)
                {
                    Rectangle dest = new Rectangle((int)tile.Key.X * TileSize, (int)tile.Key.Y * TileSize, TileSize, TileSize);
                    Texture2D tileTexture = textureRegions[tile.Value];
                    spriteBatch.Draw(tileTexture, dest, Color.White);
                }
            }
            spriteBatch.End();
            Globals.GraphicsDevice.SetRenderTarget(null);
        }



        private Vector2 GetMapSize(Dictionary<Vector2, int> tileMap)
        {
            int maxX = 0;
            int maxY = 0;

            foreach (var key in tileMap.Keys)
            {
                maxX = Math.Max(maxX, (int)key.X);
                maxY = Math.Max(maxY, (int)key.Y);
            }
            return new Vector2(maxX * TileSize, maxY * TileSize);
        }

        public List<Rectangle> GetNearbyTilesColliders(Rectangle bounds)
        {
            return CollisionDetection.GetNearbyTilesColliders(bounds, foreground, TileSize);
        }
    }
}
