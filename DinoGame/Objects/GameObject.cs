using System;
using System.Drawing;

namespace DinoGame.Objects
{
    class GameObject
    {
        protected Point _position;
        public Point _pointMoving;
        public Size _size;
        public GameObject ()
        {
            _position = new Point(0, 0);
            _pointMoving = new Point(0, 0);
            _size = new Size(0, 0);
        }
        public GameObject (Point position,Point pointMoving,Size size)
        {
            this._position = position;
            this._pointMoving = pointMoving;
            this._size = size;
        }
        public virtual void Draw(BufferedGraphics _buffer)
        {

        }
        public virtual void UpdatePosition(int speed)
        {

        }
    }
    class CloudObject:GameObject
    {
        static Image ImageObj { get; } = Image.FromFile("Resources\\cloud.png");
        public CloudObject(Point position, Point pointMoving, Size size):base(position,pointMoving,size)
        {
            _position.X += _size.Width;
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(ImageObj, _position);
        }
        public override void UpdatePosition(int speed)
        {

            _position.X -= speed;
            if (_position.X+_size.Width < 0) _position.X = Game.Width + 50;
        }
    }
    class DinoObject : GameObject
    {
        static Image[] _imageObj= { Image.FromFile("Resources\\Dino1.png"),
            Image.FromFile("Resources\\Dino2.png"), Image.FromFile("Resources\\Dino3.png") };
        private int _indexImage;
        private int _speed;
        private int _jump;
        private int _jumpHeight;
        public DinoObject(Point position, Point pointMoving, Size size) : base(position, pointMoving, size)
        {
            _position.X += _size.Width;
            _indexImage = 1;
            _speed = 10;
            _jumpHeight = 0;
        }
        public void Draw(BufferedGraphics _buffer,int speed)
        {
            _buffer.Graphics.DrawImage(_imageObj[_indexImage], new Rectangle(_position, _size));
            if (_jump == 0)
            {
                _speed -= speed;
                if (_speed <= 0)
                {
                    if (_indexImage == 1) _indexImage = 2;
                    else _indexImage = 1;
                    _speed = 10;
                }
            }
            else _indexImage = 0;
        }
        public void UpdatePosition()
        {
            if (_jump == 1)
            {
                if (_jumpHeight <30) { 
                    _position.Y -= 4;
                    _jumpHeight++;
                } else
                {
                    _position.Y += 4;
                    _jumpHeight++;
                    if (_jumpHeight == 60)
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

    }
}
