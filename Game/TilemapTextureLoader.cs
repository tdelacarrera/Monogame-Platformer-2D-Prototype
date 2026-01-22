using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogame;

public class TilemapTextureLoader()
{
    public static List<Texture2D> LoadTileTextures(Texture2D textureAtlas, int tileSize, GraphicsDevice graphicsDevice)
    {
        List<Texture2D> tileTextures = new List<Texture2D>();

        for (int y = 0; y < textureAtlas.Height / tileSize; y++)
        {
            for (int x = 0; x < textureAtlas.Width / tileSize; x++)
            {
                Rectangle tileRegion = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                Texture2D tileTexture = new Texture2D(graphicsDevice, tileSize, tileSize);
                Color[] tileData = new Color[tileSize * tileSize];

                textureAtlas.GetData(0, tileRegion, tileData, 0, tileData.Length);
                tileTexture.SetData(tileData);
                tileTextures.Add(tileTexture);
            }
        }
        
        return tileTextures;
    }

}




