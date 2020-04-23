using System.Windows.Forms;

namespace DinoGame
{
    class Program
    {
        static void Main(string[] args)
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
