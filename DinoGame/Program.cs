using System.Windows.Forms;

namespace DinoGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Form form = new Form();
            form.Width = 960;
            form.Height = 580;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
            //form.FormClosing += new FormClosingEventHandler(MyMethod);
            Game.Init(form);
            Game.Draw();
            Application.Run(form);
        }
        //static void MyMethod(object sender, System.EventArgs e)
        //{

        //}
    }
}
