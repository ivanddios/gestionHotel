namespace ReservasHotel.View
{
	using System;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using Habitaciones.Core;
    using Habitaciones.XML;
    using Gestión_Hotel.XML;
    using Gestión_Hotel.Core;

	public partial class DlgAltaReserva: Form
    {
	

        private Panel BuildPnlNombreCliente()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edNombre = new TextBox { Dock = DockStyle.Fill }; 
            if (reservaModificar != null)
            {
                this.edNombre.AppendText(reservaModificar.Cliente.Nombre);
            }
            var lbNombre = new Label
            {
                Text = "Nombre:",
                Dock = DockStyle.Left
            };

			toret.Controls.Add(this.edNombre);
			toret.Controls.Add(lbNombre);
			toret.MaximumSize = new Size(int.MaxValue, edNombre.Height * 2);

			this.edNombre.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
				bool invalid = string.IsNullOrWhiteSpace(this.Nombre);

				invalid = invalid || !char.IsLetter(this.Nombre[0]);

                if (invalid)
                {
					this.edNombre.Text = "¿Nombre?";
                }

                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
            };

            return toret;
        }

        private Panel BuildPnlApellidosCliente()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edApellidos = new TextBox { Dock = DockStyle.Fill };
            if (reservaModificar != null)
            {
                this.edApellidos.AppendText(reservaModificar.Cliente.Nombre);
            }
            var lbApellidos = new Label
            {
                Text = "Apellidos:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edApellidos);
            toret.Controls.Add(lbApellidos);
            toret.MaximumSize = new Size(int.MaxValue, edApellidos.Height * 2);

            this.edApellidos.Validating += (sender, cancelArgs) =>
            {
                var btAccept = (Button)this.AcceptButton;
                bool invalid = string.IsNullOrWhiteSpace(this.Apellidos);

                invalid = invalid || !char.IsLetter(this.Apellidos[0]);

                if (invalid)
                {
                    this.edApellidos.Text = "¿Apellidos?";
                }

                btAccept.Enabled = !invalid;
                cancelArgs.Cancel = invalid;
            };

            return toret;
        }

        public Panel BuildPnlFechaEntrada()
        {
            var toret = new Panel { Dock = DockStyle.Top };

            // Create a new DateTimePicker control and initialize it.
            this.fechaEntrada = new DateTimePicker { Dock = DockStyle.Fill };

            var lbFechaEntrada = new Label
            {
                Text = "Fecha entrada:",
                Dock = DockStyle.Left
            };
            // Set the MinDate and MaxDate.
            fechaEntrada.MaxDate = new DateTime(2030, 12, 30);
            if (reservaModificar != null)
            {
                this.fechaEntrada.Value = this.reservaModificar.FechaEntrada;
                fechaEntrada.MinDate = new DateTime(2018, 10, 1);
            }
            else
            {
                fechaEntrada.MinDate = DateTime.Now;
            }
            

            // Set the CustomFormat string.
            fechaEntrada.CustomFormat = "dd, MMMM yyyy - dddd";
            fechaEntrada.Format = DateTimePickerFormat.Custom;


            var fEntradaOriginal = this.fechaEntrada.Value;
            this.fechaEntrada.ValueChanged += (sender, e) => this.comprobarHabitacionesDisponibles();
            toret.Controls.Add(fechaEntrada);
            toret.Controls.Add(lbFechaEntrada);
            toret.MaximumSize = new Size(int.MaxValue, fechaEntrada.Height * 2);

            return toret;
        }

        public Panel BuildPnlFechaSalida()
        {
            var toret = new Panel { Dock = DockStyle.Top };

            // Create a new DateTimePicker control and initialize it.
            this.fechaSalida = new DateTimePicker { Dock = DockStyle.Fill };

            var lbFechaSalida = new Label
            {
                Text = "Fecha salida:",
                Dock = DockStyle.Left
            };

            // Set the MinDate and MaxDate.
            fechaSalida.MaxDate = new DateTime(2030, 12, 30);
            
            if (reservaModificar != null)
            {
                this.fechaSalida.Value = this.reservaModificar.FechaSalida;
                fechaSalida.MinDate = new DateTime(2018, 10, 1);
            }
            else
            {
                fechaSalida.MinDate = DateTime.Now;
            }
            // Set the CustomFormat string.
            fechaSalida.CustomFormat = "dd, MMMM yyyy - dddd";
            fechaSalida.Format = DateTimePickerFormat.Custom;

            var fSalidaOriginal = this.fechaSalida.Value;
            this.fechaSalida.ValueChanged += (sender, e) => this.comprobarHabitacionesDisponibles();
            toret.Controls.Add(fechaSalida);
            toret.Controls.Add(lbFechaSalida);
            toret.MaximumSize = new Size(int.MaxValue, fechaSalida.Height * 2);

            return toret;
        }

        private Panel BuildPnlGaraje()
        {
            var toret = new Panel();
            this.edGaraje = new CheckBox();
            if (reservaModificar != null)
            {
                this.edGaraje.Checked = this.reservaModificar.UsaGaraje;
            }
            this.edGaraje.Text = "Garaje";
            this.edGaraje.TextAlign = ContentAlignment.MiddleRight;
            
            this.edGaraje.CheckedChanged += (sender, e) => this.ActualizarTarifaGaraje();
            toret.Controls.Add(this.edGaraje);
            toret.Dock = DockStyle.Top;
            toret.MaximumSize = new Size(int.MaxValue, edGaraje.Height * 2);

            return toret;
        }

        private Panel BuildPnlTarifa()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edTarifa = new NumericUpDown
            {
                Value = 0,
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill,
                DecimalPlaces = 2,
                Increment = 0.25M,
                Minimum = 0.0M
            };

            if (reservaModificar != null)
            {
                this.edTarifa.Value = (decimal) this.reservaModificar.TarifaDia;
            }

            var lbTarifa = new Label
            {
                Text = "Tarifa:",
                Dock = DockStyle.Left
            };

            this.edTarifa.ValueChanged += (sender, e) => this.calcularTotal();

            toret.Controls.Add(this.edTarifa);
            toret.Controls.Add(lbTarifa);
            toret.MaximumSize = new Size(int.MaxValue, edTarifa.Height * 2);


            return toret;
        }

        private Panel BuildPnlTotal()
        {
            var toret = new Panel { Dock = DockStyle.Top };
            this.edTotal = new TextBox
            {
                Text = "0",
                TextAlign = HorizontalAlignment.Right,
                Dock = DockStyle.Fill,
                ReadOnly = true
            };

            if (reservaModificar != null)
            {
                this.edTotal.Text = this.reservaModificar.calcularTotal().ToString();
            }

            var lbTotal = new Label
            {
                Text = "Total:",
                Dock = DockStyle.Left
            };

            toret.Controls.Add(this.edTotal);
            toret.Controls.Add(lbTotal);
            toret.MaximumSize = new Size(int.MaxValue, edTotal.Height * 2);


            return toret;
        }




        private Panel BuildPanelLista()
        {
            var pnlLista = new Panel();
            pnlLista.SuspendLayout();
            pnlLista.Dock = DockStyle.Fill;

            // Crear gridview
            this.grdLista = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                MultiSelect = false,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            this.grdLista.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.grdLista.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            var textCellTemplate0 = new DataGridViewTextBoxCell();
            var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            var textCellTemplate3 = new DataGridViewTextBoxCell();
            var textCellTemplate4 = new DataGridViewTextBoxCell();
            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.BackColor = Color.Lavender;
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            textCellTemplate2.Style.BackColor = Color.Lavender;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.BackColor = Color.Lavender;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.BackColor = Color.Lavender;
            textCellTemplate4.Style.ForeColor = Color.Black;


            var column0 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "Habitacion",
                ReadOnly = true,
                Width = 80

            };

            var column1 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Tipo",
                ReadOnly = true,
                Width = 100
            };

            var column2 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Comodidades",
                ReadOnly = true,
                Width = 195

            };
           
            
            

            this.grdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2
            });

            pnlLista.Controls.Add(this.grdLista);
            pnlLista.ResumeLayout(false);

            return pnlLista;
        }


        private Panel BuildPanelListaClientes()
        {
            var pnlLista = new Panel();
            pnlLista.SuspendLayout();
            pnlLista.Dock = DockStyle.Fill;

            // Crear gridview
            this.grdListaClientes = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoGenerateColumns = false,
                MultiSelect = false,
                AllowUserToAddRows = false,
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            this.grdListaClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.grdListaClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            var textCellTemplate0 = new DataGridViewTextBoxCell();
            var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            var textCellTemplate3 = new DataGridViewTextBoxCell();
            var textCellTemplate4 = new DataGridViewTextBoxCell();
            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.BackColor = Color.Lavender;
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            textCellTemplate2.Style.BackColor = Color.Lavender;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.BackColor = Color.Lavender;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.BackColor = Color.Lavender;
            textCellTemplate4.Style.ForeColor = Color.Black;


            var column0 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "DNI",
                ReadOnly = true,
                Width = 120

            };

            var column1 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Nombre",
                ReadOnly = true,
                Width = 230
            };

            




            this.grdListaClientes.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1
            });

            pnlLista.Controls.Add(this.grdListaClientes);
            pnlLista.ResumeLayout(false);

            return pnlLista;
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
                DialogResult = DialogResult.None,
                Text = "&Guardar"
            };

            btGuarda.Click += (sender, e) => this.comprobarReserva();

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

            var pnlAlta = new TableLayoutPanel { Dock = DockStyle.Fill };
			pnlAlta.SuspendLayout();
			this.Controls.Add(pnlAlta);
            
			//var pnlNombreCliente = this.BuildPnlNombreCliente();
			//pnlAlta.Controls.Add(pnlNombreCliente);

            //var pnlApellidosCliente = this.BuildPnlApellidosCliente();
            //pnlAlta.Controls.Add(pnlApellidosCliente);

            var pnlListaClientes = this.BuildPanelListaClientes();
            pnlAlta.Controls.Add(pnlListaClientes);

            var pnlFechaEntrada = this.BuildPnlFechaEntrada();
            pnlAlta.Controls.Add(pnlFechaEntrada);

            var pnlFechaSalida = this.BuildPnlFechaSalida();
            pnlAlta.Controls.Add(pnlFechaSalida);

            var pnlUsaGaraje = this.BuildPnlGaraje();
            pnlAlta.Controls.Add(pnlUsaGaraje);

            var pnlTarifa = this.BuildPnlTarifa();
            pnlAlta.Controls.Add(pnlTarifa);

            var pnlLista = this.BuildPanelLista();
            pnlAlta.Controls.Add(pnlLista);

            var pnlTotal = this.BuildPnlTotal();
            pnlAlta.Controls.Add(pnlTotal);


            var pnlBotones = this.BuildPnlBotones();
			pnlAlta.Controls.Add(pnlBotones);

			pnlAlta.ResumeLayout(true);

            this.Text = "Alta Reserva";
            this.Size = new Size(400,
			                     //pnlNombreCliente.Height + pnlApellidosCliente.Height + 
                                 pnlFechaEntrada.Height + pnlFechaSalida.Height +
                                 pnlListaClientes.Height +
                                 pnlLista.Height + pnlUsaGaraje.Height + pnlTarifa.Height +
                                 pnlBotones.Height + pnlTotal.Height + 30);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);
        }

       



        public string Nombre => this.edNombre.Text;
        public string Apellidos => this.edApellidos.Text;
        public DateTime FechaEntrada => this.fechaEntrada.Value;
        public DateTime FechaSalida => this.fechaSalida.Value;
        public Button Bt => this.btComprobarHabitaciones;
        public bool UsaGaraje => this.edGaraje.Checked;
        public double Tarifa => (double)this.edTarifa.Value;
        public Habitacion habitacion;
        public Cliente cliente;

        private TextBox edNombre;
        private TextBox edApellidos;
        private TextBox edTotal;
        private DateTimePicker fechaEntrada;
        private DateTimePicker fechaSalida;
        private CheckBox edGaraje;
        private NumericUpDown edTarifa;
        

        public Button btComprobarHabitaciones;

        private Panel pnlPpal;
        public DataGridView grdLista;
        public DataGridView grdListaClientes;

        private RegistroReservas reservas;
        private RegistroHabitaciones habitaciones;
        private RegistroClientes clientes;
        private Reserva reservaModificar;


    }
}
