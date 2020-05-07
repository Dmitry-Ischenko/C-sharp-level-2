using System.Drawing;


namespace DinoGame.Objects
{
    interface ICollision
    {
        bool Collision(ICollision obj);

        Rectangle Rect { get; }
    }
}
