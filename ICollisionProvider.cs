using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Monogame
{
    public interface ICollisionProvider
    {
        List<Rectangle> GetNearbyTilesColliders(Rectangle bounds);

    }
}
