using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinoGame.Objects
{
    class GameOver : GameObject
    {
        private static Image[] _image = {
            Image.FromFile("Resources\\gameover.png"),
            Image.FromFile("Resources\\restart.png")
        };

        public GameOver(int width, int height)
        {
            int xPosition = (width - _image[0].Size.Width)/2;
            int yPosition = (height - _image[0].Size.Height- _image[1].Size.Height-50)/2;
            Position = new Point(xPosition, yPosition);
        }

        public override void Draw(BufferedGraphics _buffer)
        {

            Size _size = new Size(_image[0].Size.Width, _image[0].Size.Height);
            _buffer.Graphics.DrawImage(_image[0], new Rectangle(Position, _size));
            int xPosition = ((_image[0].Size.Width - _image[1].Size.Width) / 2) + Position.X;
            int yPosition = Position.Y + 50 + _image[0].Size.Height;
            Point _position = new Point(xPosition, yPosition);
            _size = new Size(_image[1].Size.Width, _image[1].Size.Height);
            _buffer.Graphics.DrawImage(_image[1], new Rectangle(_position, _size));
            //_buffer.Graphics.DrawImage(_image[0], new Rectangle(new Point(0, 0), new Size(_image[0].Size.Width, _image[0].Size.Height)));
            //_buffer.Graphics.DrawImage(_image[1], new Rectangle(new Point(_image[0].Size.Height, 0), new Size(_image[1].Size.Width, _image[1].Size.Height)));
        }

        public override void UpdatePosition()
        {
            throw new NotImplementedException();
        }
    }
}
