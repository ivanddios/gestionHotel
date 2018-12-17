
namespace ReservasHotel.View
{
	using System;
    using System.Windows.Forms;
    using System.Drawing;

    public partial class MainWindow : Form
    {
        private MainMenu mPpal;
        private MenuItem mArchivo;

        private Panel pnlPpal;
        private Panel pnl;
        private Panel pnlHabitaciones;
        private Panel pnlClientes;
        private Panel pnlBusquedasPpal;
        private Panel pnlBusquedasDisponibilidad;
        public DataGridView grdLista;
        public TextBox edDetalle;


        private void Build()
        {
            this.BuildMenu();

            this.SuspendLayout();
            this.pnlPpal = new Panel()
            {
                Dock = DockStyle.Fill
            };

            this.pnlPpal.SuspendLayout();
            this.Controls.Add(this.pnlPpal);
            this.pnlPpal.Controls.Add(this.BuildPanelLista());
            this.pnlPpal.Controls.Add(this.BuildPanelDetalle());
            this.pnlPpal.Controls.Add(this.BuildPnlBotones());
            this.pnlPpal.ResumeLayout(false);

            this.MinimumSize = new Size(900, 600);
            this.Resize += (obj, e) => this.ResizeWindow();
            this.Text = "Hotel";

            this.ResumeLayout(true);
            this.ResizeWindow();

            this.Show();
        }

        private Panel BuildPanel()
        {
            //this.BuildMenu();

            //this.SuspendLayout();
            this.pnl = new Panel()
            {
                Dock = DockStyle.Fill
            };

            //this.pnlPpal.SuspendLayout();
            //this.Controls.Add(this.pnlPpal);
            this.pnl.Controls.Add(this.BuildPanelLista());
            this.pnl.Controls.Add(this.BuildPanelDetalle());
            this.pnl.Controls.Add(this.BuildPnlBotones());
            //this.pnlPpal.ResumeLayout(false);
            this.pnl.Resize += (obj, e) => this.ResizeWindow();
            //this.MinimumSize = new Size(900, 600);
            //this.Resize += (obj, e) => this.ResizeWindow();
            //this.Text = "Hotel";

            this.ResumeLayout(true);
            this.ResizeWindow();

            return pnl;
        }

        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            //Elementos del menu
            this.mArchivo = new MenuItem("&Archivo");
            this.mClientes = new MenuItem("&Clientes");
			this.mHabitaciones = new MenuItem("&Habitaciones");
			this.mReservas = new MenuItem("&Reservas");
            this.mBusquedas = new MenuItem("&Busquedas");
            //Operaciones del los elementos del menu
            this.OpSalir = new MenuItem("&Salir") { Shortcut = Shortcut.CtrlQ };
            this.OpSalir.Click += (sender, e) => this.Salir();

            //Operaciones menu CLIENTES
            this.OpConsultarClientes = new MenuItem("&Consultar");
            this.OpInsertarCliente = new MenuItem("&Insertar");

            //Operaciones menu HABITACIONES
            this.OpConsultarHabitaciones = new MenuItem("&Consultar");
            this.OpInsertarHabitacion = new MenuItem("&Insertar");

            //Operaciones de menu RESERVAS
            this.OpConsultarReservas = new MenuItem("&Consultar");
            this.OpAltaReserva = new MenuItem("&Nueva");
            this.OpGraficos= new MenuItem("&Gráficos");

            this.OpReservasPendientes = new MenuItem("&Reservas Pendientes");
            this.OpDisponibilidad = new MenuItem("&Disponibilidad");
            this.OpOcupacion = new MenuItem("&Ocupacion");

            //Agregacion de las operaciones a los elementos
            this.mArchivo.MenuItems.Add(this.OpSalir);

            //Agregacion operaciones de CLIENTES
            this.mClientes.MenuItems.Add(this.OpConsultarClientes);
            this.mClientes.MenuItems.Add(this.OpInsertarCliente);

            //Agregacion operaciones de HABITACIONES
            this.mHabitaciones.MenuItems.Add(this.OpConsultarHabitaciones);
            this.mHabitaciones.MenuItems.Add(this.OpInsertarHabitacion);
            
            //Agregacion operaciones de RESERVAS
            this.mReservas.MenuItems.Add(this.OpConsultarReservas);
            this.mReservas.MenuItems.Add(this.OpAltaReserva);
            this.mReservas.MenuItems.Add(this.OpGraficos);
            
