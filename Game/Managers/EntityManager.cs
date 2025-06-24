using System;
using System.Collections.Generic;
using System.IO;
using Engine.Entity;
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

            entities = EntityLoader.LoadEntities(root + "entities.csv");
            instancedEntities = EntitySpawner.SpawnEntities(entities);

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
    }
}
