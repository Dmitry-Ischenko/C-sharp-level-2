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
        public static Random Rand { get; private set; }

        // Свойства
        // Ширина и высота игрового поля

        static public Random Random { get; } = new Random();
        static public int Width { get; private set; }
        static public int Height { get; private set; }
        static CloudObject[] objs = new CloudObject[3];
        static DinoObject dino;
        static DrawText showTextInWindow;
        static GroundObject ground;
        static CactusObject Cactus;
        static Timer timer = new Timer();
        static int tickCount = 0;
        static ulong hz = 0;
        static int speed = 1;
        static ulong record = 0;
        static int start = 0;

        static Game()
        {

        }

        static public void Init(Form form)
        {
            // Графическое устройство для вывода графики            
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            Rand = new Random();
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            // Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            //form.KeyDown -= Form_KeyDown;
            form.KeyDown += Form_KeyDown;
            //form.SizeChanged -= SizeChanged;
            form.SizeChanged += SizeChanged;
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Interval = 1;
            //timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;
            hz = 0;
            start = 1;
            Load();
        }

        private static void SizeChanged(object sender, EventArgs e)
        {
            if (sender is Form form) {
                Console.WriteLine($"Width:{form.Width} Height:{form.Height}");
            }
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (!timer.Enabled && start == 0)
                {
                    if (hz > record) record = hz;
                    hz = 0;
                    Load();
                    Draw();
                    timer.Start();
                }
                else if (!timer.Enabled && start == 1)
                {
                    timer.Start();
                } else
                {
                    dino.Jump();
                }
            }
            if (e.KeyCode == Keys.Z)
            {
                if (timer.Enabled)
                {
                    timer.Stop();
                } else
                {
                    timer.Start();
                }
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            tickCount++;
            if (tickCount > 1000)
            {
                //speed++;
                tickCount = 0;
            }
            if (tickCount%100 == 0)
            {
                hz++;
            }
            Update();
            Draw();
        }

        static public void Load()
        {
            tickCount = 0;
            speed = 8;
            for (int i = 1; i <= objs.Length; i++)
            {
                if (objs[i-1] == null)
                {
                    objs[i - 1] = new CloudObject(new Point(Width / i, Height / 4 / i), new Point(1, Width / 4), new Size(136, 35));
                } else
                {
                    objs[i - 1].AllDateUpdate(new Point(Width / i, Height / 4 / i), new Point(1, Width / 4), new Size(136, 35));
                }
            }
            if (dino == null)
            {
                dino = new DinoObject(new Point(0, Height - Height / 5), new Point(0, 0), new Size(88, 94));
            } else
            {
                dino.AllDateUpdate(new Point(0, Height - Height / 5), new Point(0, 0), new Size(88, 94));
            }
            if (Cactus == null)
            {
                Cactus = new CactusObject(new Point(Width, Height - Height / 5 + 25), new Point(speed, 0), new Size(30, 70));
            } else
            {
                Cactus.AllDateUpdate(new Point(Width, Height - Height / 5 + 25), new Point(speed, 0), new Size(30, 70));
            }
            if (showTextInWindow == null)
            {
                showTextInWindow = new DrawText(new Point(Width - Width / 4, 10), new Point(0, 0), new Size(21, 20));
            }
            if (ground == null)
            {
                ground = new GroundObject(new Point(0, Height - Height / 5 + 70), new Point(speed, 0), new Size(0, 0));
            } else
            {
                ground.AllDateUpdate(new Point(0, Height - Height / 5 + 70), new Point(speed, 0), new Size(0, 0));
            }
            
        }
        static public void Draw()
        {
            Buffer.Graphics.Clear(Color.FromArgb(255, 247, 247, 247));
            ground.Draw(Buffer);
            foreach (CloudObject obj in objs)
                obj.Draw(Buffer);            
            Cactus.Draw(Buffer);
            dino.Draw(Buffer);
            showTextInWindow.Draw(Buffer);
            Buffer.Render();
        }

        static public void Update()
        {
            Cactus.PointMoving = new Point(speed,Cactus.PointMoving.Y);
            ground.PointMoving = new Point(speed, ground.PointMoving.Y);
            foreach (CloudObject obj in objs)
            {
                obj.UpdatePosition();
            }
            ground.UpdatePosition();            
            dino.UpdatePosition();
            Cactus.UpdatePosition();
            if (Cactus.Position.X + Cactus.Size.Width < 0)
            {
                Cactus.Position = new Point(Width, Cactus.Position.Y);
                Cactus.UpdateIndex();
            }
            showTextInWindow.OutString = $"HI {record:00} {hz:000}";
            if (dino.Collision(Cactus))
            {
                timer.Stop();
                start = 0;
            }
        }
    }
}
