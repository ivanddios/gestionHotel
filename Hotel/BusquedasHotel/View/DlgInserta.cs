namespace BusquedasHotel.View {
    using System.Drawing;
    using System.Windows.Forms;
    

    public class DlgInserta: Form {
        public DlgInserta()
        {
            this.Build();
        }
        
        private Panel BuildPnlDuracion()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edDuracion = new TextBox { Dock = DockStyle.Fill };
            var lbDuracion = new Label {
				Text = "Duración (en horas):",
                Dock = DockStyle.Left
            };
            
            toret.Controls.Add( this.edDuracion );
            toret.Controls.Add( lbDuracion );
            toret.MaximumSize = new Size( int.MaxValue, edDuracion.Height * 2 );
            
            this.edDuracion.Validating += (sender, cancelArgs) => {
                var btAccept = (Button) this.AcceptButton;
				int i;
				bool valid = int.TryParse(this.Duracion,out i);
                
                if ( !valid ) {
					this.edDuracion.Text = "Por favor, duración en números. Borre este texto y Escríbala " +
						"de nuevo";
                }
                
                btAccept.Enabled = valid;
                cancelArgs.Cancel = !valid;
            };
            
            return toret;
        }
        
        private Panel BuildPnlNombreAparato()
        {
            var toret = new Panel();
            this.edNombreAparato = new TextBox { Dock = DockStyle.Fill };
            var lbNombre = new Label()
            {
				Text = "Nombre Aparato Elec.:",
                Dock = DockStyle.Left
            };
            
            toret.Controls.Add( this.edNombreAparato );
			toret.Controls.Add( lbNombre );
            toret.Dock = DockStyle.Top;
            toret.MaximumSize = new Size( int.MaxValue, edNombreAparato.Height * 2 );        
            
            this.edNombreAparato.Validating += (sender, cancelArgs) => {
                var btAccept = (Button) this.AcceptButton;
				bool invalid = !this.edNombreAparato.Text.Equals("Radio")
				                    &&!this.edNombreAparato.Text.Equals("Televisor")
				                    &&!this.edNombreAparato.Text.Equals("Reproductor DVD")
				                    &&!this.edNombreAparato.Text.Equals("Adaptador TDT");

                if ( invalid ) {
					this.edNombreAparato.Text = "Por favor, especifique un nombre Correcto: Radio," +
						"Televisor, Adaptador TDT o Reproductor DVD";
                }
                
                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
            };
            
            return toret;
        }

        private Panel BuildPnlNserie()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edNserie = new TextBox { Dock = DockStyle.Fill };
            var lbNserie = new Label
            {
                Text = "Número de Serie:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edNserie);
            toret.Controls.Add(lbNserie);
            toret.MaximumSize = new Size(int.MaxValue, edNserie.Height * 2);

            this.edNserie.Validating += (sender, cancelArgs) => {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Nserie);

                invalid = invalid || !char.IsLetter(this.Nserie[0]);

                if (invalid)
                {
					this.edNserie.Text = "Por favor, especifique un nº de serie Correcto.";
                }

                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
            };

            return toret;
        }
		private Panel BuildPnlModelo()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edModelo = new TextBox { Dock = DockStyle.Fill };
            var lbModelo = new Label
            {
                Text = "Modelo:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edModelo);
            toret.Controls.Add(lbModelo);
            toret.MaximumSize = new Size(int.MaxValue, edModelo.Height * 2);

            this.edModelo.Validating += (sender, cancelArgs) => {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Modelo);

                invalid = invalid || !char.IsLetter(this.Modelo[0]);

                if (invalid)
                {
					this.edModelo.Text = "Por favor, especifique un Modelo Correcto.";
                }

                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
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

            toret.Controls.Add( btGuarda );
            toret.Controls.Add( btCierra );
            toret.Dock = DockStyle.Top;
            
            return toret;
        }

        private void Build()
        {
            this.SuspendLayout();

            var pnlInserta = new TableLayoutPanel { Dock = DockStyle.Fill };
            pnlInserta.SuspendLayout();
            this.Controls.Add( pnlInserta );

            var pnlDuracion = this.BuildPnlDuracion();
            pnlInserta.Controls.Add( pnlDuracion );

            var pnlNombreAparato = this.BuildPnlNombreAparato();
			pnlInserta.Controls.Add(pnlNombreAparato);

            var pnlNserie = this.BuildPnlNserie();
			pnlInserta.Controls.Add(pnlNserie);

            var pnlModelo = this.BuildPnlModelo();
            pnlInserta.Controls.Add(pnlModelo);

           

            
            var pnlBotones = this.BuildPnlBotones();
            pnlInserta.Controls.Add( pnlBotones );

            pnlInserta.ResumeLayout( true );
            
            this.Text = "Nueva Reparación";
            this.Size = new Size( 600, 
			                     pnlDuracion.Height + pnlNombreAparato.Height
			                     + pnlNserie.Height + pnlModelo.Height + pnlBotones.Height );
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout( false );
        }
        
        private void OnValidate(System.ComponentModel.CancelEventArgs e)
        {
			bool toret = string.IsNullOrWhiteSpace( this.Duracion );
            
			toret = toret || string.IsNullOrWhiteSpace( this.NombreAparato );
			toret = toret || string.IsNullOrWhiteSpace(this.Nserie);
			toret = toret || string.IsNullOrWhiteSpace(this.Modelo);
            
            e.Cancel = toret;
        }
        public string Duracion => this.edDuracion.Text;
		public string NombreAparato => this.edNombreAparato.Text;
		public string Nserie => this.edNserie.Text;
		public string Modelo => this.edModelo.Text;
  

        private TextBox edDuracion;
		private TextBox edNombreAparato;
		private TextBox edNserie;
		private TextBox edModelo;
    }    
}
