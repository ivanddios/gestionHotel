

namespace ReservasHotel
{
	using System;
	using System.Windows.Forms;
	using ReservasHotel.View;

    class MainClass
    {
        public static void Main(string[] args)
        {

            //Gtk.Application.Init();
            var mainForm = new MainWindow();
            Application.Run(mainForm);
        }
    }
}
