using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame

{
    public class Level
    {
        private RenderTarget2D RenderTarget;
        private Texture2D TextureAtlas;
        private List<Texture2D> textureRegions;
        private Dictionary<Vector2, int> Foreground;
        private Dictionary<Vector2, int> Midground;
        private string Root;
        private string TextureAtlasPath;
        public Vector2 Dimensions;
        public readonly int TileSize = Globals.TileSize;
        public GraphicsDevice GraphicsDevice;

        public Level(string root, string textureAtlasPath, GraphicsDevice graphicsDevice)
        {
            Root = root;
            TextureAtlasPath = textureAtlasPath;
            GraphicsDevice = graphicsDevice;
        }
        public void LoadContent(ContentManager content)
        {
            Foreground = LevelLoader.LoadLevel(Root + "foreground.csv");
            Midground = LevelLoader.LoadLevel(Root + "midground.csv");
            Dimensions = GetMapSize(Foreground);
            TextureAtlas = content.Load<Texture2D>(TextureAtlasPath);
            textureRegions = TilemapTextureLoader.LoadTileTextures(TextureAtlas, TileSize, GraphicsDevice);
            RenderTarget = new RenderTarget2D(GraphicsDevice, (int)Dimensions.X, (int)Dimensions.Y);
            DrawOnRenderTarget();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(RenderTarget, Vector2.Zero, Color.White);
        }

        public void DrawOnRenderTarget()
        {
            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            GraphicsDevice.SetRenderTarget(RenderTarget);
            GraphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            foreach (var tile in Midground)
            {
                if (tile.Value != -1)
                {
                    Rectangle dest = new Rectangle((int)tile.Key.X * TileSize, (int)tile.Key.Y * TileSize, TileSize, TileSize);
                    Texture2D tileTexture = textureRegions[tile.Value];
                    spriteBatch.Draw(tileTexture, dest, Color.White);
                }
            }
            foreach (var tile in Foreground)
            {
                if (tile.Value != -1)
                {
                    Rectangle dest = new Rectangle((int)tile.Key.X * TileSize, (int)tile.Key.Y * TileSize, TileSize, TileSize);
                    Texture2D tileTexture = textureRegions[tile.Value];
                    spriteBatch.Draw(tileTexture, dest, Color.White);
                }
            }
            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);
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

        public List<Rectangle> GetTilesColliders(Rectangle bounds)
        {
            return CalculateTilesColliders(bounds, Foreground, TileSize);
        }
        
        public List<Rectangle>CalculateTilesColliders(Rectangle bounds, Dictionary<Vector2, int> layer, int tileSize)
         {
            int leftTile = (int)Math.Floor((float)bounds.Left / tileSize);
            int rightTile = (int)Math.Ceiling((float)bounds.Right / tileSize) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / tileSize);
            int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / tileSize) - 1;

            List<Rectangle> result = new();

            for (int y = topTile; y <= bottomTile; y++)
                {
                 for (int x = leftTile; x <= rightTile; x++)
                    {
                        Vector2 position = new Vector2(x, y);
                        if (layer.ContainsKey(position) && layer[position] > -1)
                        {
                            result.Add(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));
                        }
                 }
             }
             return result;
        }
        
    }
}
