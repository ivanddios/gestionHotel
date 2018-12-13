using System.Drawing;
using System.Windows.Forms;

namespace Gestión_Hotel.UI
{
    class DlgInsertarCliente: Form
    {
        public DlgInsertarCliente()
        {
            this.Build();
        }


        void Build()
        {
            this.SuspendLayout();

            var pnlInserta = new TableLayoutPanel { Dock = DockStyle.Fill };
            pnlInserta.SuspendLayout();
            this.Controls.Add(pnlInserta);

            var pnlNombre = this.BuildNombrePanel();
            pnlInserta.Controls.Add(pnlNombre);

            var pnlDni = this.BuildDniPanel();
            pnlInserta.Controls.Add(pnlDni);

            var pnlTelefono = this.BuildTelefonoPanel();
            pnlInserta.Controls.Add(pnlTelefono);

            var pnlEmail = this.BuildEmailPanel();
            pnlInserta.Controls.Add(pnlEmail);

            var pnlDireccion = this.BuildDireccionPanel();
            pnlInserta.Controls.Add(pnlDireccion);

            var pnlBotones = this.BuildBotonesPanel();
            pnlInserta.Controls.Add(pnlBotones);

            pnlInserta.ResumeLayout(true);

            this.Text = "Añadir un nuevo cliente";
            this.Size = new Size(450,
                            pnlNombre.Height + pnlDni.Height
                            + pnlTelefono.Height + pnlEmail.Height
                            + pnlDireccion.Height + pnlBotones.Height);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }


        Panel BuildNombrePanel()
        {
            var pnlNombre = new Panel();
            this.tbNombre = new TextBox() { Dock = DockStyle.Fill };
            var lblNombre = new Label()
            {
                Text = "Nombre Completo",
                Dock = DockStyle.Left
            };

            pnlNombre.Controls.Add(this.tbNombre);
            pnlNombre.Controls.Add(lblNombre);
            pnlNombre.Dock = DockStyle.Top;
            pnlNombre.MaximumSize = new Size(int.MaxValue, tbNombre.Height * 2);

            return pnlNombre;
        }

        Panel BuildDniPanel()
        {
            var pnlDni = new Panel();
            this.tbDni = new TextBox() { Dock = DockStyle.Fill };
            var lblDni = new Label()
            {
                Text = "DNI",
                Dock = DockStyle.Left
            };

            pnlDni.Controls.Add(this.tbDni);
            pnlDni.Controls.Add(lblDni);
            pnlDni.Dock = DockStyle.Top;
            pnlDni.MaximumSize = new Size(int.MaxValue, tbDni.Height * 2);

            return pnlDni;
        }

        Panel BuildTelefonoPanel()
        {
            var pnlTelefono = new Panel();
            this.nudTelefono = new NumericUpDown
            {
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill,
                Maximum = 999999999,
                Minimum = 600000000
            };

            var lbNumSerie = new Label()
            {
                Text = "Teléfono",
                Dock = DockStyle.Left
            };
            pnlTelefono.Controls.Add(this.nudTelefono);
            pnlTelefono.Controls.Add(lbNumSerie);
            pnlTelefono.Dock = DockStyle.Top;
            pnlTelefono.MaximumSize = new Size(int.MaxValue, nudTelefono.Height * 2);

            return pnlTelefono;
        }

        Panel BuildEmailPanel()
        {
            var pnlEmail = new Panel();
            this.tbEmail = new TextBox() { Dock = DockStyle.Fill };
            var lblEmail = new Label()
            {
                Text = "Email",
                Dock = DockStyle.Left
            };

            pnlEmail.Controls.Add(this.tbEmail);
            pnlEmail.Controls.Add(lblEmail);
            pnlEmail.Dock = DockStyle.Top;
            pnlEmail.MaximumSize = new Size(int.MaxValue, tbEmail.Height * 2);

            return pnlEmail;
        }

        Panel BuildDireccionPanel()
        {
            var pnlDireccion = new Panel();
            this.tbDireccion = new TextBox() { Dock = DockStyle.Fill };
            var lblDireccion = new Label()
            {
                Text = "Dirección",
                Dock = DockStyle.Left
            };

            pnlDireccion.Controls.Add(this.tbDireccion);
            pnlDireccion.Controls.Add(lblDireccion);
            pnlDireccion.Dock = DockStyle.Top;
            pnlDireccion.MaximumSize = new Size(int.MaxValue, tbDireccion.Height * 2);

            return pnlDireccion;
        }

        private Panel BuildBotonesPanel()
        {
            var pnlBotones = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1
            };

            var btCierra = new Button();
            btCierra.DialogResult = DialogResult.Cancel;
            btCierra.Text = "&Cancelar";
            var btGuarda = new Button();
            btGuarda.DialogResult = DialogResult.OK;
            btGuarda.Text = "&Guardar";
            pnlBotones.Controls.Add(btGuarda);
            pnlBotones.Controls.Add(btCierra);
            pnlBotones.Dock = DockStyle.Top;

            this.AcceptButton = btGuarda;
            this.CancelButton = btCierra;

            pnlBotones.Controls.Add(btGuarda);
            pnlBotones.Controls.Add(btCierra);
            pnlBotones.Dock = DockStyle.Top;

            return pnlBotones;
        }



        private TextBox tbNombre;
        private TextBox tbDni;
        private NumericUpDown nudTelefono;
        private TextBox tbEmail;
        private TextBox tbDireccion;

        public string Nombre => this.tbNombre.Text;
        public string Dni => this.tbDni.Text;
        public int Telefono => (int)this.nudTelefono.Value;
        public string Email => this.tbEmail.Text;
        public string Direccion => this.tbDireccion.Text;
    }
}
