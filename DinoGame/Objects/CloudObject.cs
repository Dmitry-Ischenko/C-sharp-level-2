using System;
using System.Drawing;

namespace DinoGame.Objects
{
    class CloudObject : GameObject
    {
        static Image ImageObj { get; } = Image.FromFile("Resources\\cloud.png");
        //public int Speed { get; set; }
        public CloudObject(Point position, Point pointMoving, Size size) : base(position, pointMoving, size)
        {

        }
        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(ImageObj, new Rectangle(Position, Size));
        }
        public override void UpdatePosition()
        {

            Position = new Point(Position.X - PointMoving.X, Position.Y);
            if (Position.X + Size.Width < 0) Position = new Point(Game.Width + 50, Position.Y);
        }
    }
}
