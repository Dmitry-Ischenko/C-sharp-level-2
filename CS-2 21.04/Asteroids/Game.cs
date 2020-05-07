using System;
using System.Windows.Forms;
using System.Drawing;
using Asteroids.Objects;

namespace Asteroids
{
    static class Game
    {
        static public ulong Timer { get; private set; } = 0;

        static BufferedGraphicsContext context;
        static public BufferedGraphics Buffer { get; private set; }

        // Свойства
        // Ширина и высота игрового поля

        static public Random Random { get; } = new Random();
        static public int Width { get;private set; }
        static public int Height { get;private set; }
        static public Image background = Image.FromFile("Images\\fon.jpg");
        static public Ship Ship { get; private set; }
        static BaseObject[] objs = new BaseObject[50];
        static Timer timer = new Timer();
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
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
            form.KeyDown += Form_KeyDown;
            Load();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) ;
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Timer++;
            Update();
            Draw();
        }

        static public void Load()
        {
            for (int i = 0; i < objs.Length/2; i++)
                objs[i] = new Asteroid(new Point(800, i*20), new Point(i, i), new Size(20, 20));
            for (int i = objs.Length / 2;i<objs.Length; i++)
                objs[i] = new Star(new Point(i * 3, i * 3), new Point(i, i), new Size(20, 20));
            Ship = new Ship(new Point(0, Height / 2));

        }


        static public void Draw()
        {
            // Проверяем вывод графики
            //Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawImage(background, 0, 0);
            //Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            foreach (BaseObject obj in objs)
                obj?.Draw();
            Ship.Draw();
            Buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs)
            {
                obj?.Update();
                if (obj is Asteroid)
                {
                    if (obj.Collision(Ship))
                    {
                        Ship.Low(1);
                        Console.WriteLine("Clash!");
                    }
                }
            }


        }

        
    }
}
