using System;
using System.Drawing;
namespace Asteroids.Objects
{
    class BaseObject
    {
        protected Point Pos;//{ get; set; }
        protected Point Dir;// { get; set; }
        protected Size Size;// { get; set; }

        public BaseObject()
        {
            Pos = new Point(0, 0);
            Dir = new Point(0, 0);
            Size = new Size(0, 0);
        }
        public BaseObject(Point pos, Point dir, Size size)
        {
            this.Pos = pos;
            this.Dir = dir;
            this.Size = size;
        }

        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse(Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height);
        }


        public virtual void Update()
        {
            Pos.X += Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }

    }

    class Star: BaseObject
    {
        static Image Image { get; } = Image.FromFile("Images\\star.png");


        public Star(Point pos, Point dir, Size size):base(pos,dir,size)
        {

        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Image, Pos);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
            //Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + 50;
        }


    }
}
