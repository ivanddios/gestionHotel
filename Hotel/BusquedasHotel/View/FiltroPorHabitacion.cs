namespace BusquedasHotel.View
{
	using System.Drawing;
	using System.Windows.Forms;


	public class FiltroPorHabitacion : Form
	{
		public FiltroPorHabitacion()
		{
			this.Build();
		}

		private Panel BuildPnlFiltroHabitacion()
		{
			var toret = new Panel { Dock = DockStyle.Right };

			this.edHabitacion = new TextBox { Dock = DockStyle.Fill };
			var lbHabitacion = new Label
			{
				Text = "Número de Habitación:",
				Dock = DockStyle.Left
			};

			toret.Controls.Add(this.edHabitacion);
			toret.Controls.Add(lbHabitacion);
			toret.MaximumSize = new Size(int.MaxValue, edHabitacion.Height * 2);

			this.edHabitacion.Validating += (sender, cancelArgs) =>
			{
				int i;
				var btAccept = (Button)this.AcceptButton;
				bool valid = int.TryParse(this.Habitacion, out i);

				if (!valid)
				{
					this.edHabitacion.Text = "Por favor, habitación en números. Borre este texto y Escríbala " +
						"de nuevo";
				}

				btAccept.Enabled = valid;
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

		private Panel BuildPnlFiltroPiso()
        {
            var toret = new Panel { Dock = DockStyle.Right };

            this.edPiso = new TextBox { Dock = DockStyle.Fill };
            var lbPiso = new Label
            {
                Text = "Piso de Habitación:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edPiso);
			toret.Controls.Add(lbPiso);
            toret.MaximumSize = new Size(int.MaxValue, edPiso.Height * 2);

            this.edPiso.Validating += (sender, cancelArgs) =>
            {
                int i;
                var btAccept = (Button)this.AcceptButton;
                bool valid = int.TryParse(this.Piso, out i);

                if (!valid)
                {
                    this.edPiso.Text = "Por favor, piso en números de al 0 al 9. Borre este texto y Escríbala " +
                        "de nuevo";
                }

                btAccept.Enabled = valid;
                //cancelArgs.Cancel = !valid;
            };         

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

			var pnlFiltroPorHabitacion = new TableLayoutPanel { Dock = DockStyle.Fill };
			pnlFiltroPorHabitacion.SuspendLayout();
			this.Controls.Add(pnlFiltroPorHabitacion);

			var pnlFiltro = this.BuildPnlFiltroHabitacion();
			pnlFiltroPorHabitacion.Controls.Add(pnlFiltro);

			var pnlPiso = this.BuildPnlFiltroPiso();
			pnlFiltroPorHabitacion.Controls.Add(pnlPiso);

			var pnlBotones = this.BuildPnlBotones();
			pnlFiltroPorHabitacion.Controls.Add(pnlBotones);

			pnlFiltroPorHabitacion.ResumeLayout(true);

			this.Text = "Filtro Por Habitacion ";
			this.Size = new Size(600,
			                     pnlFiltro.Height + pnlPiso.Height + pnlBotones.Height);
			this.FormBorderStyle = FormBorderStyle.FixedDialog;
			this.MinimizeBox = false;
			this.MaximizeBox = false;
			this.StartPosition = FormStartPosition.CenterParent;
			this.ResumeLayout(false);
		}
        
		private void OnValidate(System.ComponentModel.CancelEventArgs e)
		{
			bool toret = string.IsNullOrWhiteSpace(this.Habitacion);
			bool toret2 = string.IsNullOrWhiteSpace(this.Piso);

			e.Cancel = toret || toret2;
		}

		public string Habitacion => this.edHabitacion.Text;
		public string Piso => this.edPiso.Text;

		private TextBox edPiso;
		private TextBox edHabitacion;
		private Button btHabitacion;
	}
}
        
