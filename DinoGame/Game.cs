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
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Interval = 1;
            timer.Tick += Timer_Tick;
            timer.Start();
            Load();
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
        }
        static public void Draw()
        {
            Buffer.Graphics.Clear(Color.Gray);
            foreach (CloudObject obj in objs)
                obj.Draw(Buffer);
            Buffer.Render();
        }

        static public void Update()
        {
            foreach (CloudObject obj in objs)
                obj.UpdatePosition(speed);
        }
    }
}
