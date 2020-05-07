using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoGame.Objects
{
    class PterodactylObject : GameObject
    {
        private static Image[] _imageObj = 
        {
            Image.FromFile("Resources\\Pterodacty1.png"),
            Image.FromFile("Resources\\Pterodacty2.png")
        };
        private int _index;
        public int SpeedAnimation { get; set; }
        public static int Count { get; set; }
        public PterodactylObject(Point position, Point pointMoving, Size size) :base(position, pointMoving, size)
        {
            Count++;
            _index = 0;
            SpeedAnimation = 20;
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(_imageObj[_index], new Rectangle(Position, Size));
            //_buffer.Graphics.DrawRectangle(new Pen(Color.Red, 2), this.Rect);
            SpeedAnimation--;
            if (SpeedAnimation <=0)
            {
                if (_index == 0) _index = 1;
                else _index = 0;
                SpeedAnimation = 20;
            }
        }

        public override void UpdatePosition()
        {
            Position = new Point(Position.X - PointMoving.X, Position.Y);
        }
    }
}
