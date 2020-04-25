using System.Windows.Forms;

namespace Asteroids
{
    class Program
    {
        static void Main()
        {
            Form form = new Form();
            form.Width = 1024;
            form.Height = 768;
            form.Show();
            Game.Init(form);
            Game.Draw();
            Application.Run(form);
        }
    }
}