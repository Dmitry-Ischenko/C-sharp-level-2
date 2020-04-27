using System;
using System.Drawing;

namespace DinoGame.Objects
{
    class DinoObject : GameObject
    {
        static Image[] _imageObj = { Image.FromFile("Resources\\Dino1.png"),
            Image.FromFile("Resources\\Dino2.png"), Image.FromFile("Resources\\Dino3.png") };
        private int _indexImage;
        public int SpeedAnimation { get; set; }
        private int _jump;
        private int _jumpHeight;
        public DinoObject(Point position, Point pointMoving, Size size) : base(position, pointMoving, size)
        {
            Position = new Point(Position.X + Size.Width, Position.Y);
            _indexImage = 1;
            SpeedAnimation = 10;
            _jumpHeight = 0;
        }
        public override void UpdatePosition()
        {
            if (_jump == 1)
            {
                if (_jumpHeight < 25)
                {
                    //Position.Y -= 8;
                    Position = new Point(Position.X, Position.Y -7);
                    _jumpHeight++;
                }
                else
                {
                    Position = new Point(Position.X, Position.Y +7);
                    _jumpHeight++;
                    if (_jumpHeight == 50)
                    {
                        _jumpHeight = 0;
                        _jump = 0;
                    }
                }
            }
        }
        public void Jump()
        {
            if (_jump == 0) _jump = 1;
        }

        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(_imageObj[_indexImage], new Rectangle(Position, Size));
            if (_jump == 0)
            {
                SpeedAnimation--;
                if (SpeedAnimation <= 0)
                {
                    if (_indexImage == 1) _indexImage = 2;
                    else _indexImage = 1;
                    SpeedAnimation = 10;
                }
            }
            else _indexImage = 0;
        }
    }
}
