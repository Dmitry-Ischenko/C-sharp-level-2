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
        static Image Image { get; } = Image.FromFile("Resources\\cloud.png");
        public CloudObject(Point position, Point pointMoving, Size size):base(position,pointMoving,size)
        {
            _position.X += _size.Width;
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(Image, _position);
        }
        public override void UpdatePosition(int speed)
        {

            _position.X -= speed;
            //_position.X -= _pointMoving.X;
            //Pos.X -= Dir.X;
            ////Pos.Y = Pos.Y + Dir.Y;
            if (_position.X+_size.Width < 0) _position.X = Game.Width + 50;
        }
    }
}
