using System.Drawing;

namespace Asteroids.Objects
{
    interface ICollision
    {
        bool Collision(ICollision obj);

        Rectangle Rect { get; }
    }
}
