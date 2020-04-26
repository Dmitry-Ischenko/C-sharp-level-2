using System;
using System.Drawing;

namespace DinoGame.Objects
{
    class DrawText : GameObject
    {
        //static Image ImageObj { get; } = Image.FromFile("Resources\\numbers.png");
        static Image[] sprites = new Image[12]; 
        public DrawText(Point position)     
        {
            Image ImageObj = Image.FromFile("Resources\\numbers.png");
            ImageObj.SelectActiveFrame;
            for (int i = 0; i < sprites.Length; i++)
            {
                //sprites[0] = Image.
            }
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            throw new NotImplementedException();
        }

        public override void UpdatePosition()
        {
            throw new NotImplementedException();
        }
    }
}
