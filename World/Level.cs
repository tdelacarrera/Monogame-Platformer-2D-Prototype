using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame.World
{
    public class Level : ICollisionProvider
    {
        private RenderTarget2D renderTarget;
        private Texture2D textureAtlas;
        private List<Texture2D> textureRegions;
        private static Dictionary<Vector2, int> foreground;
        private static Dictionary<Vector2, int> midground;
        private string root;
        private string textureAtlasPath;
        public Vector2 Dimensions;
        public static readonly int TILE_SIZE = 16;

        public Level(string root, string textureAtlasPath)
        {
            this.root = root;
            this.textureAtlasPath = textureAtlasPath;
        }
        public void LoadContent(ContentManager content)
        {
            foreground = LoadLevel(root + "foreground.csv");
            midground = LoadLevel(root + "midground.csv");
            Dimensions = GetMapSize(foreground);
            textureAtlas = content.Load<Texture2D>(textureAtlasPath);
            textureRegions = LoadTileTextures(textureAtlas);
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
                    Rectangle dest = new Rectangle((int)tile.Key.X * TILE_SIZE, (int)tile.Key.Y * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    Texture2D tileTexture = textureRegions[tile.Value];
                    spriteBatch.Draw(tileTexture, dest, Color.White);
                }
            }
            foreach (var tile in foreground)
            {
                if (tile.Value != -1)
                {
                    Rectangle dest = new Rectangle((int)tile.Key.X * TILE_SIZE, (int)tile.Key.Y * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    Texture2D tileTexture = textureRegions[tile.Value];
                    spriteBatch.Draw(tileTexture, dest, Color.White);
                }
            }
            spriteBatch.End();
            Globals.GraphicsDevice.SetRenderTarget(null);
        }



        private List<Texture2D> LoadTileTextures(Texture2D textureAtlas)
        {
            List<Texture2D> tileTextures = new List<Texture2D>();

            for (int y = 0; y < textureAtlas.Height / TILE_SIZE; y++)
            {
                for (int x = 0; x < textureAtlas.Width / TILE_SIZE; x++)
                {
                    Rectangle tileRegion = new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE);
                    Texture2D tileTexture = new Texture2D(Globals.GraphicsDevice, TILE_SIZE, TILE_SIZE);
                    Color[] tileData = new Color[TILE_SIZE * TILE_SIZE];

                    textureAtlas.GetData(0, tileRegion, tileData, 0, tileData.Length);
                    tileTexture.SetData(tileData);
                    tileTextures.Add(tileTexture);
                }
            }

            return tileTextures;
        }

        private Dictionary<Vector2, int> LoadLevel(string filepath)
        {

            StreamReader streamReader = new(filepath);
            Dictionary<Vector2, int> result = new();

            int y = 0;
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] tilemap = line.Split(',');

                for (int x = 0; x < tilemap.Length; x++)
                {
                    if (int.TryParse(tilemap[x], out int value))
                    {
                        if (value > -1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
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
            return new Vector2(maxX * TILE_SIZE, maxY * TILE_SIZE);
        }


        public List<Rectangle> GetNearbyTilesColliders(Rectangle bounds)
        {
            int leftTile = (int)Math.Floor((float)bounds.Left / TILE_SIZE);
            int rightTile = (int)Math.Ceiling((float)bounds.Right / TILE_SIZE) - 1;
            int topTile = (int)Math.Floor((float)bounds.Top / TILE_SIZE);
            int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / TILE_SIZE) - 1;

            leftTile = MathHelper.Clamp(leftTile, 0, foreground.Count - 1);
            rightTile = MathHelper.Clamp(rightTile, 0, foreground.Count - 1);
            topTile = MathHelper.Clamp(topTile, 0, foreground.Count - 1);
            bottomTile = MathHelper.Clamp(bottomTile, 0, foreground.Count - 1);

            List<Rectangle> result = new();

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    Vector2 position = new Vector2(x, y);
                    if (foreground.ContainsKey(position) && foreground[position] > -1)
                    {

                        result.Add(new Rectangle(x * TILE_SIZE, y * TILE_SIZE, TILE_SIZE, TILE_SIZE));
                    }
                }
            }
            return result;
        }


    }
}
