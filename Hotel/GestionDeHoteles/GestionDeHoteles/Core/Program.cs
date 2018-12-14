using GestionDeHoteles.GUI;

namespace GestionDeHoteles
{
    public class MainClass
    {
        public static void Main(string[] args){         
			Gtk.Application.Init();
			MainWindow main = new MainWindow(new XML.XMLBrowser(), 1280, 720);
			main.Show();
            Gtk.Application.Run();
        }
    }
}
