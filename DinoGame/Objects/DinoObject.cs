using System;
using System.Drawing;

namespace DinoGame.Objects
{
    class DinoObject : GameObject
    {
        static Image[] _imageObj = {
            Image.FromFile("Resources\\Dino1.png"),
            Image.FromFile("Resources\\Dino2.png"),
            Image.FromFile("Resources\\Dino3.png"),
            Image.FromFile("Resources\\Dino4.png"),
            Image.FromFile("Resources\\Dino5.png"),
            Image.FromFile("Resources\\Dino6.png")
        };
        private int _indexImage;
        public int SpeedAnimation { get; set; }
        private int _jump;
        private int _bendDown;
        private Point groundPosition;
        private Point maxHeightJump;
        private new Rectangle[] Rect = new Rectangle[3];
        public DinoObject(Point position, Point pointMoving, Size size) : base(position, pointMoving, size)
        {
            DinoInit(position, pointMoving, size);
        }
        private void UpdateRect()
        {
            if (_bendDown == 0 || !groundPosition.Equals(Position))
            {
                Rect[0] = new Rectangle(new Point(Position.X + 40, Position.Y), new Size(40, 28));
                Rect[1] = new Rectangle(new Point(Position.X + 6, Position.Y + 29), new Size(54, 32));
                Rect[2] = new Rectangle(new Point(Position.X + 24, Position.Y + 62), new Size(26, 24));
            } else if (_bendDown == 1 && groundPosition.Equals(Position))
            {
                Rect[0] = new Rectangle(new Point(Position.X, Position.Y+ 42), new Size(109, 28));
                Rect[1] = new Rectangle(new Point(Position.X, Position.Y + 42), new Size(54, 32));
                Rect[2] = new Rectangle(new Point(Position.X + 24, Position.Y + 62), new Size(26, 24));
            }
        }
        private void DinoInit(Point position, Point pointMoving, Size size)
        {
            Position = new Point(Position.X + Size.Width, Position.Y);
            groundPosition = new Point(Position.X,Position.Y);
            maxHeightJump = new Point(Position.X,Position.Y-(Size.Height*2));
            _indexImage = 1;
            SpeedAnimation = 10;
            _jump = 0;
            _bendDown = 0;
            UpdateRect();
        }
        public override void AllDataUpdate(Point position, Point pointMoving, Size size)
        {
            base.AllDataUpdate(position, pointMoving, size);
            DinoInit(position, pointMoving, size);
        }
        public override void UpdatePosition()
        {
            if (_jump == 1 || !groundPosition.Equals(Position))
            {
                if (_jump == 1 && Position.Y > maxHeightJump.Y)
                {
                    Position = new Point(Position.X, Position.Y - 7);
                }else
                {
                    Position = new Point(Position.X, Position.Y + 7);
                    if (_jump == 1) _jump = 0;
                }
                if (Position.Y > groundPosition.Y)
                {
                    Position = new Point(groundPosition.X, groundPosition.Y);
                }
            }
            UpdateRect();
        }
        public void Jump()
        {
            if (_jump == 0 && groundPosition.Equals(Position) && _bendDown == 0) _jump = 1;
        }
        public int BendDown
        {
            get
            {
                return _bendDown;
            }
            set
            {
                //Console.Write("   BendDown method Run: \n");
                if (_jump == 0)
                {
                    if (value == 1 || value == 0)
                    {
                        _bendDown = value;
                        //Console.WriteLine($"      BendDown is {_bendDown}");
                    }
                } else
                {
                    //Console.WriteLine($"      JUMP! is {_jump}");
                }
            }
        }
        public void DinoDie()
        {
            _indexImage = 3;
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            Point _position;
            if (_indexImage == 4 || _indexImage == 5)
            {
                _position = new Point(Position.X, Position.Y + 34);
            } else
            {
                _position = Position;
            }
            Size _size = new Size(_imageObj[_indexImage].Size.Width, _imageObj[_indexImage].Size.Height);
            _buffer.Graphics.DrawImage(_imageObj[_indexImage], new Rectangle(_position, _size));

            //for (int i = 0; i < Rect.Length; i++)
            //{
            //    _buffer.Graphics.DrawRectangle(new Pen(Color.Red, 2), Rect[i]);
            //}

            if (_jump == 0 && _bendDown == 0 && groundPosition.Equals(Position))
            {
                SpeedAnimation--;
                if (SpeedAnimation <= 0)
                {
                    if (_indexImage == 1) _indexImage = 2;
                    else _indexImage = 1;
                    SpeedAnimation = 10;
                }
            }
            if (_jump == 0 && _bendDown == 1 && groundPosition.Equals(Position))
            {
                SpeedAnimation--;
                if (SpeedAnimation <= 0)
                {
                    if (_indexImage == 4) _indexImage = 5;
                    else _indexImage = 4;
                    SpeedAnimation = 10;
                }
            }

            if (_jump == 1 && _bendDown == 0) { _indexImage = 0; }
        }
    public new bool Collision(ICollision other)
        {
            for (int i = 0; i < Rect.Length; i++)
            {
                if (other.Rect.IntersectsWith(this.Rect[i])) return true;
            }
            return false;
        }
    }
}
