namespace BusquedasHotel.View {
    using System.Windows.Forms;
    using System.Drawing;
    
    public partial class MainWindow {
              
        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            this.mArchivo = new MenuItem( "&Archivo" );
            this.mBusquedas = new MenuItem( "&Busquedas" );

            this.opSalir = new MenuItem("&Salir") { Shortcut = Shortcut.CtrlQ };
            this.opSalir.Click += (sender, e) => this.Salir();

			this.opCincoDiasTodoHotel = new MenuItem("&Reservas Pendientes Todo Hotel (5 días)");
			this.opCincoDiasTodoHotel.Click += (sender, e) => this.PendientesHotel5Dias();
			this.opCincoDias = new MenuItem("&Reservas Pendientes (5 días)");
			//this.opCincoDias.Click += (sender, e) => this.FiltrarPorHabitacionCincoDias();
			//this.opDisponibilidad = new MenuItem("&Disponibilidad");
			//this.opDisponibilidad.Click += (sender, e) => this.Disponibilidad();
			//this.opRPorPersona = new MenuItem("&Reservas Por Persona");
			//this.opRPorPersona.Click += (sender, e) => this.FiltrarPorPersona();
			//this.opRPorHabitacion = new MenuItem("&Reservas Por Habitacion");
			//this.opRPorHabitacion.Click += (sender, e) => this.FiltrarPorHabitacion();
			//this.opOcupacion = new MenuItem("&Ocupacion");
			//this.opOcupacion.Click += (sender, e) => this.Ocupacion();
			//this.opVistaGlobal = new MenuItem("&Ver Todas las Reservas");
			//this.opVistaGlobal.Click += (sender, e) => this.VistaGlobal();
   //         this.opReservasOrdenadasPorHabitacion = new MenuItem("&Reservas Ordenadas Por Habitacion");
			//this.opReservasOrdenadasPorHabitacion.Click += (sender, e) => this.OrdenarPorHabitacion();
   //         this.opReservasOrdenadasPorCliente = new MenuItem("&Reservas Ordenadas Por Cliente");
   //         this.opReservasOrdenadasPorCliente.Click += (sender, e) => this.OrdenarPorCliente();
            this.mArchivo.MenuItems.Add( this.opSalir );
			this.mBusquedas.MenuItems.Add(this.opCincoDiasTodoHotel);
            this.mBusquedas.MenuItems.Add(this.opCincoDias);
			this.mBusquedas.MenuItems.Add(this.opDisponibilidad);
			this.mBusquedas.MenuItems.Add(this.opRPorPersona);
			this.mBusquedas.MenuItems.Add(this.opRPorHabitacion);
			this.mBusquedas.MenuItems.Add(this.opReservasOrdenadasPorHabitacion);
            this.mBusquedas.MenuItems.Add(this.opReservasOrdenadasPorCliente);
            this.mBusquedas.MenuItems.Add(this.opOcupacion);


