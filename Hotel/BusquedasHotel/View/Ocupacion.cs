namespace BusquedasHotel.View
{
    using System.Drawing;
    using System.Windows.Forms;

    public class Ocupacion : Form
    {
		public Ocupacion()
        {
            this.Build();
        }

		private Panel BuildPnlFiltroAnho()
        {
            var toret = new Panel { Dock = DockStyle.Right };

            this.edAnho = new TextBox { Dock = DockStyle.Fill };
            var lbAnho = new Label
            {
                Text = "Año:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edAnho);
            toret.Controls.Add(lbAnho);
            toret.MaximumSize = new Size(int.MaxValue, edAnho.Height * 2);

            this.edAnho.Validating += (sender, cancelArgs) =>
            {
                int i;
                var btAccept = (Button)this.AcceptButton;
                bool valid = int.TryParse(this.Anho, out i);

                if (!valid)
                {
                    this.edAnho.Text = "";
                }

                //btAccept.Enabled = valid;
                //cancelArgs.Cancel = !valid;
            };
            /* this.btHabitacion = new Button();
             this.btHabitacion.Dock = DockStyle.Bottom;
             this.btHabitacion.Text = "Filtrar";
             this.btHabitacion.Click += BotonFiltroClickado;
             toret.Controls.Add(this.btHabitacion);
             //toret.Controls.Add(this.BuildPnlBotones());*/


            return toret;
        }

        private Panel BuildPnlFiltroFecha()
        {
			var toret = new Panel { Dock = DockStyle.Fill};

			this.edFecha = new DateTimePicker();
			this.edFecha.CustomFormat = "MMMM dd, yyyy - dddd";
			this.edFecha.Format = DateTimePickerFormat.Custom;
			this.edFecha.Location = new System.Drawing.Point(25, 12);
			this.edFecha.Name = "datepicker";
			this.edFecha.Size = new System.Drawing.Size(200, 200);
			this.edFecha.TabIndex = 0;
			this.edFecha.Dock = DockStyle.Fill;
            var lbFecha = new Label
            {
                Text = "Fecha:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edFecha);
            toret.Controls.Add(lbFecha);
            toret.MaximumSize = new Size(int.MaxValue, edFecha.Height * 2);
			/*var btAccept = (Button)this.AcceptButton;
			btAccept.Enabled = true;         */
            return toret;
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
                Text = "&Cancelar"
            };

            var btGuarda = new Button()
            {
                DialogResult = DialogResult.OK,
                Text = "&Guardar"
            };

            this.AcceptButton = btGuarda;
            this.CancelButton = btCierra;

            toret.Controls.Add(btGuarda);
            toret.Controls.Add(btCierra);
            toret.Dock = DockStyle.Top;

            return toret;
        }
        private void Build()
        {
            this.SuspendLayout();
   

            var pnlFiltroOcupacion = new TableLayoutPanel { Dock = DockStyle.Fill };
			pnlFiltroOcupacion.SuspendLayout();
			this.Controls.Add(pnlFiltroOcupacion);

            var pnlFiltro = this.BuildPnlFiltroAnho();
			pnlFiltroOcupacion.Controls.Add(pnlFiltro);

			var pnlFecha = this.BuildPnlFiltroFecha();
			pnlFiltroOcupacion.Controls.Add(pnlFecha);
            var pnlBotones = this.BuildPnlBotones();
            pnlFiltroOcupacion.Controls.Add(pnlBotones);

            pnlFiltroOcupacion.ResumeLayout(true);

            this.Text = "Filtro Por Persona ";
            this.Size = new Size(600,
			                     pnlFiltro.Height+ pnlBotones.Height + pnlFecha.Height);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }

        private void OnValidate(System.ComponentModel.CancelEventArgs e)
        {
			/*bool toret = string.IsNullOrWhiteSpace(this.Anho);

            e.Cancel = toret;*/
        }

		//public string Fecha => this.edFecha.ToString;
		public string Anho => this.edAnho.Text;
		public System.DateTime Fecha => this.edFecha.Value;

		private DateTimePicker edFecha;
		private TextBox edAnho;
        private Button btPersona;
    }
}
