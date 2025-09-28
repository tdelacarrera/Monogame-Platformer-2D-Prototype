using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Monogame
{
    public interface ICollisionDetection
    {
        List<Rectangle> GetNearbyTilesColliders(Rectangle bounds);

    }
}
