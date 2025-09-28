using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame

{
    public class Player : IPlayer
    {
        public int SPEED { get; set; } = 200;
        public int GRAVITY { get; set; } = 1100;
        public int JUMP_STRENGTH { get; set; } = 340;
        public ICollisionDetection CollisionDetection { get; set; }
        public Vector2 Velocity { get; set; }
        public int JumpCount { get; set; }
        public Texture2D Texture { get; set; }
        public bool OnGround { get; set; }
        public string Group { get; set; } = "empty";
        public Vector2 Position { get; set; }

        public Player(Vector2 position, ICollisionDetection collisionDetection)
        {
            Group = "player";
            Position = position;
            CollisionDetection = collisionDetection;
        }

        public void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("player");
        }

        private void UpdateVelocity()
        {
            Vector2 v = Velocity;
            if (InputController.KeyDown(Keys.A))
                v.X = -SPEED;
            else if (InputController.KeyDown(Keys.D))
                v.X = SPEED;
            else
                v.X = 0;

            v.Y += GRAVITY * Globals.DeltaTime;

            if (InputController.KeyPressed(Keys.W) || InputController.KeyPressed(Keys.Space))
            {
                if (OnGround)
                {
                    v.Y = -JUMP_STRENGTH;
                    JumpCount = 1;
                }
                else if (JumpCount == 1)
                {
                    v.Y = -JUMP_STRENGTH;
                    JumpCount = 2;
                }
            }
            Velocity = v;
        }

        public void UpdatePosition()
        {
            Vector2 v = Velocity;
            OnGround = false;
            var newPos = Position + Velocity * Globals.DeltaTime;
            Rectangle newBounds = CalculateBounds(newPos);
            List<Rectangle> nearbyColliders = CollisionDetection.GetNearbyTilesColliders(newBounds);
            foreach (var collider in nearbyColliders)
            {
                if (newPos.X != Position.X)
                {
                    newBounds = CalculateBounds(new(newPos.X, Position.Y));
                    if (newBounds.Intersects(collider))
                    {
                        if (newPos.X > Position.X)
                            newPos.X = collider.Left - Texture.Width;
                        else
                            newPos.X = collider.Right;
                    }
                }
                newBounds = CalculateBounds(new(Position.X, newPos.Y));
                if (newBounds.Intersects(collider))
                {
                    if (Velocity.Y > 0)
                    {
                        newPos.Y = Position.Y;
                        OnGround = true;
                        v.Y = 0;
                        JumpCount = 0;
                    }
                    else
                    {
                        newPos.Y = collider.Bottom;
                        v.Y = 0;

                    }
                }
            }
            Position = newPos;
            Velocity = v;
        }

        private Rectangle CalculateBounds(Vector2 newPos)
        {
            return new Rectangle((int)newPos.X, (int)newPos.Y, Texture.Width, Texture.Height);
        }

        public void Update(GameTime gameTime)
        {
            UpdateVelocity();
            UpdatePosition();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public void DetectPickables(List<IPickable> pickables)
        {
            foreach (IPickable pickable in pickables)
            {
                if (GetBounds().Intersects(pickable.GetBounds()) && pickable is Coin)
                {
                    pickable.Pickup();
                }
            }
        }
        public Rectangle GetBounds()
        {
            if (Texture == null)
                return Rectangle.Empty;

            return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height
            );
        }

        public void DetectPickables()
        {
            throw new System.NotImplementedException();
        }
    }
}
