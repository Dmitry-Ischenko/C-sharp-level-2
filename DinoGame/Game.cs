using System;
using System.Collections.Generic;
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
        static List<GameObject> ObjectsList = new List<GameObject>();
        static public Random Random { get; } = new Random();
        static public int Width { get; private set; }
        static public int Height { get; private set; }
        static DinoObject dino;
        static Timer timer = new Timer();
        static GameOver gameOver;
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
            //Rand = new Random();
            Graphics g;
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            // Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            form.MaximumSize = new Size(form.Width,form.Height);
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            //form.KeyDown -= Form_KeyDown;
            form.KeyDown += Form_KeyDown;
            form.KeyUp += Form_KeyUp;
            //form.SizeChanged -= SizeChanged;
            //form.SizeChanged += SizeChanged;
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            timer.Interval = 1;
            //timer.Tick -= Timer_Tick;
            timer.Tick += Timer_Tick;
            hz = 0;
            start = 1;
            ObjectsList.Clear();
            Load();
        }

        private static void Form_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && timer.Enabled)
            {
                //Console.WriteLine("---------------------------");
                //Console.Write("Ked Up: ");
                dino.BendDown = 0;
            }
        }

        //private static void SizeChanged(object sender, EventArgs e)
        //{
        //    if (sender is Form form) {
        //        //Console.WriteLine($"Width:{form.Width} Height:{form.Height}");
        //    }
        //}

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && timer.Enabled)
            {
                //Console.Write("Ked Down: \n");
                dino.BendDown = 1;
            }
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Up)
            {
                if (!timer.Enabled && start == 0)
                {
                    if (hz > record) record = hz;
                    hz = 0;
                    ObjectsList.Clear();
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
                speed++;
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
            //speed = 0;
            CactusObject.Count = 0;
            PterodactylObject.Count = 0;
            if (ObjectsList.Count == 0)
            {
                for (int i = 1; i < 4; i++)
                {
                    ObjectsList.Add(new CloudObject(new Point(Width / i, Height / 4 / i), new Point(1, Width / 4), new Size(136, 35)));
                }
                ObjectsList.Add(new GroundObject(new Point(0, Height - Height / 5 + 70), new Point(speed, 0), new Size(0, 0)));
                ObjectsList.Add(new CactusObject(new Point(Width, Height - Height / 5 + 25), new Point(speed, 0), new Size(30, 70)));
                ObjectsList.Add(new DrawText(new Point(Width - Width / 4, 10), new Point(0, 0), new Size(21, 20)));                
            } 
            if (dino == null)
            {
                dino = new DinoObject(new Point(0, Height - Height / 5), new Point(0, 0), new Size(88, 94));
            }
            else
            {
                dino.AllDataUpdate(new Point(0, Height - Height / 5), new Point(0, 0), new Size(88, 94));
            }
            gameOver = new GameOver(Width, Height);

        }
        static public void Draw()
        {
            Buffer.Graphics.Clear(Color.FromArgb(255, 247, 247, 247));
            foreach (GameObject obj in ObjectsList)
                obj.Draw(Buffer);
            dino.Draw(Buffer);
            if (!timer.Enabled && start == 0)
            {
                gameOver.Draw(Buffer);
            }
            Buffer.Render();
        }
        static void GameOver()
        {
            timer.Stop();
            start = 0;
            dino.DinoDie();
        }
        static public void Update()
        {
            int updateGameObject = 0;
            GameObject objBuff=null;
            foreach (GameObject obj in ObjectsList)
            {
                switch (obj)
                {
                    case CloudObject obj1:
                    {
                            obj1.UpdatePosition();
                            break;
                    }
                    case GroundObject obj1:
                    {
                            obj1.PointMoving = new Point(speed, obj1.PointMoving.Y);
                            obj1.UpdatePosition();
                            break;
                    }
                    case DrawText obj1:
                    {
                            obj1.OutString = $"HI {record:000} {hz:000}";
                            break;
                    }
                    case CactusObject obj1:
                    {
                            obj1.PointMoving = new Point(speed, obj1.PointMoving.Y);
                            obj1.UpdatePosition();
                            if (obj1.Position.X + obj1.Size.Width < 0)
                            {
                                obj1.Position = new Point(Width, obj1.Position.Y);
                                obj1.UpdateIndex();
                                if (CactusObject.Count == 1 && Rand.Next(0,2) == 1)
                                {
                                    updateGameObject = 1;
                                } else if (CactusObject.Count == 2)
                                {
                                    updateGameObject = 2;
                                    objBuff = obj1;
                                }
                                if (hz > 5 && CactusObject.Count == 1 && Rand.Next(0, 2) == 1)
                                {
                                        updateGameObject = 3;
                                        objBuff = obj1;
                                }
                            }
                            if (dino.Collision(obj1))
                            {
                                GameOver();
                            }
                            break;
                    }
                    case PterodactylObject obj1:
                    {
                            obj1.PointMoving = new Point(speed, obj1.PointMoving.Y);
                            obj1.UpdatePosition();
                            if (obj1.Position.X + obj1.Size.Width < 0)
                            {
                                int yPosition = Rand.Next(0, 3);
                                obj1.Position = new Point(Width, Height - Height / 3 + 35 * yPosition);
                                if (PterodactylObject.Count == 1 && Rand.Next(0, 4) == 2)
                                {
                                    updateGameObject = 4;
                                } else if (PterodactylObject.Count == 2)
                                {
                                    updateGameObject = 5;
                                    objBuff = obj1;
                                } else if (PterodactylObject.Count == 1)
                                {
                                    updateGameObject = 6;
                                    objBuff = obj1;
                                }
                            }
                            if (dino.Collision(obj1))
                            {
                                GameOver();
                            }
                            break;
                    }
                    default:
                        break;
                }

            }        
            switch (updateGameObject)
            {
                case 1:
                    {
                        ObjectsList.Add(new CactusObject(new Point(Width + Rand.Next(300, 900), Height - Height / 5 + 25), new Point(speed, 0), new Size(30, 70)));
                        //Console.WriteLine("case 1");
                        break;
                    }
                case 2:
                    {
                        ObjectsList.Remove(objBuff);
                        CactusObject.Count--;
                        //Console.WriteLine("case 2");
                        break;
                    }
                case 3:
                    {
                        ObjectsList.Remove(objBuff);
                        CactusObject.Count--;
                        int yPosition = Rand.Next(0, 3);
                        ObjectsList.Add(new PterodactylObject(new Point(Width, Height - Height / 3 + 35 * yPosition), new Point(speed, 0), new Size(92, 68)));
                        //Console.WriteLine("case 3");
                        break;
                    }
                case 4:
                    {
                        int yPosition = Rand.Next(0, 3);
                        ObjectsList.Add(new PterodactylObject(new Point(Width + Rand.Next(300, 900), Height - Height / 3 + 35 * yPosition), new Point(speed, 0), new Size(92, 68)));
                        //Console.WriteLine("case 4");
                        break;
                    }
                case 5:
                    {
                        ObjectsList.Remove(objBuff);
                        PterodactylObject.Count--;
                        //Console.WriteLine("case 5");
                        break;
                    }
                case 6:
                    {
                        ObjectsList.Remove(objBuff);
                        PterodactylObject.Count--;
                        ObjectsList.Add(new CactusObject(new Point(Width, Height - Height / 5 + 25), new Point(speed, 0), new Size(30, 70)));
                        //Console.WriteLine("case 6");
                        break;
                    }
                default:
                    break;
            }
            dino.UpdatePosition();
        }
    }
}
 