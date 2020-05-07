using System.Drawing;
using System.Collections.Generic;
using System;

namespace DinoGame.Objects
{
    class DrawText : GameObject
    {
        static Image[] sprites = new Image[12];
        public string OutString { get; set; }
        private readonly Dictionary<char, int> index = new Dictionary<char, int> {
            {'0',0},
            {'1',1},
            {'2',2},
            {'3',3},
            {'4',4},
            {'5',5},
            {'6',6},
            {'7',7},
            {'8',8},
            {'9',9},
            {'H',10},
            {'I',11}
        };
        public DrawText(Point position, Point pointMoving, Size size):base(position, pointMoving, size)     
        {
            Image ImageObj = Image.FromFile("Resources\\numbers.png");
            Bitmap bmp = new Bitmap(ImageObj);
            Size imgSize = new Size(20, ImageObj.Height);
            Point imgPos;
            System.Drawing.Imaging.PixelFormat format = bmp.PixelFormat;
            for (int i = 0; i < sprites.Length; i++)
            {
                imgPos = new Point(i*20,0);
                if (imgSize.Width+imgPos.X > bmp.Width)
                {
                    imgSize = new Size(bmp.Width - imgPos.X, bmp.Height);
                }
                Rectangle cloneRect = new Rectangle(imgPos, imgSize);
                sprites[i] = bmp.Clone(cloneRect, format);
            }
            OutString = "";
        }
        public override void Draw(BufferedGraphics _buffer)
        {
            Point _position = Position;
            foreach (char simbol in OutString)
            {
                int value;
                if (simbol.Equals(' '))
                {
                    _position = new Point(_position.X + sprites[0].Width, _position.Y);
                }
                else if (index.TryGetValue(simbol,out value))
                {
                    _buffer.Graphics.DrawImage(sprites[value], new Rectangle(_position, Size));
                    _position = new Point(_position.X + sprites[0].Width, _position.Y);
                }
            }
        }

        public override void UpdatePosition()
        {
            
        }
    }
}
