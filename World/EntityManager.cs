using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Monogame.Entities;

namespace Monogame.World
{
    public class EntityManager
    {
        private static Dictionary<Vector2, int> entities;
        private List<Entity> instancedEntities;
        private List<Entity> entitiesToRemove;
        private string root;

        public EntityManager(String root) {
            this.root = root;
            entitiesToRemove = new List<Entity>();
        }

        public void LoadContent(ContentManager content){

            entities = LoadLevel(root + "entities.csv");
            SpawnEntities(entities);

            foreach (var entity in instancedEntities) {
                entity.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime, Player player) {
            foreach (var entity in instancedEntities)
            {
                entity.Update(gameTime);
                if (player.Bounds.Intersects(entity.Bounds) && entity.Group == "coin") {
                    entitiesToRemove.Add(entity);
                }
            }
            foreach (var entity in entitiesToRemove)
            {
                instancedEntities.Remove(entity);
            }
            entitiesToRemove.Clear();
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (var entity in instancedEntities)
            {
                entity.Draw(spriteBatch);
            }
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

        private void SpawnEntities(Dictionary<Vector2, int> entities)
        {
            instancedEntities = new List<Entity>();
            foreach (var entity in entities)
            {
                if (entity.Value != -1)
                {
                    if (entity.Value == 108)
                    {
                        Coin coin = new Coin(entity.Key * Level.TILE_SIZE);
                        instancedEntities.Add(coin);
                    }
                }
            }
        }
    }
}
