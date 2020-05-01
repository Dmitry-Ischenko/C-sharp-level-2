using System;
using System.Drawing;

namespace DinoGame.Objects
{
    class GroundObject:GameObject
    {
        private static Image _imageObj = Image.FromFile("Resources\\ground.png");

        public GroundObject(Point position, Point pointMoving, Size size) :base(position, pointMoving, size)
        {
            Size = new Size(_imageObj.Width, _imageObj.Height);
        }
        public override void AllDateUpdate(Point position, Point pointMoving, Size size)
        {
            base.AllDateUpdate(position, pointMoving, size);
            Size = new Size(_imageObj.Width, _imageObj.Height);
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            if (Position.X*-1>=Size.Width)
            {
                Position = new Point(0, Position.Y);
            }
            _buffer.Graphics.DrawImage(_imageObj, new Rectangle(Position, Size));
            _buffer.Graphics.DrawImage(_imageObj, new Rectangle(new Point(Position.X+Size.Width,Position.Y), Size));
        }

        public override void UpdatePosition()
        {
            Position = new Point(Position.X - PointMoving.X, Position.Y);
        }
    }
}
