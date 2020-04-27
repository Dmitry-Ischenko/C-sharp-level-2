using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoGame.Objects
{
    class CactusObject : GameObject
    {
        public CactusObject(Point position, Point pointMoving, Size size) : base(position, pointMoving, size)
        {
        }

        static Image ImageObj { get; } = Image.FromFile("Resources\\catus1.png");
        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(ImageObj, new Rectangle(Position, Size));
        }

        public override void UpdatePosition()
        {
            Position = new Point(Position.X-PointMoving.X,Position.Y);
        }
    }
}