            this.mPpal.MenuItems.Add( this.mArchivo );
            this.mPpal.MenuItems.Add( this.mBusquedas );
			this.mPpal.MenuItems.Add(this.opVistaGlobal);
            this.Menu = mPpal;
        }
        
  
		public Panel BuildPanelListaDisponibilidad()
        {
			//this.BuildPanelLista().Hide();
            var pnlLista = new Panel();
            pnlLista.SuspendLayout();
            pnlLista.Dock = DockStyle.Fill;

            // Crear gridview
            this.grdListaDisponibilidad = new DataGridView()
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

			this.grdListaDisponibilidad.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
			this.grdListaDisponibilidad.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            var textCellTemplate0 = new DataGridViewTextBoxCell();
			var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            textCellTemplate0.Style.BackColor = Color.LightGray;
			textCellTemplate0.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.BackColor = Color.Wheat;
            textCellTemplate1.Style.ForeColor = Color.Black;
			textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            textCellTemplate2.Style.BackColor = Color.Wheat;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate2.Style.Alignment = DataGridViewContentAlignment.MiddleRight;

            var column0 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "Nº Habitacion",
                Width = 120,
                ReadOnly = true
            };
			var column1 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Número de Habitacion",
                Width = 150,
                ReadOnly = true
            };

            var column2 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Número de Piso",
                Width = 120,
                ReadOnly = true
            };
			this.grdListaDisponibilidad.Columns.AddRange(new DataGridViewColumn[] {
				column0,column1,column2
            });


			this.grdListaDisponibilidad.SelectionChanged +=
				    (sender, e) => this.FilaSeleccionadaDisponibilidad();
			pnlLista.Controls.Add(this.grdListaDisponibilidad);
            pnlLista.ResumeLayout(false);
            return pnlLista;
        }


        private Panel BuildPanelDetalle()
        {
			var pnlDetalle = new Panel { Dock = DockStyle.Bottom };
            pnlDetalle.SuspendLayout();
			this.BuildStatus();
            this.edDetalle = new TextBox {
				Dock = DockStyle.Fill,
                Multiline = true,
                ReadOnly = true,
                Font = new Font( FontFamily.GenericMonospace, 9 ),
                ForeColor = Color.Navy,
                BackColor = Color.Olive
            };
            pnlDetalle.Controls.Add( this.edDetalle );

			this.btFiltroDni = new Button{
				Dock=DockStyle.Right
			};
			this.edDetalle.Controls.Add(this.btFiltroDni);
            pnlDetalle.ResumeLayout( false );

            //this.Shown += (sender, e) => this.Actualiza();
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
            var textCellTemplate6 = new DataGridViewTextBoxCell();
            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.BackColor = Color.Wheat;
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate2.Style.BackColor = Color.Wheat;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.BackColor = Color.Wheat;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.BackColor = Color.Wheat;
			textCellTemplate4.Style.ForeColor = Color.Black;
            textCellTemplate5.Style.BackColor = Color.Wheat;
			textCellTemplate5.Style.ForeColor = Color.Black;
            textCellTemplate6.Style.BackColor = Color.Wheat;
            textCellTemplate6.Style.ForeColor = Color.Black;
            
            var column0 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "Nº Reserva",
                Width = 80,
                ReadOnly = true
            };
            
            var column1 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "DNI Cliente",
                Width = 100,
                ReadOnly = true
            };
            
            var column2 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Nº Habitación",
                Width = 60,
                ReadOnly = true
            };
			var column6 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate6,
                HeaderText = "Nº Piso",
                Width = 60,
                ReadOnly = true
            };

            var column3 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Fecha Entrada",
                Width = 120,
                ReadOnly = true
            };
            
            var column4 = new DataGridViewTextBoxColumn {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Fecha Salida",
                Width = 120,
                ReadOnly = true
            };

			var column5 = new DataGridViewTextBoxColumn
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate5,
                HeaderText = "Id Reserva",
                Width = 120,
                ReadOnly = true
            };
            
            this.grdLista.Columns.AddRange( new DataGridViewColumn[] {
				column0, column1, column2, column6, column3, column4, column5
            } );


           // this.grdLista.SelectionChanged +=
                                    //    (sender, e) => this.FilaSeleccionada();
            pnlLista.Controls.Add( this.grdLista );
            pnlLista.ResumeLayout( false );
            return pnlLista;
        }

        private void BuildStatus()
        {
			this.sbStatus = new StatusBar { Dock = DockStyle.Bottom };
			this.Controls.Add(this.sbStatus);
        }      

        public Panel BuildPpal()
        {

            this.BuildPanelLista();
            this.SuspendLayout();
            this.pnlPpal = new Panel()
            {
                Dock = DockStyle.Fill
            };

            this.pnlPpal.SuspendLayout();
            this.pnlPpal.Controls.Add(this.BuildPanelLista());
			this.pnlPpal.Controls.Add( this.BuildPanelDetalle() );
            this.pnlPpal.ResumeLayout( false );

            this.Resize += (obj, e) => this.ResizeWindow();
         
            
            this.ResumeLayout( true );
            //this.ResizeWindow();
            this.Shown += (sender, e) => this.Actualiza();

            return this.pnlPpal;
        }


        public Panel BuildDisponibilidad()
        {

            this.BuildPanelLista();
            this.SuspendLayout();

            this.pnlDisponibilidad = new Panel()
            {
                Dock = DockStyle.Fill,
                Visible = false
            };

            this.pnlDisponibilidad.SuspendLayout();
            this.pnlDisponibilidad.Controls.Add(this.BuildPanelListaDisponibilidad());
            this.pnlDisponibilidad.ResumeLayout(false);

            //this.MinimumSize = new Size(1000, 500);
            this.Resize += (obj, e) => this.ResizeWindow();
      

            this.ResumeLayout(true);
           // this.ResizeWindow();
            //this.Closed += (sender, e) => this.Salir();
            this.Shown += (sender, e) => this.Actualiza();

            return this.pnlDisponibilidad;
        }


            public void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;
            
            // Redimensionar la tabla
            this.grdLista.Width = width;
            
            this.grdLista.Columns[ ColNum ].Width =
                                (int) System.Math.Floor( width *.15 ); 
			this.grdLista.Columns[ ColCliente ].Width =
                                (int) System.Math.Floor( width *.15 ); 
            this.grdLista.Columns[ ColNumHabitacion ].Width =
                                (int) System.Math.Floor( width *.15 );
			this.grdLista.Columns[ ColPisoHabitacion ].Width =
                                (int) System.Math.Floor( width *.10 ); 
            this.grdLista.Columns[ ColNumHabitacion ].Width =
                                (int) System.Math.Floor( width *.15 );  
            this.grdLista.Columns[ ColNumHabitacion ].Width =
                                (int) System.Math.Floor( width *.15 ); 
            this.grdLista.Columns[ ColFechaEntrada ].Width =
                                (int) System.Math.Floor( width *.15 ); 
            this.grdLista.Columns[ ColFechaSalida ].Width =
                                (int) System.Math.Floor( width *.15 );  
			this.grdLista.Columns[ColIdReserva].Width =
                                (int)System.Math.Floor(width * .15);   
        }
        

        public string Habitacion => this.edHabitacion.Text;

        private MainMenu mPpal;
        private MenuItem mArchivo;
        private MenuItem mBusquedas;
        private MenuItem opSalir;
		private MenuItem opCincoDias;
        private MenuItem opCincoDiasTodoHotel;
		private MenuItem opDisponibilidad;
		private MenuItem opRPorPersona;
		private MenuItem opRPorHabitacion;
        private MenuItem opOcupacion;
		private MenuItem opVistaGlobal;
		private MenuItem opReservasOrdenadasPorHabitacion;
        private MenuItem opReservasOrdenadasPorCliente;
        private StatusBar sbStatus;
		private Panel pnlPpal;
        private Panel pnlDisponibilidad;
        private TextBox edDetalle;
        private DataGridView grdLista;
		private DataGridView grdListaDisponibilidad;
		private Button btFiltroDni;
		private Button btFiltroHabitacion;
        private TextBox edHabitacion;
    }
}
