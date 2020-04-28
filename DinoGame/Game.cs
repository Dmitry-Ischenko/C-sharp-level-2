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
        static DrawText showTextInWindow;
        //static DrawText showWandH;
        static GroundObject ground;
        static CactusObject Cactus;
        static Timer timer = new Timer();
        //static int speed = 1;
        static int tickCount = 0;
        static ulong hz = 0;
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
            form.KeyDown += Form_KeyDown;
            form.SizeChanged += SizeChanged;
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Interval = 16;
            timer.Tick += Timer_Tick;
            //timer.Start();
            Load();
        }

        private static void SizeChanged(object sender, EventArgs e)
        {
            if (sender is Form form) {
                Console.WriteLine($"Width:{form.Width} Height:{form.Height}");
            }
            
            //showWandH = $"{} {form.ClientSize.Height}";
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && timer.Enabled)
            {
                dino.Jump();
            }
            if(!timer.Enabled)
            {
                timer.Start();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            tickCount++;
            if (tickCount > 60)
            {
                hz++;
                tickCount = 0;
            }
            Update();
            Draw();
        }

        static public void Load()
        {
            for (int i = 1; i <= objs.Length; i++)
            {
                objs[i-1] = new CloudObject(new Point(Width/i, Height / 4/i), new Point(1, Width / 4), new Size(136, 35));
            }
            dino = new DinoObject(new Point(0, Height- Height/5), new Point(0,0), new Size(88,94));
            Cactus = new CactusObject(new Point(Width, Height - Height / 5+25), new Point(8, 0), new Size(30, 70));
            showTextInWindow = new DrawText(new Point(Width- Width/4, 10),new Point(0,0),new Size(21,20));
            //showWandH = new DrawText(new Point(Width/5, 10),new Point(0,0),new Size(21,20));
            ground = new GroundObject(new Point(0, Height - Height / 5 + 70), new Point(8, 0), new Size(0, 0));
        }
        static public void Draw()
        {
            Buffer.Graphics.Clear(Color.FromArgb(255, 247, 247, 247));
            //Buffer.Graphics.DrawLine(new Pen(Color.Black,2),new Point(0, Height - Height / 5+60),new Point(Width, Height - Height / 5 + 60));
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
            foreach (CloudObject obj in objs)
            {
                obj.UpdatePosition();
            }
            ground.UpdatePosition();            
            dino.UpdatePosition();
            Cactus.UpdatePosition();
            if (Cactus.Position.X+Cactus.Size.Width < 0) Cactus.Position = new Point(Width,Cactus.Position.Y);
            showTextInWindow.OutString = $"HI 00 {hz:000}";
            if (dino.Collision(Cactus))
            {
                timer.Stop();
            }
        }
    }
}
