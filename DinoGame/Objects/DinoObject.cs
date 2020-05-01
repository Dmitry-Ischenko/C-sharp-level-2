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
        //1. Point(40,0) Size(52,35)
        //2. Point(0,29) Size(69,32)
        //3. Point(20,66) Size(40,28)
        private new Rectangle[] Rect = new Rectangle[3];
        public DinoObject(Point position, Point pointMoving, Size size) : base(position, pointMoving, size)
        {
            Position = new Point(Position.X + Size.Width, Position.Y);
            _indexImage = 1;
            SpeedAnimation = 10;
            _jumpHeight = 0;
            _jump = 0;
            Rect[0] = new Rectangle(new Point(Position.X+40,Position.Y),new Size(44,32));
            Rect[1] = new Rectangle(new Point(Position.X,Position.Y+29),new Size(69,32));
            Rect[2] = new Rectangle(new Point(Position.X+20,Position.Y+62),new Size(38,28));
        }
        public override void AllDateUpdate(Point position, Point pointMoving, Size size)
        {
            base.AllDateUpdate(position, pointMoving, size);
            Position = new Point(Position.X + Size.Width, Position.Y);
            _indexImage = 1;
            SpeedAnimation = 10;
            _jumpHeight = 0;
            _jump = 0;
            Rect[0] = new Rectangle(new Point(Position.X + 40, Position.Y), new Size(44, 32));
            Rect[1] = new Rectangle(new Point(Position.X, Position.Y + 29), new Size(69, 32));
            Rect[2] = new Rectangle(new Point(Position.X + 20, Position.Y + 62), new Size(38, 28));
        }
        public override void UpdatePosition()
        {
            if (_jump == 1)
            {
                if (_jumpHeight < 25)
                {
                    //Position.Y -= 8;
                    Position = new Point(Position.X, Position.Y - 7);
                    _jumpHeight++;
                }
                else
                {
                    Position = new Point(Position.X, Position.Y + 7);
                    _jumpHeight++;
                    if (_jumpHeight == 50)
                    {
                        _jumpHeight = 0;
                        _jump = 0;
                    }
                }
            }
            Rect[0] = new Rectangle(new Point(Position.X + 40, Position.Y), new Size(44, 32));
            Rect[1] = new Rectangle(new Point(Position.X, Position.Y + 29), new Size(69, 32));
            Rect[2] = new Rectangle(new Point(Position.X + 20, Position.Y + 62), new Size(38, 28));
        }
        public void Jump()
        {
            if (_jump == 0) _jump = 1;
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(_imageObj[_indexImage], new Rectangle(Position, Size));
            for (int i = 0; i < Rect.Length; i++)
            {
                _buffer.Graphics.DrawRectangle(new Pen(Color.Red, 2), Rect[i]);
            }
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


        //public Rectangle Rect
        //{
        //    get
        //    {
        //        return new Rectangle(Position, Size);
        //    }
        //}
    public new bool Collision(ICollision other)
        {
            //if (other.Rect.IntersectsWith(this.Rect)) return true; else return false;
            for (int i = 0; i < Rect.Length; i++)
            {
                if (other.Rect.IntersectsWith(this.Rect[i])) return true;
            }
            return false;
        }
    }
}
