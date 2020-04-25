using System;
using System.Drawing;
namespace Asteroids.Objects
{
    abstract class BaseObject : ICollision
    {
        protected Point Pos;// { get; set; }
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

        abstract public void Draw();


        public abstract void Update();

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(Pos, Size);
            }
        }

        public bool Collision(ICollision other)
        {
            if (other.Rect.IntersectsWith(this.Rect)) return true; else return false;
        }

    }

    class Star : BaseObject
    {
        static Image Image { get; } = Image.FromFile("Images\\star.png");


        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
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

    class Asteroid : BaseObject
    {
        //static Image Image { get; } = Image.FromFile("Images\\star.png");


        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        public override void Draw()
        {
            //Game.Buffer.Graphics.DrawImage(Image, Pos);
            Game.Buffer.Graphics.DrawEllipse(Pens.Azure, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            Pos.X -= Dir.X;
            //Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Pos.X = Game.Width + 50;
        }

    }


    class Ship : BaseObject
    {
        int protect;

        public int Protect
        {
            get
            {
                return protect;
            }
        }
        static Image Image { get; } = Image.FromFile("Images\\ship.png");


        public Ship(Point pos) : base(pos, new Point(0,0), Image.Size)
        {
            protect = 100;
        }

        public void Low(int value)
        {
            protect -= value;
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawString(protect.ToString(),SystemFonts.CaptionFont, Brushes.White, Pos.X, Pos.Y - 20);
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
