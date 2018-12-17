namespace BusquedasHotel.View
{
    using System.Drawing;
    using System.Windows.Forms;


	public class Disponibilidad : Form
    {
		public Disponibilidad()
        {
            this.Build();
        }
  
		private Panel BuildPnlBotones()
        {
            var toret = new TableLayoutPanel()
            {
                ColumnCount = 2,
                RowCount = 1
            };

            var btCierra = new Button()
            {
                DialogResult = DialogResult.Cancel,
                Text = "&No"
            };

            var btGuarda = new Button()
            {
                DialogResult = DialogResult.OK,
                Text = "&Sí"
            };

            this.AcceptButton = btGuarda;
            this.CancelButton = btCierra;

            toret.Controls.Add(btGuarda);
            toret.Controls.Add(btCierra);
            toret.Dock = DockStyle.Top;

            return toret;
        }

		private Panel BuildPnlTexto()
		{
			var toret = new Panel { Dock = DockStyle.Fill };
			var lbAnuncio = new Label
			{
				Text = "¿Desea visualizar las Habitaciones Disponibles?",
				Dock = DockStyle.Fill
			};
			toret.Controls.Add(lbAnuncio);

			return toret;
		}

		private void Build()
        {
			this.SuspendLayout();

            var pnlDisponibilidad = new TableLayoutPanel { Dock = DockStyle.Fill };
			pnlDisponibilidad.SuspendLayout();
			this.Controls.Add(pnlDisponibilidad);

            var pnlTexto = this.BuildPnlTexto();
            pnlDisponibilidad.Controls.Add(pnlTexto);
            var pnlBotones = this.BuildPnlBotones();
            pnlDisponibilidad.Controls.Add(pnlBotones);
			pnlDisponibilidad.ResumeLayout(true);

            this.Text = "Comprobacion de Disponibilidad ";
            this.Size = new Size(600,
			                     pnlBotones.Height+pnlTexto.Height);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }

        public string Persona => this.edPersona.Text;


		private Label lbAnuncio;
        private Panel pnlPpal;
		private DataGridView grdLista;
        private TextBox edPersona;
        private Button btPersona;
    }
}
