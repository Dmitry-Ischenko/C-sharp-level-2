using System.Windows.Forms;

namespace Asteroids
{
    class Program
    {
        static void Main()
        {
            Form form = new Form();
            //System.Drawing.Icon icon = new System.Drawing.Icon("game.ico");
            //form.Icon = icon;
            form.Width = 1024;
            form.Height = 768;
            form.Show();
            form.FormClosing += Form_FormClosing;
            Game.Init(form);
            Game.Draw();
            Application.Run(form);
        }

        private static void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
        }
    }
}