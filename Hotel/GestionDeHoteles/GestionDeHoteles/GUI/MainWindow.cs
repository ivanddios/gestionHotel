namespace GestionDeHoteles.GUI
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using GestionDeHoteles.XML;
	using Gtk;

	public partial class MainWindow : Gtk.Window
	{
		public MainWindow(XMLBrowser browser, int width, int height) : base(Gtk.WindowType.Toplevel)
		{
			this.Browser = browser;         
			this.DefaultWidth = width;
			this.DefaultHeight = height;
			this.Build();
		}

		private void Build()
		{
			this.titulo = "";

			vBox = new Gtk.VBox(false, 3);
			vBox.Visible = true;

			HBOpcionesGraficos = new Gtk.HBox(false, 2);
			setVisible(HBOpcionesGraficos);
			HBGraph = new Gtk.HBox(false, 3);
			setVisible(HBGraph);
			HVGraphControls = new Gtk.VBox(false, 3);
			HVGraphControls.Visible = true;
			HBMeses = new Gtk.HBox(false, 3);
			setVisible(HBMeses);
			HBAnhos = new Gtk.HBox(false, 3);
			setVisible(HBAnhos);

			this.WidthGraph = this.DefaultWidth;
			this.HeightGraph = this.DefaultHeight;

			Gtk.Action actViewDrawing = new Gtk.Action("ViewNotebook", "Drawing demo", "View drawing demo", null);
			actViewDrawing.Activated += (obj, evt) => this.OnViewDrawing();

			this.btAumentar = InitButton(this.btAumentar, strBtAumentar);
			this.btReducir = InitButton(this.btReducir, strBtReducir);
			this.btMeses = InitButton(this.btMeses, strBtMeses);
			this.btAnhos = InitButton(this.btAnhos, strBtAnhos);
			this.btAnhoSiguiente = InitButton(this.btAnhoSiguiente, strBtAnhoSiguiente);
			this.btAnhoAnterior = InitButton(this.btAnhoAnterior, strBtAnhoAnterior);
			this.btOcGen = InitButton(this.btOcGen, strBtOcGen);
			this.btOcPCl = InitButton(this.btOcPCl, strBtOcPCl);
			this.btOcPHb = InitButton(this.btOcPHb, strBtOcPHb);
			this.btComHb = InitButton(this.btComHb, strBtComHb);

			this.cbClientes = new Gtk.ComboBox { Visible = false };
			this.cbHabitaciones = new Gtk.ComboBox { Visible = false };
			this.cbComodidades = new Gtk.ComboBox { Visible = false };

			this.graph = new Graph((int)(this.DefaultWidth * 0.9), (int)(this.DefaultHeight * 0.75));

			this.DeleteEvent += (o, args) => Gtk.Application.Quit();

			btAumentar.Clicked += (object sender, EventArgs e) => this.Ajustar();
			btReducir.Clicked += (object sender, EventArgs e) => this.Reducir();
			btMeses.Clicked += (object sender, EventArgs e) => this.setPorMeses(true);
			btAnhos.Clicked += (object sender, EventArgs e) => this.setPorMeses(false);
			btAnhoSiguiente.Clicked += (object sender, EventArgs e) => setAnho(1);
			btAnhoAnterior.Clicked += (object sender, EventArgs e) => setAnho(-1);
			cbClientes.Changed += (object sender, EventArgs e) => changeCB();
			cbHabitaciones.Changed += (object sender, EventArgs e) => changeCB();
			cbComodidades.Changed += (object sender, EventArgs e) => changeCB();

			btOcGen.Clicked += (object sender, EventArgs e) => this.MostrarOcupacionGeneral();
			btOcPCl.Clicked += (object sender, EventArgs e) => this.MostrarOcupacionPorCliente("");
			btOcPHb.Clicked += (object sender, EventArgs e) => this.MostrarOcupacionPorHabitacion(0);
			btComHb.Clicked += (object sender, EventArgs e) => this.MostrarComodidadesHabitaciones("");

			HBOpcionesGraficos.PackStart(this.btOcGen);
			HBOpcionesGraficos.PackStart(this.btOcPCl);
			HBOpcionesGraficos.PackStart(this.cbClientes);
			HBOpcionesGraficos.PackStart(this.btOcPHb);
			HBOpcionesGraficos.PackStart(this.cbHabitaciones);
			HBOpcionesGraficos.PackStart(this.btComHb);
			HBOpcionesGraficos.PackStart(this.cbComodidades);

            HBMeses.PackStart(this.btAnhoAnterior);
			HBMeses.PackStart(this.btMeses);
            HBMeses.PackStart(this.btAnhoSiguiente);

			HBAnhos.PackStart(this.btAnhos);

			HVGraphControls.PackStart(this.HBMeses);
			HVGraphControls.PackStart(this.HBAnhos);
			HVGraphControls.PackStart(this.btAumentar);
			HVGraphControls.PackStart(this.btReducir);

			HBGraph.PackStart(graph);
			HBGraph.PackEnd(HVGraphControls);

			vBox.PackStart(HBOpcionesGraficos);
			vBox.PackStart(HBGraph);

			this.Add(vBox);
			this.setPorMeses(false);
			this.OnViewDrawing();
		}

		/// <summary>
		/// Evento para dibujar el gráfico
		/// </summary>
		private void OnViewDrawing()
		{
			HBGraph.Remove(graph);
			int width = 0, height = 0;
			base.GetSize(out width, out height);
			Console.WriteLine(width + " x " + height);
			graph = new Graph(WidthGraph, HeightGraph, this.Lista, this.porMeses);
			graph.Titulo = this.titulo;
			graph.Visible = true;
			if (firstTime){
				this.Anho = this.Lista.Count - 1;
				firstTime = false;
			}
			HBGraph.PackStart(graph);
		}
      
		protected override bool OnConfigureEvent(Gdk.EventConfigure args){
			base.OnConfigureEvent(args);
			this.Ajustar();

			return true;
		}

		/// <summary>
		/// Ajusta el gráfico al tamaño de la ventana
		/// </summary>
		private void Ajustar()
		{
			int width = 0, height = 0;
			base.GetSize(out width, out height);
			this.WidthGraph = (int)(width * porcentajeAumento);
			this.HeightGraph = (int)(height * porcentajeAumento);
			this.OnViewDrawing();
		}

		/// <summary>
		/// Reduce el tamaño del gráfico un tamaño fijo
		/// </summary>
		private void Reducir()
		{
			//Info importante: No se reduce el tamño de ventana, así que el gráfico se va a quedar rodeado
			this.WidthGraph -= tamanhoFijo;
			this.HeightGraph -= tamanhoFijo;
			this.OnViewDrawing();
		}

        /// <summary>
        /// Cambia la lista al anho seleccionado según el gráfico de ocupacion que se quiera mostrar
        /// </summary>
        /// <param name="valor">+1/-1 si pulsa un boton</param>
		private void setAnho(int valor){
			this.Anho = this.Anho + valor;
            int anhoX = Convert.ToInt32(getAnhoString(this.Anho));
			switch(this.datosMostrados){
				case OCUPACION_GENERAL:
					this.Lista = browser.getNumReservasPorMeses(anhoX);
					break;
				case OCUPACION_POR_CLIENTE:
					this.Lista = browser.getReservasClientePorMeses(this.cbClientes.ActiveText, anhoX);
					break;
				case OCUPACION_POR_HABITACION:
					this.Lista = browser.getReservasHabitacionPorMeses(Convert.ToInt32(this.cbHabitaciones.ActiveText), anhoX);
					break;
				case OCUPACION_POR_COMODIDAD:
					this.Lista = browser.getNumComodidadesPorMeses(this.cbComodidades.ActiveText, anhoX);
                    break;
			}
			this.OnViewDrawing();
		}

        /// <summary>
        /// Cambia la lista al grafico de ocupacion que se quiera mostrar
        /// </summary>    
		private void setGeneral(){
			this.porMeses = false;
            switch(this.datosMostrados){
                case OCUPACION_GENERAL:
					this.Lista = browser.getNumReservas();
                    break;
                case OCUPACION_POR_CLIENTE:
					this.Lista = browser.getReservasClienteAnho(this.cbClientes.ActiveText);
                    break;
                case OCUPACION_POR_HABITACION:
					this.Lista = browser.getReservasHabitacionAnho(Convert.ToInt32(this.cbHabitaciones.ActiveText));
                    break;
                case OCUPACION_POR_COMODIDAD:
					this.Lista = browser.getNumComodidadesAnho(this.cbComodidades.ActiveText);
                    break;
			}
            this.OnViewDrawing();
		}

		/// <summary>
		/// Cambia el grafico a por meses o por años
		/// </summary>
		/// <param name="x">If set to <c>true</c> x.</param>
		private void setPorMeses(bool x){
			this.porMeses = x;
			if (porMeses){
				btAnhos.Visible = true;
                btAnhoSiguiente.Visible = true;
                btAnhoAnterior.Visible = true;
				btMeses.Visible = false;
				this.setAnho(0);
			}else{
				btAnhos.Visible = false;
				btAnhoSiguiente.Visible = true;
				btAnhoAnterior.Visible = true;
                btAnhoSiguiente.Visible = false;
                btAnhoAnterior.Visible = false;
				btMeses.Visible = true;
                this.setGeneral();
			}
		}

        /// <summary>
        /// Manejador de evento de combobox, llama a poner el grafico segun por meses o años
        /// </summary>
        private void changeCB(){
            if(porMeses){
                this.setAnho(0);
            }else{
                this.setGeneral();
            }
        }

        /// <summary>
        /// Pone el gráfico al numero de reservas general
        /// </summary>
		public void setGraficoGeneral(){
			this.MostrarOcupacionGeneral();
		}

        /// <summary>
        /// Oculta el boton de ocupacion general
        /// </summary>
		private void MostrarOcupacionGeneral(){
			this.titulo = strBtOcGen;
            this.btOcGen.Visible = false;
			this.btOcPCl.Visible = true;
			this.btOcPHb.Visible = true;
            this.btComHb.Visible = true;
			this.cbClientes.Visible = false;
            this.cbHabitaciones.Visible = false;
            this.cbComodidades.Visible = false;

			this.datosMostrados = OCUPACION_GENERAL;
			this.setGeneral();
		}

        /// <summary>
        /// Pone el gráfico al numero de reservas del ciente identificado por idCliente. Dejar vacio o a null si no se quiere ningun cliente concreto
        /// </summary>
		/// <param name="idCliente">ID (DNI) del cliente.</param>
		public void setGraficoCliente(string idCliente)
        {
            this.MostrarOcupacionPorCliente(idCliente);
		}

        /// <summary>
        /// Oculta el boton de ocupacion por cliente y muestra el combobox con los posibles clientes, además de poner el grafico de ocupacion a mostrar por cliente
        /// </summary>
		private void MostrarOcupacionPorCliente(string idCliente){
            this.titulo = strBtOcPCl;
            this.btComHb.Visible = true;
            this.btOcGen.Visible = true;
			this.btOcPCl.Visible = false;
			this.btOcPHb.Visible = true;
			this.cbClientes.Visible = true;
			this.cbHabitaciones.Visible = false;
			this.cbComodidades.Visible = false;

			this.datosMostrados = OCUPACION_POR_CLIENTE;
			List<string> clientes = browser.getClientes();
			this.RepopulateComboBox(cbClientes, clientes);
			if(!(idCliente == null || idCliente.Equals(""))){
                Console.WriteLine(idCliente);
                clientes.ForEach((string x) => Console.Write("[" + x + "]"));
                if (clientes.Contains(idCliente)){
                    Console.WriteLine("FUNCIONA");
                    cbClientes.Active = clientes.IndexOf(idCliente);
				}
			}

            if(this.porMeses){
                setAnho(0);
            }else{
                setGeneral();
            }
		}

        /// <summary>
        /// Pone el gráfico al numero de reservas de la habitacion identificada por habitacion. Dejar a 0 si no se quiere ninguna habitación concreta
        /// </summary>
        /// <param name="habitacion">Id habitacion.</param>
		public void setGraficoHabitacion(int habitacion){
            Console.WriteLine(habitacion);
			this.MostrarOcupacionPorHabitacion(habitacion);
		}

        /// <summary>
        /// Oculta el boton de ocupacion por habitacion y muestra el combobox con los posibles tipos de habitacion,
        /// además de poner el grafico de ocupacion a mostrar por tipo de habitacion
        /// </summary>
		private void MostrarOcupacionPorHabitacion(int habitacion){
			this.titulo = strBtOcPHb;
			this.btComHb.Visible = true;
			this.btOcGen.Visible = true;
			this.btOcPCl.Visible = true;
			this.btOcPHb.Visible = false;
            this.cbClientes.Visible = false;
			this.cbHabitaciones.Visible = true;
			this.cbComodidades.Visible = false;

            this.datosMostrados = OCUPACION_POR_HABITACION;
			List<int> habitaciones = browser.getHabitaciones();
			this.RepopulateComboBox(cbHabitaciones, habitaciones);
			if(!(habitacion.Equals(0))){
				if(habitaciones.Contains(habitacion)){
					cbClientes.Active = habitaciones.IndexOf(habitacion);
                }
            }

            if(this.porMeses){
                setAnho(0);
            }else{
                setGeneral();
            }
		}

        /// <summary>
        /// Pone el grafico al numero de reservas con una habitacion con la comodidad indicada. Dejar comodidad a null o vacia para ninguna en concreto
        /// </summary>
        /// <param name="comodidad">Comodidad.</param>
		public void setGraficoComodidad(string comodidad){
			this.MostrarComodidadesHabitaciones(comodidad);
		}

        /// <summary>
        /// Pone el grafico al numero de reservas con la primera comodidad de la habitacion indicada. Dejar comodidad a null o vacia para ninguna en concreto.
		/// Si la habitacion no tiene comodidades, no se concretará ninguna
        /// </summary>
        /// <param name="habitacion">Id de habitacion.</param>
        public void setGraficoComodidad(int habitacion){
			try{
                this.MostrarComodidadesHabitaciones(browser.getComodidadesDe(habitacion)[0]);
			}catch(Exception){
                this.MostrarComodidadesHabitaciones("");
			}
        }

        /// <summary>
		/// Oculta el boton de ocupacion por comodidades y muestra el combobox con las posibles comodidades,
		/// además de poner el grafico de ocupacion a mostrar comodidades
        /// </summary>      
		private void MostrarComodidadesHabitaciones(string comodidad){
			this.titulo = strBtComHb;
			this.btComHb.Visible = false;
			this.btOcGen.Visible = true;
			this.btOcPCl.Visible = true;
			this.btOcPHb.Visible = true;
            this.cbClientes.Visible = false;
            this.cbHabitaciones.Visible = false;
			this.cbComodidades.Visible = true;

            this.datosMostrados = OCUPACION_POR_COMODIDAD;
			List<string> comodidades = browser.getComodidades();
			this.RepopulateComboBox(cbComodidades, comodidades);
			comodidades.ForEach((string obj) => Console.Write("[" + obj + "], "));
			if(!(comodidad == null || comodidad.Equals(""))){
				if(comodidades.Contains(comodidad)){
					cbClientes.Active = comodidades.IndexOf(comodidad);
                }
            }

			if(this.porMeses){
				setAnho(0);
			}else{
				setGeneral();
			}
		}

        /// <summary>
        /// Regenera un dropdown segun una lista pasada
        /// </summary>
        /// <param name="cb">ComboBox.</param>
		/// <param name="valores">Lista.</param>
        private void RepopulateComboBox(Gtk.ComboBox cb, List<string> valores){         
			cb.Clear();
            CellRendererText cell = new CellRendererText();
			cb.PackStart(cell, false);
			cb.AddAttribute(cell, "text", 0);
            ListStore store = new ListStore(typeof(string));
			cb.Model = store;

			foreach (string str in valores) {
				cb.AppendText(str);
            }
        }

        /// <summary>
        /// Regenera el dropdown segun una lista de ints
        /// </summary>
        /// <param name="comboBox">Combo box.</param>
        /// <param name="valores">Valores.</param>
		private void RepopulateComboBox(Gtk.ComboBox comboBox, List<int> valores){
			List<string> list = new List<string>();

			valores.ForEach((int obj) => list.Add(Convert.ToString(obj)));

			RepopulateComboBox(comboBox, list);
		}

		private Gtk.VBox vBox;
		private Gtk.VBox HVGraphControls;

		private Gtk.HBox HBOpcionesGraficos;
		private Gtk.HBox HBGraph;
		private Gtk.HBox HBMeses;
		private Gtk.HBox HBAnhos;

		private Gtk.Button btAumentar;
		private Gtk.Button btReducir;
		private Gtk.Button btMeses;
		private Gtk.Button btAnhos;
		private Gtk.Button btAnhoSiguiente;
		private Gtk.Button btAnhoAnterior;
		private Gtk.Button btOcGen;
		private Gtk.Button btOcPCl;
		private Gtk.Button btOcPHb;
		private Gtk.Button btComHb;

		private Gtk.ComboBox cbClientes;
		private Gtk.ComboBox cbHabitaciones;
		private Gtk.ComboBox cbComodidades;

		private String strBtAumentar = "Ajustar tamaño del gráfico";
		private String strBtReducir = "Reducir tamaño del gráfico";
		private String strBtMeses = "Gráfico por meses";
		private String strBtAnhos = "Gráfico por años";
		private String strBtAnhoAnterior = "<<";
		private String strBtAnhoSiguiente = ">>";
		private String strBtOcGen = "Ocupación general";
		private String strBtOcPCl = "Ocupación por cliente";
		private String strBtOcPHb = "Ocupación por habitación";
		private String strBtComHb = "Ocupación con comodidad";

		private Graph graph;
        
		private double porcentajeAumento = 0.75;
		private int tamanhoFijo = 52;

		private List<int> Lista
		{
			get{
				if(this.lista == null){
					return new List<int>();
				}
				return this.lista;
			}
			set{
				//Quizá tocar para ver si está porMeses
				this.lista = value;
			}
		}
		private List<int> lista;

		private int WidthGraph{
			get; set;
		}

		private int HeightGraph{
			get; set;
		}

		private int datosMostrados;

		private const int OCUPACION_GENERAL = 0;
		private const int OCUPACION_POR_CLIENTE = 1;
		private const int OCUPACION_POR_HABITACION = 2;
        private const int OCUPACION_POR_COMODIDAD = 3;

		private string titulo;

		private bool porMeses;

		private int Anho
		{
			get{
				return this.anho;
			}
			set{
				if (value <= 0){
					this.anho = numAnhos;
				}else if (value > numAnhos){
					this.anho = 1;
				}else{
					this.anho = value;
				}

                if (this.anho <= 1){
					this.strBtAnhoAnterior = "<< " + getAnhoString(numAnhos);
                }else{
					this.strBtAnhoAnterior = "<< " + getAnhoString(this.anho - 1);
                }

				if (this.anho >= numAnhos){
					this.strBtAnhoSiguiente = getAnhoString(1) + " >>";
                }else{
					this.strBtAnhoSiguiente = getAnhoString(this.anho + 1) + " >>";
				}

				this.btAnhoSiguiente.Label = this.strBtAnhoSiguiente;
                this.btAnhoAnterior.Label = this.strBtAnhoAnterior;
			}
		}
		private int anho;
		private int numAnhos;

		private string getAnhoString(int valor){
			try{
				return Convert.ToString((System.DateTime.Today.Year) - (numAnhos - valor));
			}catch (Exception){
				return "0";
			}
		}

		public XMLBrowser Browser{
			set{
                this.browser = value;
				this.numAnhos = browser.getNumAnhos();
			}
			get{
				return this.browser;
			}
		}
		private XMLBrowser browser;
		private bool firstTime = true;
	}

}
