using System;
namespace GestionDeHoteles.GUI
{
    public partial class MainWindow
    {
		public void setVisible(Gtk.Button boton){
			boton.Visible = true;
		}

        public void setVisible(Gtk.Widget x){
            x.Visible = true;
        }

        private Gtk.Button InitButton(Gtk.Button bt, String label){
            bt = new Gtk.Button();
            bt.Label = label;
            setVisible(bt);
            return bt;
        }
    }
}
