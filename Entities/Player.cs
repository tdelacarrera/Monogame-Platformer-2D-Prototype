using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame.Entities
{
    public class Player : Entity
    {
        private ICollisionProvider collisionProvider;
        private const int SPEED = 200;
        private const int GRAVITY = 1100;
        private const int JUMP_STRENGHT = 340;
        private Vector2 velocity;
        private bool onGround;
        private int jumpCount = 0;
        public Player(Vector2 position, ICollisionProvider collisionProvider)
        {
            Group = "player";
            Position = position;
            this.collisionProvider = collisionProvider;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("player");
        }

        private void UpdateVelocity()
        {
            if (InputManager.KeyDown(Keys.A))
                velocity.X = -SPEED;
            else if (InputManager.KeyDown(Keys.D))
                velocity.X = SPEED;
            else
                velocity.X = 0;


            velocity.Y += GRAVITY * Globals.DeltaTime;


            if (InputManager.KeyPressed(Keys.W) || InputManager.KeyPressed(Keys.Space))
            {
                if (onGround)
                {
                    velocity.Y = -JUMP_STRENGHT;
                    jumpCount = 1;
                }
                else if (jumpCount == 1)
                {
                    velocity.Y = -JUMP_STRENGHT;
                    jumpCount = 2;
                }
            }
        }

        public void UpdatePosition()
        {
            onGround = false;
            var newPos = Position + velocity * Globals.DeltaTime;
            Rectangle newBounds = CalculateBounds(newPos);
            List<Rectangle> nearbyColliders = collisionProvider.GetNearbyTilesColliders(newBounds);
            foreach (var collider in nearbyColliders)
            {
                if (newPos.X != Position.X)
                {
                    newBounds = CalculateBounds(new(newPos.X, Position.Y));
                    if (newBounds.Intersects(collider))
                    {
                        if (newPos.X > Position.X)
                            newPos.X = collider.Left - texture.Width;
                        else
                            newPos.X = collider.Right;
                    }
                }
                newBounds = CalculateBounds(new(Position.X, newPos.Y));
                if (newBounds.Intersects(collider))
                {
                    if (velocity.Y > 0)
                    {
                        newPos.Y = Position.Y;
                        onGround = true;
                        velocity.Y = 0;
                        jumpCount = 0;
                    }
                    else
                    {
                        newPos.Y = collider.Bottom;
                        velocity.Y = 0;

                    }
                }
            }
            Position = newPos;

        }
        private Rectangle CalculateBounds(Vector2 newPos)
        {
            return new Rectangle((int)newPos.X, (int)newPos.Y, texture.Width, texture.Height);
        }

        public override void Update(GameTime gameTime)
        {
            UpdatePosition();
            UpdateVelocity();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
    }
}
