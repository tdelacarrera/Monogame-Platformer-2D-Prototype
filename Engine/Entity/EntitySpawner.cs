using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Monogame.Entities;
using Engine.Level;

namespace Engine.Entity
{
    public class EntitySpawner()
    {
        public static List<Entity> SpawnEntities(Dictionary<Vector2, int> entities)
        {
            List<Entity> instancedEntities = new List<Entity>();
            foreach (var entity in entities)
            {
                if (entity.Value != -1)
                {
                    if (entity.Value == 108)
                    {
                        //TODO: FIX SIZE
                        Coin coin = new Coin(entity.Key * 16);
                        instancedEntities.Add(coin);
                    }
                }
            }
            return instancedEntities;
        }

    }           


}
