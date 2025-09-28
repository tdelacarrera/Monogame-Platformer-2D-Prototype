using Microsoft.Xna.Framework;

namespace Monogame
{
    public interface IPlayer : IGameEntity
    {
        ICollisionDetection CollisionDetection { get; set; }
        Vector2 Velocity { get; set; }
        bool OnGround { get; set; }
        int JumpCount { get; set; }
        void DetectPickables();
    }
}