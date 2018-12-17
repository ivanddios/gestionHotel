using System.Drawing;
using System.Windows.Forms;

namespace Habitaciones.UI
{
    public class DlgInsertaHabitacion : Form
    {
        public DlgInsertaHabitacion()
        {
            this.Build();
        }


        void Build()
        {
            this.SuspendLayout();

            var pnlInserta = new TableLayoutPanel { Dock = DockStyle.Fill };
            pnlInserta.SuspendLayout();
            this.Controls.Add(pnlInserta);

            var pnlTipo = this.BuildTipoPanel();
            pnlInserta.Controls.Add(pnlTipo);

            var pnlFechaReserva = this.BuildFechaReservaPanel();
            pnlInserta.Controls.Add(pnlFechaReserva);

            var pnlIdentificador = this.BuildIdentificadorPanel();
            pnlInserta.Controls.Add(pnlIdentificador);

            var pnlFechaRenovacion = this.BuildFechaRenovacionPanel();
            pnlInserta.Controls.Add(pnlFechaRenovacion);

            var pnlComodidades = this.BuildComodidadesPanel();
            pnlInserta.Controls.Add(pnlComodidades);

            var pnlBotones = this.BuildBotonesPanel();
            pnlInserta.Controls.Add(pnlBotones);

            pnlInserta.ResumeLayout(true);

            this.Text = "Añadir una nueva habitación";
            this.Size = new Size(450,
                            pnlTipo.Height + pnlFechaReserva.Height
                            + pnlIdentificador.Height + pnlFechaRenovacion.Height
                            + pnlComodidades.Height + pnlBotones.Height);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }


        Panel BuildTipoPanel()
        {
            var pnlTipo = new Panel();
            this.tbTipo = new TextBox() { Dock = DockStyle.Fill };
            var lblTipo = new Label()
            {
                Text = "Tipo",
                Dock = DockStyle.Left
            };

            pnlTipo.Controls.Add(this.tbTipo);
            pnlTipo.Controls.Add(lblTipo);
            pnlTipo.Dock = DockStyle.Top;
            pnlTipo.MaximumSize = new Size(int.MaxValue, tbTipo.Height * 2);

            return pnlTipo;
        }

        Panel BuildFechaReservaPanel()
        {
            var pnlFechaReserva = new Panel();
			this.tbFechaReserva = new DateTimePicker { Dock = DockStyle.Fill };
			this.tbFechaReserva.Format = DateTimePickerFormat.Custom;
			this.tbFechaReserva.CustomFormat = "dd/MM/yyyy";
            var lblFechaReserva = new Label()
            {
                Text = "Fecha Reserva",
                Dock = DockStyle.Left
            };

            pnlFechaReserva.Controls.Add(this.tbFechaReserva);
            pnlFechaReserva.Controls.Add(lblFechaReserva);
            pnlFechaReserva.Dock = DockStyle.Top;
            pnlFechaReserva.MaximumSize = new Size(int.MaxValue, tbFechaReserva.Height * 2);

            return pnlFechaReserva;
        }

        Panel BuildIdentificadorPanel()
        {
            var pnlIdentificador = new Panel();
            this.nudIdentificador = new NumericUpDown
            {
                //Value = 0,
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill,
                Minimum = 101,
                Maximum = 999
            };

            var lbNumSerie = new Label()
            {
                Text = "Identificador",
                Dock = DockStyle.Left
            };
            pnlIdentificador.Controls.Add(this.nudIdentificador);
            pnlIdentificador.Controls.Add(lbNumSerie);
            pnlIdentificador.Dock = DockStyle.Top;
            pnlIdentificador.MaximumSize = new Size(int.MaxValue, nudIdentificador.Height * 2);

            return pnlIdentificador;
        }

        Panel BuildFechaRenovacionPanel()
        {
			var pnlFechaRenovacion = new Panel();
			this.tbFechaRenovacion = new DateTimePicker { Dock = DockStyle.Fill };
            this.tbFechaRenovacion.Format = DateTimePickerFormat.Custom;
            this.tbFechaRenovacion.CustomFormat = "dd/MM/yyyy";
            var lblFechaRenovacion = new Label()
            {
                Text = "Renovación",
                Dock = DockStyle.Left
            };

            pnlFechaRenovacion.Controls.Add(this.tbFechaRenovacion);
            pnlFechaRenovacion.Controls.Add(lblFechaRenovacion);
            pnlFechaRenovacion.Dock = DockStyle.Top;
            pnlFechaRenovacion.MaximumSize = new Size(int.MaxValue, tbFechaRenovacion.Height * 2);

            return pnlFechaRenovacion;
        }

        Panel BuildComodidadesPanel()
        {
            var pnlComodidades = new Panel();
            this.tbComodidades = new TextBox() { Dock = DockStyle.Fill };
            var lblComodidades = new Label()
            {
                Text = "Comodidades",
                Dock = DockStyle.Left
            };

            pnlComodidades.Controls.Add(this.tbComodidades);
            pnlComodidades.Controls.Add(lblComodidades);
            pnlComodidades.Dock = DockStyle.Top;
            pnlComodidades.MaximumSize = new Size(int.MaxValue, tbComodidades.Height * 2);

            return pnlComodidades;
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



        private TextBox tbTipo;
		private DateTimePicker tbFechaReserva;
        private NumericUpDown nudIdentificador;
		private DateTimePicker tbFechaRenovacion;
        private TextBox tbComodidades;

        public string Tipo => this.tbTipo.Text;
		public string FechaReserva => this.tbFechaReserva.Value.Date.ToShortDateString();
        public int Identificador => (int)this.nudIdentificador.Value;
		public string FechaRenovacion => this.tbFechaRenovacion.Value.Date.ToShortDateString();
        public string Comodidades => this.tbComodidades.Text;
    }
}
