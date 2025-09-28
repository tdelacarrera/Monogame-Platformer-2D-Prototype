using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Monogame;

public class CollisionDetection()
{
    public static List<Rectangle> GetNearbyTilesColliders(Rectangle bounds, Dictionary<Vector2, int> layer, int tileSize)
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