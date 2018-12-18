namespace BusquedasHotel.View {
    using System;
	using System.Windows.Forms;
	using BusquedasHotel.Core;
	using System.Collections.Generic;
	using System.Linq;
    
    

	public partial class MainWindow: Form {
		public const int ColNum = 0;
		public const int ColCliente = 1;
		public const int ColNumHabitacion = 2;
		public const int ColFechaEntrada = 3;
        public const int ColFechaSalida = 4;
		public const int ColIdReserva = 5;
        public const int ColTipoHabitacion = 2;
        public const int ColComodidades = 3;
        public const int ColUltimaReserva = 4;


        public MainWindow()
		{

            //this.Build();
            this.BuildPpal();
            this.BuildDisponibilidad();
			//this.reservas=this.generarDatosAleatorios();
            this.reservas = RegistroReservas.RecuperarXml();
            this.regHabitaciones = RegistroHabitaciones.RecuperaXml();
            this.clientes = RegistroClientes.RecuperaXml();
            Console.WriteLine("Datos aleatorios");
            //this.PendientesHotel5Dias();
			this.habitacionesDisponibles=new List<Habitacion>();
			//this.reservasPorHabitacion = this.FiltrarPorHabitacion(300);
		}

        public Panel devolverPanelPpal()
        {
            return this.pnlPpal;
        }

        public Panel devolverPanelDisponibilidad()
        {
            return this.pnlDisponibilidad;
        }
        
		private void VistaGlobal(){
			this.pnlDisponibilidad.Hide();
			this.pnlPpal.Show();
			this.Actualiza();
		}

        public void Ocupacion()
        {
            this.pnlDisponibilidad.Hide();
            this.pnlPpal.Show();
            List<Reserva> aux = new List<Reserva>();
            var ocupacion = new Ocupacion();
            this.habitacionesDisponibles = new List<Habitacion>();
            this.habitacionesOcupadas = new List<Reserva>();

            if (ocupacion.ShowDialog() == DialogResult.OK && ocupacion.Anho.Equals(""))
            {
               /* for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        this.habitacionesDisponibles.Add(new Habitacion(j, i));
                    }
                }*/
                foreach (Reserva reserva in this.reservas)
                {
                    foreach (Habitacion habitacion in this.regHabitaciones)
                    {
                        if (reserva.Habitacion.Identificador.Equals(habitacion.Identificador)
                             && ocupacion.Fecha >= reserva.FechaEntrada &&
                      ocupacion.Fecha <= reserva.FechaSalida)
                        {
                            if (!aux.Contains(reserva))
                            {
                                aux.Add(reserva);
                            }
                        }

                    }
                }
                this.habitacionesOcupadas = aux;

                this.ActualizaOcupacion();
                return;
            }

            else if (ocupacion.ShowDialog() == DialogResult.OK && !ocupacion.Anho.Equals(""))
            {
               /* for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        this.habitacionesDisponibles.Add(new Habitacion(j, i));
                    }
                }*/
                foreach (Reserva reserva in this.reservas)
                {
                    foreach (Habitacion habitacion in this.regHabitaciones)
                    {
                        if (reserva.Habitacion.Identificador.Equals(habitacion.Identificador)
                        && reserva.FechaEntrada.Year.Equals(int.Parse(ocupacion.Anho)))
                        {
                            if (!aux.Contains(reserva))
                            {
                                aux.Add(reserva);
                            }
                        }

                    }
                }
                this.habitacionesOcupadas = aux;

                this.ActualizaOcupacion();
                return;
            }


        }

        public void Disponibilidad()
        {
            this.pnlPpal.Hide();
            this.pnlDisponibilidad.Show();
            this.ActualizaDisponibilidad();
            List<Habitacion> aux = new List<Habitacion>();
            var disponibilidad = new Disponibilidad();
            this.habitacionesDisponibles.Clear();
            if (disponibilidad.ShowDialog() == DialogResult.OK)
            {
            
                foreach (Reserva reserva in this.reservas)
                {
                    foreach (Habitacion habitacion in this.regHabitaciones)
                    {
                        if (!aux.Contains(habitacion))
                        {
                            aux.Add(habitacion);
                        }
                        if (reserva.Habitacion.Identificador.Equals(habitacion.Identificador))
                        {
                            System.Console.WriteLine(habitacion.ToString());
                            aux.Remove(habitacion);
                            Console.WriteLine(aux.Count);
                        }

                    }
                }
                this.habitacionesDisponibles.Clear();
                this.habitacionesDisponibles = aux;

                this.ActualizaDisponibilidad();
                return;
            }
        }



        public void PendientesHotel5Dias(){
			this.pnlDisponibilidad.Hide();
			this.pnlPpal.Show();
            this.reservasPorHabitacionCincoDias = new List<Reserva>();
            foreach (Reserva reserva in this.reservas)
            {
                if (reserva.FechaEntrada.CompareTo(DateTime.Now.AddDays(5)) <= 0 &&
                       reserva.FechaEntrada.CompareTo(DateTime.Now) >= 0)
                  {
                        this.reservasPorHabitacionCincoDias.Add(reserva);
                  }
            }
            var ordenadas = this.reservasPorHabitacionCincoDias.OrderBy((x) => x.Habitacion.Identificador);
            this.reservasPorHabitacionCincoDias = ordenadas.ToList();
            this.ActualizaConFiltroPorHabitacion5Dias();
        }



        //    	private void FiltrarPorHabitacionCincoDias(){

        //          this.pnlDisponibilidad.Hide();
        //          this.pnlPpal.Show();
        //	var filtro5Dias= new Filtro5Dias();

        //	this.reservasPorHabitacionCincoDias= new List<Reserva>();
        //	if (filtro5Dias.ShowDialog() == DialogResult.OK)
        //          {
        //		foreach (Reserva reserva in this.reservas)
        //              {
        //			if (reserva.Habitacion.Numero.Equals(int.Parse(filtro5Dias.Habitacion)) &&
        //			    reserva.Habitacion.Piso.Equals(int.Parse(filtro5Dias.Piso)))
        //                  {
        //				if (reserva.FechaEntrada.CompareTo(DateTime.Now.AddDays(5)) <= 0 &&
        //                     reserva.FechaEntrada.CompareTo(DateTime.Now) >= 0)
        //                      {
        //                          this.reservasPorHabitacionCincoDias.Add(reserva);
        //                      }
        //                  }

        //              }
        //              this.ActualizaConFiltroPorHabitacion5Dias();
        //              return;
        //          }         
        //      }

        public void FiltrarPorPersona()
        {
            this.pnlDisponibilidad.Hide();
            this.pnlPpal.Show();
            var filtroPorPersona = new FiltroPorPersona();

            this.reservasPorPersona = new List<Reserva>();
            if (filtroPorPersona.ShowDialog() == DialogResult.OK)
            {
                foreach (Reserva reserva in this.reservas)
                {
                    if (reserva.Cliente.Dni.Equals(filtroPorPersona.Persona))
                    {
                        this.reservasPorPersona.Add(reserva);
                        //System.Console.WriteLine(reserva.ToString());
                    }
                }
                this.ActualizaConFiltroPorPersona();
                return;
            }
        }

        public void FiltrarPorPersona(string DNI)
        {

            //this.btFiltroDni = sender as Button;
            //if(this.btFiltroDni!=null){
            this.pnlDisponibilidad.Hide();
            this.pnlPpal.Show();


            this.reservasPorPersona = new List<Reserva>();
            foreach (Reserva reserva in this.reservas)
            {
                if (reserva.Cliente.Dni.Equals(DNI))
                {
                    this.reservasPorPersona.Add(reserva);
                    //System.Console.WriteLine(reserva.ToString());
                }
            }
            this.ActualizaConFiltroPorPersona();
            return;
            //}

        }

        //private void OrdenarPorCliente()
        //      {

        //          this.pnlDisponibilidad.Hide();
        //          this.pnlPpal.Show();
        //	var ordenadas = this.reservas.OrderBy((x) => x.Cliente.Dni);
        //          this.reservas = ordenadas.ToList();
        //          this.Actualiza();
        //          return;
        //      }

        //private void OrdenarPorHabitacion(){

        //          this.pnlDisponibilidad.Hide();
        //          this.pnlPpal.Show();
        //	var ordenadas =this.reservas.OrderBy((x)=>x.Habitacion.Piso).ThenBy((y)=>y.Habitacion.Numero);
        //	this.reservas = ordenadas.ToList();
        //	this.Actualiza();
        //	return;
        //}

        public void FiltrarPorHabitacion()
        {

            this.pnlDisponibilidad.Hide();
            this.pnlPpal.Show();
            var filtroPorHabitacion = new FiltroPorHabitacion();

            this.reservasPorHabitacion = new List<Reserva>();
            if (filtroPorHabitacion.ShowDialog() == DialogResult.OK)
            {
                foreach (Reserva reserva in this.reservas)
                {
                    if (reserva.Habitacion.Identificador.Equals(int.Parse(filtroPorHabitacion.Habitacion)))
                    {
                        this.reservasPorHabitacion.Add(reserva);
                        //this.reservasPorHabitacion.OrderBy((Reserva arg) => arg.Habitacion.Numero);
                        //System.Console.WriteLine(reserva.ToString());
                    }
                }
                this.ActualizaConFiltroPorHabitacion();
                return;
            }
        }

        //this.Shown += (sender, e) => this.Actualiza();

        /*	private List<Reserva> FiltrarCincoDias()
            {
                TimeSpan timeSpan = DateTime.Now.AddDays(5) - DateTime.Now;
                List<Reserva> rPH = new List<Reserva>();
                foreach (Reserva reserva in this.reservas)
                {
                    TimeSpan timeSpan2 = reserva.FechaEntrada - DateTime.Now;
                    if (TimeSpan.Compare(timeSpan,timeSpan2)>=0)
                    {
                        rPH.Add(reserva);
                        //System.Console.WriteLine(reserva.ToString());
                    }
                }
                return rPH;
            }*/


        private void Salir()
		{
			//this.reservas.GuardaXml();
			this.Dispose( true );
            //this.reservas.Clear();
		}

		private void ActualizaConFiltroPorHabitacion5Dias()
        {
            DateTime ahora = DateTime.Now;

			this.sbStatus.Text = "Reservas: " + this.reservasPorHabitacionCincoDias.Count.ToString()
                            + " | " + ahora.ToShortDateString()
                            + " | " + ahora.ToShortTimeString();
			if (this.reservasPorHabitacionCincoDias.Count > 0)
                this.ActualizaListaConFiltroPorHabitacion5Dias(0);
        }

        private void ActualizaListaConFiltroPorHabitacion5Dias(int numRow)
        {
            //int numReservas = this.reservas.Count;
			int numReservas = this.reservasPorHabitacionCincoDias.Count;
            // Crea y actualiza filas
            for (int i = numRow; i < numReservas; ++i)
            {
                if (this.grdLista.Rows.Count <= i)
                {
                    this.grdLista.Rows.Add();
                }

                this.ActualizaFilaDeListaConFiltroDeHabitacion5Dias(i);
            }


            // Eliminar filas sobrantes
            int numExtra = this.grdLista.Rows.Count - numReservas;
            for (; numExtra > 0; --numExtra)
            {
                this.grdLista.Rows.RemoveAt(numReservas);
            }

            return;
        }

        private void ActualizaFilaDeListaConFiltroDeHabitacion5Dias(int rowIndex)
        {
            if (rowIndex < 0
              || rowIndex > this.grdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(rowIndex));
            }

            DataGridViewRow row = this.grdLista.Rows[rowIndex];
			Reserva reserva = this.reservasPorHabitacionCincoDias[rowIndex];

            // Assign data
            row.Cells[ColNum].Value = (rowIndex + 1).ToString().PadLeft(4, ' ');
            row.Cells[ColCliente].Value = reserva.Cliente.Dni;
            row.Cells[ColNumHabitacion].Value = reserva.Habitacion.Identificador.ToString();
            //row.Cells[ColPisoHabitacion].Value = reserva.Habitacion.Piso.ToString();
            row.Cells[ColFechaEntrada].Value = reserva.FechaEntrada.ToShortDateString();
            row.Cells[ColFechaSalida].Value = reserva.FechaSalida.ToShortDateString();
            row.Cells[ColIdReserva].Value = reserva.IdReserva.ToString();

            // Assign tooltip text
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.ToolTipText = reserva.ToString();
            }

            return;
        }


        private void FilaSeleccionadaConFiltroPorHabitacion5Dias()
        {
            int fila = System.Math.Max(0, this.grdLista.CurrentRow.Index);

			if (this.reservasPorHabitacionCincoDias.Count > fila)
            {
				this.edDetalle.Text = this.reservasPorHabitacionCincoDias[fila].ToString()
                    + ". ";//+this.reservas[fila].ToString();
                this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                this.edDetalle.SelectionLength = 0;
            }
            else
            {
                this.edDetalle.Clear();
            }

            return;
        }

		public void ActualizaConFiltroPorHabitacion()
        {
            DateTime ahora = DateTime.Now;

			this.sbStatus.Text = "Reservas: " + this.reservasPorHabitacion.Count.ToString()
                            + " | " + ahora.ToShortDateString()
                            + " | " + ahora.ToShortTimeString();
			if(this.reservasPorHabitacion.Count>0)
                this.ActualizaListaConFiltroPorHabitacion(0);
        }

		private void ActualizaListaConFiltroPorHabitacion(int numRow)
        {
            //int numReservas = this.reservas.Count;
			int numReservas = this.reservasPorHabitacion.Count;
            // Crea y actualiza filas
            for (int i = numRow; i < numReservas; ++i)
            {
                if (this.grdLista.Rows.Count <= i)
                {
                    this.grdLista.Rows.Add();
                }

				this.ActualizaFilaDeListaConFiltroDeHabitacion(i);
            }


            // Eliminar filas sobrantes
            int numExtra = this.grdLista.Rows.Count - numReservas;
            for (; numExtra > 0; --numExtra)
            {
                this.grdLista.Rows.RemoveAt(numReservas);
            }

            return;
        }

		private void ActualizaFilaDeListaConFiltroDeHabitacion(int rowIndex)
        {
            if (rowIndex < 0
              || rowIndex > this.grdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(rowIndex));
            }

            DataGridViewRow row = this.grdLista.Rows[rowIndex];
            Reserva reserva = this.reservasPorHabitacion[rowIndex];

            // Assign data
            row.Cells[ColNum].Value = (rowIndex + 1).ToString().PadLeft(4, ' ');
            row.Cells[ColCliente].Value = reserva.Cliente.Dni;
			row.Cells[ColNumHabitacion].Value = reserva.Habitacion.Identificador.ToString();
			//row.Cells[ColPisoHabitacion].Value = reserva.Habitacion.Piso.ToString();
            row.Cells[ColFechaEntrada].Value = reserva.FechaEntrada.ToShortDateString();
            row.Cells[ColFechaSalida].Value = reserva.FechaSalida.ToShortDateString();
            row.Cells[ColIdReserva].Value = reserva.IdReserva.ToString();

            // Assign tooltip text
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.ToolTipText = reserva.ToString();
            }

            return;
        }


		private void FilaSeleccionadaConFiltroPorHabitacion()
        {
            int fila = System.Math.Max(0, this.grdLista.CurrentRow.Index);

            if (this.reservasPorHabitacion.Count > fila)
            {
				this.edDetalle.Text = this.reservasPorHabitacion[fila].ToString()
                    + ". ";//+this.reservas[fila].ToString();
                this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                this.edDetalle.SelectionLength = 0;
            }
            else
            {
                this.edDetalle.Clear();
            }

            return;
        }

		public void ActualizaConFiltroPorPersona()
        {
            DateTime ahora = DateTime.Now;

            this.sbStatus.Text = "Reservas: " + this.reservasPorPersona.Count.ToString()
                            + " | " + ahora.ToShortDateString()
                            + " | " + ahora.ToShortTimeString();
            if (this.reservasPorPersona.Count > 0)
                this.ActualizaListaConFiltroPorPersona(0);
        }

        private void ActualizaListaConFiltroPorPersona(int numRow)
        {
            //int numReservas = this.reservas.Count;
            int numReservas = this.reservasPorPersona.Count;
            // Crea y actualiza filas
            for (int i = numRow; i < numReservas; ++i)
            {
                if (this.grdLista.Rows.Count <= i)
                {
                    this.grdLista.Rows.Add();
                }

                this.ActualizaFilaDeListaConFiltroDePersona(i);
            }


            // Eliminar filas sobrantes
            int numExtra = this.grdLista.Rows.Count - numReservas;
            for (; numExtra > 0; --numExtra)
            {
                this.grdLista.Rows.RemoveAt(numReservas);
            }

            return;
        }

        private void ActualizaFilaDeListaConFiltroDePersona(int rowIndex)
        {
            if (rowIndex < 0
              || rowIndex > this.grdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(rowIndex));
            }

            DataGridViewRow row = this.grdLista.Rows[rowIndex];
            Reserva reserva = this.reservasPorPersona[rowIndex];

            // Assign data
            row.Cells[ColNum].Value = (rowIndex + 1).ToString().PadLeft(4, ' ');
            row.Cells[ColCliente].Value = reserva.Cliente.Dni;
			row.Cells[ColNumHabitacion].Value = reserva.Habitacion.Identificador.ToString();
            //row.Cells[ColPisoHabitacion].Value = reserva.Habitacion.Piso.ToString();
            row.Cells[ColFechaEntrada].Value = reserva.FechaEntrada.ToShortDateString();
            row.Cells[ColFechaSalida].Value = reserva.FechaSalida.ToShortDateString();
            row.Cells[ColIdReserva].Value = reserva.IdReserva.ToString();

            // Assign tooltip text
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.ToolTipText = reserva.ToString();
            }

            return;
        }


        private void FilaSeleccionadaConFiltroPorPersona()
        {
            int fila = System.Math.Max(0, this.grdLista.CurrentRow.Index);

            if (this.reservasPorPersona.Count > fila)
            {
                this.edDetalle.Text = this.reservasPorPersona[fila].ToString()
                    + ". ";//+this.reservas[fila].ToString();
                this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                this.edDetalle.SelectionLength = 0;
            }
            else
            {
                this.edDetalle.Clear();
            }

            return;
        }

		public void ActualizaDisponibilidad()
        {
            DateTime ahora = DateTime.Now;

			this.sbStatus.Text = "Habitaciones: " + this.habitacionesDisponibles.Count.ToString()
                            + " | " + ahora.ToShortDateString()
                            + " | " + ahora.ToShortTimeString();
			if (this.habitacionesDisponibles.Count > 0)
                this.ActualizaListaDisponibilidad(0);
        }

        private void ActualizaListaDisponibilidad(int numRow)
        {
            //int numReservas = this.reservas.Count;
			int numDisponibles = this.habitacionesDisponibles.Count;
            // Crea y actualiza filas
			for (int i = numRow; i < numDisponibles; ++i)
            {
				if (this.grdListaDisponibilidad.Rows.Count <= i)
                {
					this.grdListaDisponibilidad.Rows.Add();
                }

                this.ActualizaFilaDeListaDisponibilidad(i);
            }


            // Eliminar filas sobrantes
			int numExtra = this.grdListaDisponibilidad.Rows.Count - numDisponibles;
            for (; numExtra > 0; --numExtra)
            {
				this.grdListaDisponibilidad.Rows.RemoveAt(numDisponibles);
            }

            return;
        }

        private void ActualizaFilaDeListaDisponibilidad(int rowIndex)
        {
            if (rowIndex < 0
			    || rowIndex > this.grdListaDisponibilidad.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(rowIndex));
            }

			DataGridViewRow row = this.grdListaDisponibilidad.Rows[rowIndex];
			Habitacion habitacion = this.habitacionesDisponibles[rowIndex];

            // Assign data
            row.Cells[ColNum].Value = (rowIndex + 1).ToString().PadLeft(4, ' ');
			row.Cells[ColCliente].Value = habitacion.Identificador.ToString();
            row.Cells[ColTipoHabitacion].Value = habitacion.Tipo.ToString();
            row.Cells[ColComodidades].Value = habitacion.Comodidades.ToString();
            row.Cells[ColUltimaReserva].Value = habitacion.FechaReserva.ToString();


            // Assign tooltip text
            foreach (DataGridViewCell cell in row.Cells)
            {
				cell.ToolTipText = habitacion.ToString();
            }

            return;
        }


        private void FilaSeleccionadaDisponibilidad()
        {
			int fila = System.Math.Max(0, this.grdListaDisponibilidad.CurrentRow.Index);

			if (this.habitacionesDisponibles.Count > fila)
            {
				this.edDetalle.Text = this.habitacionesDisponibles[fila].ToString()
                    + ". ";//+this.reservas[fila].ToString();
                this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                this.edDetalle.SelectionLength = 0;
            }
            else
            {
                this.edDetalle.Clear();
            }

            return;
        }

		public void ActualizaOcupacion()
        {
            DateTime ahora = DateTime.Now;

			this.sbStatus.Text = "Reservas: " + this.habitacionesOcupadas.Count.ToString()
                            + " | " + ahora.ToShortDateString()
                            + " | " + ahora.ToShortTimeString();
            if (this.habitacionesOcupadas.Count > 0)
                this.ActualizaListaOcupacion(0);
        }

        private void ActualizaListaOcupacion(int numRow)
        {
            //int numReservas = this.reservas.Count;
			int numReservas = this.habitacionesOcupadas.Count;
            // Crea y actualiza filas
            for (int i = numRow; i < numReservas; ++i)
            {
                if (this.grdLista.Rows.Count <= i)
                {
                    this.grdLista.Rows.Add();
                }

                this.ActualizaFilaDeListaOcupacion(i);
            }


            // Eliminar filas sobrantes
            int numExtra = this.grdLista.Rows.Count - numReservas;
            for (; numExtra > 0; --numExtra)
            {
                this.grdLista.Rows.RemoveAt(numReservas);
            }

            return;
        }

        private void ActualizaFilaDeListaOcupacion(int rowIndex)
        {
            if (rowIndex < 0
              || rowIndex > this.grdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(rowIndex));
            }

            DataGridViewRow row = this.grdLista.Rows[rowIndex];
			Reserva reserva = this.habitacionesOcupadas[rowIndex];

            // Assign data
            row.Cells[ColNum].Value = (rowIndex + 1).ToString().PadLeft(4, ' ');
            row.Cells[ColCliente].Value = reserva.Cliente.Dni;
            row.Cells[ColNumHabitacion].Value = reserva.Habitacion.Identificador.ToString();
            //row.Cells[ColPisoHabitacion].Value = reserva.Habitacion.Piso.ToString();
            row.Cells[ColFechaEntrada].Value = reserva.FechaEntrada.ToShortDateString();
            row.Cells[ColFechaSalida].Value = reserva.FechaSalida.ToShortDateString();
            row.Cells[ColIdReserva].Value = reserva.IdReserva.ToString();

            // Assign tooltip text
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.ToolTipText = reserva.ToString();
            }

            return;
        }


        private void FilaSeleccionadaOcupacion()
        {
            int fila = System.Math.Max(0, this.grdLista.CurrentRow.Index);

			if (this.habitacionesOcupadas.Count > fila)
            {
				this.edDetalle.Text = this.habitacionesOcupadas[fila].ToString()
                    + ". ";//+this.reservas[fila].ToString();
                this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                this.edDetalle.SelectionLength = 0;
            }
            else
            {
                this.edDetalle.Clear();
            }

            return;
        }


		public void Actualiza()
		{
            DateTime ahora = DateTime.Now;
            
			this.sbStatus.Text = "Reservas: " + this.reservas.Count.ToString()
                            + " | " + ahora.ToShortDateString()
                            + " | " + ahora.ToShortTimeString();

			this.ActualizaLista( 0 );
		}
      
        private void ActualizaLista(int numRow)
		{
			//int numReservas = this.reservas.Count;
			int numReservas = this.reservas.Count;
				// Crea y actualiza filas
				for (int i = numRow; i < numReservas; ++i)
				{
					if (this.grdLista.Rows.Count <= i)
					{
						this.grdLista.Rows.Add();
					}

					this.ActualizaFilaDeLista(i);
				}


			// Eliminar filas sobrantes
			int numExtra = this.grdLista.Rows.Count - numReservas;
			for(; numExtra > 0 ; --numExtra) {
				this.grdLista.Rows.RemoveAt( numReservas );
			}

			return;
		}

		private void ActualizaFilaDeLista(int rowIndex)
		{
			if ( rowIndex < 0
			  || rowIndex > this.grdLista.Rows.Count )
			{
				throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof( rowIndex ) );
			}

			DataGridViewRow row = this.grdLista.Rows[ rowIndex ];
			Reserva reserva = this.reservas[ rowIndex ];

            // Assign data
			row.Cells[ ColNum ].Value = ( rowIndex + 1 ).ToString().PadLeft( 4, ' ' );
			row.Cells[ColCliente].Value = reserva.Cliente.Dni;
			row.Cells[ ColNumHabitacion ].Value = reserva.Habitacion.Identificador.ToString();
			//row.Cells[ColPisoHabitacion].Value = reserva.Habitacion.Piso.ToString();
			row.Cells[ ColFechaEntrada ].Value = reserva.FechaEntrada.ToShortDateString();
			row.Cells[ ColFechaSalida ].Value = reserva.FechaSalida.ToShortDateString();
			row.Cells[ ColIdReserva].Value =  reserva.IdReserva.ToString();
            
            // Assign tooltip text
            foreach(DataGridViewCell cell in row.Cells) {
                cell.ToolTipText = reserva.ToString();
            }

			return;
		}

        private void FilaSeleccionada()
        {
            int fila = System.Math.Max(0, this.grdLista.CurrentRow.Index);

            if (this.reservas.Count > fila)
            {
                this.edDetalle.Text = this.reservas[fila].ToString()
                    + ". ";//+this.reservas[fila].ToString();
                this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                this.edDetalle.SelectionLength = 0;
               /* this.btFiltroDni.Text = "Ver más Reservas con este DNI:" + this.reservas[fila].Cliente.Dni;
                this.btFiltroDni.Click += (sender, args) =>
                { this.FiltrarPorPersona(sender, args, this.reservas[fila].Cliente.Dni); };*/
            }
            else
            {
                this.edDetalle.Clear();
            }

            return;
        }



        private List<Reserva> res;
        private RegistroReservas reservas;
        private RegistroClientes clientes;
        private RegistroHabitaciones regHabitaciones;
        private List<Reserva> reservasCincoDias;
        private List<Reserva> reservasPorHabitacionCincoDias;
        private List<Reserva> reservasPorPersona;
		private List<Reserva> reservasPorHabitacion;
		private List<Habitacion> habitacionesDisponibles;
        private List<Reserva> habitacionesOcupadas;
	}
}
