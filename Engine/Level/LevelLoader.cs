using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Level;
public class LevelLoader()
{
    public static Dictionary<Vector2, int> LoadLevel(string filepath)
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
}


