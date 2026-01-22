using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame

{
    public class Player
    {
        private int Speed { get; set; } = 200;
        private int Gravity { get; set; } = 1100;
        private int Jump_strength { get; set; } = 340;
        private Vector2 Velocity { get; set; }
        private int JumpCount { get; set; }
        private Texture2D Texture { get; set; }
        private bool OnGround { get; set; }
        public Vector2 Position { get; private set; }

        public Player(Vector2 position, Texture2D texture)
        {
            Position = position;
            Texture = texture;
        }

        public void UpdateVelocity(float deltaTime)
        {
            Vector2 v = Velocity;
            if (InputController.KeyDown(Keys.A))
                v.X = -Speed;
            else if (InputController.KeyDown(Keys.D))
                v.X = Speed;
            else
                v.X = 0;

            v.Y += Gravity * deltaTime;

            if (InputController.KeyPressed(Keys.W) || InputController.KeyPressed(Keys.Space))
            {
                if (OnGround)
                {
                    v.Y = -Jump_strength;
                    JumpCount = 1;
                }
                else if (JumpCount == 1)
                {
                    v.Y = -Jump_strength;
                    JumpCount = 2;
                }
            }
            Velocity = v;
        }

        public void UpdatePosition(float deltaTime, Level level)
        {
            Vector2 v = Velocity;
            OnGround = false;
            var newPos = Position + Velocity * deltaTime;
            Rectangle newBounds = CalculateBounds(newPos);
            foreach (var collider in level.GetTilesColliders(newBounds))
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

        public Rectangle CalculateBounds(Vector2 newPos)
        {
            return new Rectangle((int)newPos.X, (int)newPos.Y, Texture.Width, Texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height
            );
        }
    }
}
