using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ReservasHotel.View
{
    class DlgMensaje:Form
    {

        public DlgMensaje(string m)
        {
            mensaje = m;
            this.Build();

        }

        private void Build()
        {
            this.SuspendLayout();
            this.pnlMensaje = new Panel()
            {
                Dock = DockStyle.Fill
            };

            this.pnlMensaje.SuspendLayout();
            this.Controls.Add(this.pnlMensaje);
            this.pnlMensaje.Controls.Add(this.BuildPnlMensaje());
            this.pnlMensaje.Controls.Add(this.BuildPnlBotones());
            this.pnlMensaje.ResumeLayout(false);

            this.MinimumSize = new Size(300, 30);
            this.MaximumSize = new Size(400, 150);
            this.Text = "Mensaje";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }


        private Panel BuildPnlMensaje()
        {
            var toret = new Panel { Dock = DockStyle.Top };

            

            var lbNombre = new Label
            {
                Text = this.mensaje,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Padding = new Padding(0,10,0,0)


        };

            toret.Controls.Add(lbNombre);
            toret.MinimumSize = new Size(300, 20);
            toret.MaximumSize = new Size(300, 35);

            return toret;
        }



        private Panel BuildPnlBotones()
        {
            var toret = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1
            };

            var btAceptar = new Button()
            {
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Right,
                Text = "&Aceptar"

        };
            

            this.AcceptButton = btAceptar;
 
            toret.Controls.Add(btAceptar);
            //toret.MinimumSize = new Size(200, 20);
            toret.MaximumSize = new Size(300, 40);
            toret.Dock = DockStyle.Bottom;

            return toret;
        }


        private Panel pnlMensaje;
        private string mensaje;

    }
}
