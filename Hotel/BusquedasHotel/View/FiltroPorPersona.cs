namespace BusquedasHotel.View
{
    using System.Drawing;
    using System.Windows.Forms;


    public class FiltroPorPersona : Form
    {
        public FiltroPorPersona()
        {
            this.Build();
        }

        private Panel BuildPnlFiltroPersona()
        {
            var toret = new Panel { Dock = DockStyle.Right };

            this.edPersona = new TextBox { Dock = DockStyle.Fill };
            var lbPersona = new Label
            {
                Text = "Número de DNI:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edPersona);
            toret.Controls.Add(lbPersona);
            toret.MaximumSize = new Size(int.MaxValue, edPersona.Height * 2);

			this.edPersona.Validating += (sender, cancelArgs) => {
                var btAccept = (Button)this.AcceptButton;
				bool invalid = string.IsNullOrWhiteSpace(this.Persona);
                
                if (invalid)
                {
                    this.edPersona.Text = "Por favor, especifique un DNI Correcto.";
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

            toret.Controls.Add(btGuarda);
            toret.Controls.Add(btCierra);
            toret.Dock = DockStyle.Top;

            return toret;
        }
        private void Build()
        {
            this.SuspendLayout();

            var pnlFiltroPorPersona = new TableLayoutPanel { Dock = DockStyle.Fill };
            pnlFiltroPorPersona.SuspendLayout();
            this.Controls.Add(pnlFiltroPorPersona);

            var pnlFiltro = this.BuildPnlFiltroPersona();
            pnlFiltroPorPersona.Controls.Add(pnlFiltro);

            var pnlBotones = this.BuildPnlBotones();
            pnlFiltroPorPersona.Controls.Add(pnlBotones);

            pnlFiltroPorPersona.ResumeLayout(true);

            this.Text = "Filtro Por Persona ";
            this.Size = new Size(600,
                                 pnlFiltro.Height + pnlBotones.Height);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }

        private void OnValidate(System.ComponentModel.CancelEventArgs e)
        {
            bool toret = string.IsNullOrWhiteSpace(this.Persona);

            e.Cancel = toret;
        }

        public string Persona => this.edPersona.Text;


        private TextBox edPersona;
        private Button btPersona;
    }
}
