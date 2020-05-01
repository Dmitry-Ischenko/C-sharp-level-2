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
        static Image[] ImageObj { get; } = {
            Image.FromFile("Resources\\catus1.png"),
            Image.FromFile("Resources\\catus2.png"),
            Image.FromFile("Resources\\catus3.png")
        };
        private int index;
        public CactusObject(Point position, Point pointMoving, Size size) : base(position, pointMoving, size)
        {
            UpdateSize();
        }
        public override void AllDateUpdate(Point position, Point pointMoving, Size size)
        {
            base.AllDateUpdate(position, pointMoving, size);
            UpdateSize();
        }
        public void UpdateIndex()
        {
            UpdateSize();
        }
        private void UpdateSize()
        {
            index = Game.Rand.Next(0, ImageObj.Length);
            Console.Write($"Size: {this.Size} || ImageSize: {ImageObj[index].Size}");
            int _size = (int)(ImageObj[index].Width * 0.7);
            this.Size = new Size(_size, Size.Height);
            Console.Write($" = > Size: {this.Size} || ImageSize: {ImageObj[index].Size}\n");
        } 
        public override void Draw(BufferedGraphics _buffer)
        {
            _buffer.Graphics.DrawImage(ImageObj[index], new Rectangle(Position, Size));
            _buffer.Graphics.DrawRectangle(new Pen(Color.Red, 2), this.Rect);
        }

        public override void UpdatePosition()
        {
            Position = new Point(Position.X-PointMoving.X,Position.Y);
        }
    }
}
