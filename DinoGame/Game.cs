using System;
using System.Drawing;
using System.Windows.Forms;
using DinoGame.Objects;

namespace DinoGame
{
    static class Game
    {
        static BufferedGraphicsContext context;
        static public BufferedGraphics Buffer { get; private set; }

        // Свойства
        // Ширина и высота игрового поля

        static public Random Random { get; } = new Random();
        static public int Width { get; private set; }
        static public int Height { get; private set; }
        //static public Image background = Image.FromFile("Images\\fon.jpg");
        static CloudObject[] objs = new CloudObject[3];
        static DinoObject dino;
        static Timer timer = new Timer();
        static int speed = 1;
        static int tickCount = 0;
        static Game()
        {

        }

        static public void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            form.KeyDown += new KeyEventHandler(Form_KeyDown);
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Interval = 16;
            timer.Tick += Timer_Tick;
            timer.Start();
            Load();
        }
        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                dino.Jump();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            tickCount++;
            if (tickCount>1000)
            {
                speed++;
                tickCount = 0;
            }
            Update();
            Draw();
        }

        static public void Load()
        {
            for (int i = 1; i <= objs.Length; i++)
            {
                objs[i-1] = new CloudObject(new Point(Width/i, Height / 4/i), new Point(speed / 4, Width / 4), new Size(136, 35));
            }
            dino = new DinoObject(new Point(0, Height- Height/5), new Point(0,0), new Size(100,100));
        }
        static public void Draw()
        {
            Buffer.Graphics.Clear(Color.FromArgb(255, 247, 247, 247));
            Buffer.Graphics.DrawLine(new Pen(Color.Black,2),new Point(0, Height - Height / 5+60),new Point(Width, Height - Height / 5 + 60));
            foreach (CloudObject obj in objs)
                obj.Draw(Buffer);
            dino.Draw(Buffer);
            Buffer.Render();
        }

        static public void Update()
        {
            foreach (CloudObject obj in objs)
            {
                obj.PointMoving = new Point(speed,0);
                obj.UpdatePosition();
            }
            dino.SpeedAnimation = speed;
            dino.UpdatePosition();
        }
    }
}
