using System;
using System.Drawing;

namespace DinoGame.Objects
{
    abstract class GameObject
    {
        protected Point Position { get; set; }
        public Point PointMoving { get; set; }
        public Size Size { get; protected set; }
        public GameObject ()
        {
            Position = new Point(0, 0);
            PointMoving = new Point(0, 0);
            Size = new Size(0, 0);
        }
        public GameObject (Point position,Point pointMoving,Size size)
        {
            this.Position = position;
            this.PointMoving = pointMoving;
            this.Size = size;
        }
        public abstract void Draw(BufferedGraphics _buffer);
        public abstract void UpdatePosition();
    }
}