            //Agregacion operaciones de BUSQUEDAS
            this.mBusquedas.MenuItems.Add(this.OpReservasPendientes);
            this.mBusquedas.MenuItems.Add(this.OpDisponibilidad);
            this.mBusquedas.MenuItems.Add(this.OpOcupacion);

            //Agregacion de los elementos al menu
            this.mPpal.MenuItems.Add(this.mArchivo);
			this.mPpal.MenuItems.Add(this.mClientes);
			this.mPpal.MenuItems.Add(this.mHabitaciones);
			this.mPpal.MenuItems.Add(this.mReservas);
            this.mPpal.MenuItems.Add(this.mBusquedas);
            this.Menu = mPpal;
            this.Show();
        }

        private Panel BuildPanelDetalle()
        {
            var pnlDetalle = new Panel { Dock = DockStyle.Bottom };

            pnlDetalle.SuspendLayout();

            this.edDetalle = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Font = new Font(FontFamily.GenericMonospace, 9),
                ForeColor = Color.Navy,
                BackColor = Color.LightGray
            };

            pnlDetalle.Controls.Add(this.edDetalle);
            pnlDetalle.ResumeLayout(false);
            return pnlDetalle;
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
            var textCellTemplate5 = new DataGridViewTextBoxCell();
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
            textCellTemplate5.Style.BackColor = Color.Lavender;
            textCellTemplate5.Style.ForeColor = Color.Black;

            var column0 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "#",
                Width = 50,
                ReadOnly = true
            };

            var column1 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Reserva",
                Width = 70,
                ReadOnly = true
            };

            var column2 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Fecha entrada",
                Width = 50,
                ReadOnly = true
            };

            var column3 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Fecha salida",
                Width = 50,
                ReadOnly = true
            };

            var column4 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Precio total",
                Width = 50,
                ReadOnly = true
            };

            var column5 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate5,
                HeaderText = "Nombre cliente",
                Width = 50,
                ReadOnly = true
            };

            this.grdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3, column4, column5
            });

            this.grdLista.SelectionChanged += (sender, e) => this.FilaSeleccionada();

            pnlLista.Controls.Add(this.grdLista);
            pnlLista.ResumeLayout(false);

            return pnlLista;
        }


        private void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;

            // Redimensionar la tabla
            this.grdLista.Width = width;

            this.grdLista.Columns[0].Width =
                                (int)System.Math.Floor(width * .05);
            this.grdLista.Columns[1].Width =
                                (int)System.Math.Floor(width * .20);
            this.grdLista.Columns[2].Width =
                                (int)System.Math.Floor(width * .20);
            this.grdLista.Columns[3].Width =
                                (int)System.Math.Floor(width * .20);
            this.grdLista.Columns[4].Width =
                                (int)System.Math.Floor(width * .15);
            this.grdLista.Columns[5].Width =
                                (int)System.Math.Floor(width * .20);
        }

        private Panel BuildPnlBotones()
        {
            var toret = new TableLayoutPanel()
            {
                ColumnCount = 3,
                RowCount = 2
            };

            this.btModificar = new Button()
            {
                Text = "&Modificar"
            };

            this.btEliminar = new Button()
            {
                Text = "&Eliminar"
            };

            this.btFactura = new Button()
            {
                Text = "&Factura"
            };


            this.btModificar.Click += (sender, e) => this.modificarReserva();
            this.btEliminar.Click += (sender, e) => this.eliminarReserva();
            this.btFactura.Click += (sender, e) => this.generarFactura();

            toret.Controls.Add(btModificar);
            toret.Controls.Add(btEliminar);
            toret.Controls.Add(btFactura);
            toret.Dock = DockStyle.Bottom;


            return toret;
        }

        private Button btEliminar;
        private Button btModificar;
        private Button btFactura;


        public MainMenu Mpal { get; set; }
        public MenuItem MArchivo { get; set; }
        public MenuItem OpSalir { get; set; }
		public MenuItem mClientes { get; set; }
		public MenuItem mHabitaciones { get; set; }
		public MenuItem mReservas { get; set; }

		public MenuItem OpConsultarReservas { get; set; }
        public MenuItem OpAltaReserva { get; set; }
        public MenuItem OpGraficos { get; set; }

        public MenuItem OpConsultarHabitaciones { get; set; }
        public MenuItem OpInsertarHabitacion { get; set; }

        public MenuItem OpConsultarClientes { get; set; }
        public MenuItem OpInsertarCliente { get; set; }

        public MenuItem mBusquedas { get; set; }
        public MenuItem OpReservasPendientes { get; set; }
        public MenuItem OpDisponibilidad { get; set; }
        public MenuItem OpOcupacion { get; set; }

    }
}
