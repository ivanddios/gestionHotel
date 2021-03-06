﻿namespace ReservasHotel.View
{
	using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using Habitaciones.Core;
    using Habitaciones.UI;
    using Habitaciones.XML;
    using Gestión_Hotel.Core;
    using Gestión_Hotel.XML;
    using GestionDeHoteles;




    public partial class MainWindow
    {
        public MainWindow()
        {
            //Creacion ventana principal
            this.Build();	
            this.reservas = RegistroReservas.RecuperarXml("registro_reservas.xml");
            this.habitaciones = RegistroHabitaciones.RecuperaXml();
            this.clientes = RegistroClientes.RecuperaXml();
            this.ActualizaListaReservas(0);


            //System.IO.File.Copy(sourceFile, destFile, true);
           // string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + reserva.IdReserva + ".txt";
           // File.WriteAllText(@path, reserva.GenerarFactura());


            this.BusquedasView = new BusquedasHotel.View.MainWindow();
            this.pnlBusquedasPpal = this.BusquedasView.BuildPpal();
            this.pnlBusquedasDisponibilidad = this.BusquedasView.BuildPanelListaDisponibilidad();

            //this.pnlBusquedasPpal =  this.BusquedasView.devolverPanelPpal();
            //this.pnlBusquedasDisponibilidad = this.BusquedasView.devolverPanelDisponibilidad();

            //Integracion CLIENTES
            this.ClienteView = new Gestión_Hotel.UI.MainWindowViewClientes();
            this.pnlClientes = this.ClienteView.BuildGUI();
            this.ClienteCore = new Gestión_Hotel.UI.MainWindowCore(this.ClienteView);

            //Integracion HABITACIONES
            this.HabitacionView = new Habitaciones.UI.MainWindowView();
            this.pnlHabitaciones = this.HabitacionView.BuildGUI();
            this.HabitacionCore = new Habitaciones.UI.MainWindowCore(this.HabitacionView);

            //Operaciones CLIENTES
            this.OpConsultarClientes.Click += (sender, e) => this.mostrarClientes();
            this.OpInsertarCliente.Click += (sender, e) => this.insertarCliente();

            //Operaciones HABITACIONES
            this.OpConsultarHabitaciones.Click += (sender, e) => this.mostrarHabitaciones();
            this.OpInsertarHabitacion.Click += (sender, e) => this.insertarHabitacion();

            //Operaciones RESERVASS
            this.OpConsultarReservas.Click += (sender, e) => this.mostrarReservas();
            this.OpGraficos.Click += (sender, e) => this.generarGrafico();
            this.OpAltaReserva.Click += (sender, e) => this.AltaReserva();

            //Operaciones BUSQUEDAS

            this.OpReservasPendientes.Click += (sender, e) => this.reservasPendientes();
            this.OpDisponibilidad.Click += (sender, e) => this.disponibilidad();
            this.OpOcupacion.Click += (sender, e) => this.ocupacion();
            this.OpFiltroHabitacion.Click += (sender, e) => this.filtrarPorHabitacion();
            this.OpFiltroPersona.Click += (sender, e) => this.filtrarPorPersona();


            this.FormClosed += (sender, e) => this.OnQuit();

        }



        /*
         * Metodos Clientes
         */
        private void mostrarClientes()
        {
            this.pnlPpal.Controls.Clear();
            this.pnlPpal.Controls.Add(this.pnlClientes);
            this.ClienteView.ResizeWindow();
        }

        private void insertarCliente()
        {
            this.ClienteCore.InsertarCliente();

        }

        /*
         * Metodos Habitaciones
         */
        private void mostrarHabitaciones()
        {
            this.habitaciones = RegistroHabitaciones.RecuperaXml();
            this.pnlPpal.Controls.Clear();
            this.pnlPpal.Controls.Add(this.pnlHabitaciones);
            this.HabitacionView.ResizeWindow();
        }

        private void insertarHabitacion()
        {

            this.HabitacionCore.BuildHabitacion();
        }




        /* Métodos Búsquedas*/

        private void reservasPendientes()
        {
            this.pnlPpal.Controls.Clear();
            
            this.BusquedasView.PendientesHotel5Dias();
            this.BusquedasView.Actualiza();
            this.pnlPpal.Controls.Add(this.pnlBusquedasPpal);
            
            this.BusquedasView.ResizeWindow();
        }

        private void disponibilidad()
        {
            this.pnlPpal.Controls.Clear();

            this.BusquedasView.Disponibilidad();
            this.BusquedasView.ActualizaDisponibilidad();
            this.pnlPpal.Controls.Add(this.pnlBusquedasDisponibilidad);

            this.BusquedasView.ResizeWindow();            
        }


        private void ocupacion()
        {
            this.pnlPpal.Controls.Clear();

            this.BusquedasView.Ocupacion();
            this.BusquedasView.ActualizaOcupacion();
            this.pnlPpal.Controls.Add(this.pnlBusquedasPpal);

            this.BusquedasView.ResizeWindow();

        }

        private void filtrarPorHabitacion()
        {
            this.pnlPpal.Controls.Clear();

            this.BusquedasView.FiltrarPorHabitacion();
            this.BusquedasView.ActualizaConFiltroPorHabitacion();
            this.pnlPpal.Controls.Add(this.pnlBusquedasPpal);

            this.BusquedasView.ResizeWindow();

        }

        private void filtrarPorPersona()
        {
            this.pnlPpal.Controls.Clear();

            this.BusquedasView.FiltrarPorPersona();
            this.BusquedasView.ActualizaConFiltroPorPersona();
            this.pnlPpal.Controls.Add(this.pnlBusquedasPpal);

            this.BusquedasView.ResizeWindow();
        }

        /*
         * Metodos Reservas
         */

        private void mostrarReservas()
        {
            this.pnlPpal.Controls.Clear();
            this.pnlPpal.Controls.Add(this.BuildPanel());
            this.ResizeWindow();
            this.ActualizaListaReservas(0);
        }

        private void AltaReserva()
        {
            var habitaciones = RegistroHabitaciones.RecuperaXml();

            var dlgAltaReserva = new DlgAltaReserva(habitaciones, null, this.clientes);


            if (dlgAltaReserva.ShowDialog() == DialogResult.OK)
            {
                var h = dlgAltaReserva.habitacion;
                h.FechaReserva = dlgAltaReserva.FechaSalida.ToString("yyyy/MM/dd");
                 var reserva = new Reserva(h, dlgAltaReserva.cliente,
                    dlgAltaReserva.FechaEntrada, dlgAltaReserva.FechaSalida, dlgAltaReserva.UsaGaraje, dlgAltaReserva.Tarifa);
                this.reservas.Add(reserva);

                
                for (int i=0; i<this.habitaciones.Count; i++)
                {
                    Console.WriteLine("Borrar: " + habitaciones[i].Identificador + " " + h.Identificador);
                    Console.WriteLine("Posible borrado hab: " + this.HabitacionCore.Registro.getHabitacion(habitaciones[i].Identificador).ToString());
                    if (this.habitaciones[i].Identificador == h.Identificador){
                       
                        Console.WriteLine("Borrar: " + habitaciones[i].Identificador + " " + h.Identificador);
                        //this.habitaciones[i].FechaReserva = h.FechaReserva;
                        //Console.WriteLine(this.HabitacionCore.Registro.Remove(habitaciones[i]));
                        this.HabitacionCore.Registro.RemoveAt(i);

                    }
                
                }

                this.HabitacionCore.Registro.Add(h);
                this.HabitacionCore.Registro.GuardaXml();
                this.habitaciones = RegistroHabitaciones.RecuperaXml();
                this.HabitacionCore.Actualiza();

                Console.WriteLine(reserva);
                actualizarReservas();
              

            }
           

            return;
        }

        private void ActualizaListaReservas(int numRow)
        {
            int numRecorridos = this.reservas.Count;

            // Crea y actualiza filas
            for (int i = numRow; i < numRecorridos; ++i)
            {
                if (this.grdLista.Rows.Count <= i)
                {
                    this.grdLista.Rows.Add();
                }

                this.ActualizaFilaDeListaReservas(i);
            }

            // Eliminar filas sobrantes
            int numExtra = this.grdLista.Rows.Count - numRecorridos;
            for (; numExtra > 0; --numExtra)
            {
                this.grdLista.Rows.RemoveAt(numRecorridos);
            }

            return;
        }

        private void ActualizaFilaDeListaReservas(int rowIndex)
        {
            if (rowIndex < 0
              || rowIndex > this.grdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException(
                            "fila fuera de rango: " + nameof(rowIndex));
            }

            DataGridViewRow row = this.grdLista.Rows[rowIndex];
            Reserva reserva = this.reservas[rowIndex];

            // Assign data
            row.Cells[0].Value = (rowIndex + 1).ToString().PadLeft(4, ' ');
            row.Cells[1].Value = reserva.IdReserva;
            row.Cells[2].Value = reserva.FechaEntrada;
            row.Cells[3].Value = reserva.FechaSalida;
            row.Cells[4].Value = reserva.calcularTotal();
            row.Cells[5].Value = reserva.Cliente.Nombre;


            // Assign tooltip text
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.ToolTipText = reserva.ToString();
            }

            return;
        }

        private void FilaSeleccionada()
        {
            int fila = System.Math.Max(0, this.grdLista.CurrentRow.Index);

            if (this.reservas.Count > fila)
            {
                this.edDetalle.Text = this.reservas[fila].ToString();
                this.edDetalle.SelectionStart = this.edDetalle.Text.Length;
                this.edDetalle.SelectionLength = 0;
            }
            else
            {
                this.edDetalle.Clear();
            }

            return;
        }


       



        public void modificarReserva()
        {
            DataGridViewRow fila = this.grdLista.CurrentRow;
            
                
                if (fila  != null)
                {

                    var reserva = this.reservas.getReserva(fila.Cells[1].Value.ToString());

                    if (reserva != null)
                    {
                        
                        var dlgAltaReserva = new DlgAltaReserva(habitaciones, reserva, this.clientes);
                        if (dlgAltaReserva.ShowDialog() == DialogResult.OK)
                        {
                            this.reservas.Remove(reserva);
                            var nuevaReserva = new Reserva(dlgAltaReserva.habitacion, dlgAltaReserva.cliente,
                                dlgAltaReserva.FechaEntrada, dlgAltaReserva.FechaSalida, dlgAltaReserva.UsaGaraje, dlgAltaReserva.Tarifa);
                            this.reservas.Add(nuevaReserva);
                        Console.WriteLine("NUEVA RESRVA");
                        Console.WriteLine(nuevaReserva);
                            actualizarReservas();

                        }

                    }


                }

            return;


        }

        public void actualizarReservas()
        {
            RegistroReservas.GuardarXml("registro_reservas.xml", this.reservas);
            this.reservas = RegistroReservas.RecuperarXml("registro_reservas.xml");
            this.ActualizaListaReservas(0);

        }

        public void eliminarReserva()
        {
            foreach (DataGridViewRow row in grdLista.SelectedRows)
            {
                if (row.Cells[0].Value != null)
                {
                    
                    Console.WriteLine(row.Cells[1].Value.ToString());
                    var reserva = this.reservas.getReserva(row.Cells[1].Value.ToString());
                    if (reserva != null)
                    {
                        this.reservas.Remove(reserva);
                    
                        actualizarReservas();

                    }
                    
                    
                }


            }
        }

        private void generarFactura()
        {
            foreach (DataGridViewRow row in grdLista.SelectedRows)
            {
                if (row.Cells[0].Value != null)
                {

                    var reserva = this.reservas.getReserva(row.Cells[1].Value.ToString());
                    if (reserva != null)
                    {

                        string message = "";
                        string caption = "";
                        // WriteAllText creates a file, writes the specified string to the file,
                        // and then closes the file.    You do NOT need to call Flush() or Close().
                        try
                        {
                            string path = Path.GetDirectoryName(Application.ExecutablePath) + "\\" + reserva.IdReserva + ".txt";
                            
                            File.WriteAllText(@path, reserva.GenerarFactura());
                            message = "Factura creada correctamente";
                            caption = "Exito";
                        }
                        catch (Exception ex)
                        {
                            message = "Ha ocurrido un problema al generar la factura";
                            caption = "Error";
                        }
                        finally
                        {
                               
                            MessageBoxButtons buttons = MessageBoxButtons.OK;
                            DialogResult result;

                            result = MessageBox.Show(message, caption, buttons);

                            if (result == System.Windows.Forms.DialogResult.OK)
                            {
                                //return false;
                            }
                        }

                       
                    }


                }


            }

        }

        private void generarGrafico()
        {
            
                GestionDeHoteles.GUI.MainWindow main = new GestionDeHoteles.GUI.MainWindow(new GestionDeHoteles.XML.XMLBrowser(), 1280, 720);
                main.Show();
                main.setGraficoGeneral();
                Gtk.Application.Run();
            
        }




        private void OnQuit()
        {
            RegistroReservas.GuardarXml("registro_reservas.xml", this.reservas);
            this.ClienteCore.RegistroClientes.GuardaXml();
            this.HabitacionCore.Registro.GuardaXml();
            Application.Exit();
        }

        private void Salir()
        {

            RegistroReservas.GuardarXml("registro_reservas.xml", this.reservas);
            this.ClienteCore.RegistroClientes.GuardaXml();
            this.HabitacionCore.Registro.GuardaXml();
            Application.Exit();
        }


        public DlgAltaReserva DlgAltaReserva
        {
            get; private set;
        }

        public Habitaciones.UI.MainWindowView HabitacionView
        {
            get; private set;
        }

        public Habitaciones.UI.MainWindowCore HabitacionCore
        {
            get; private set;
        }

        public Gestión_Hotel.UI.MainWindowViewClientes ClienteView
        {
            get; private set;
        }

        public Gestión_Hotel.UI.MainWindowCore ClienteCore
        {
            get; private set;
        }

        public BusquedasHotel.View.MainWindow BusquedasView
        {
            get; private set;
        }

        private RegistroReservas reservas;
        private RegistroHabitaciones habitaciones;
        private RegistroClientes clientes;

    }
}
