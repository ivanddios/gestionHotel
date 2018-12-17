using System;
using Habitaciones.Core;
using Habitaciones.XML;
using System.Windows.Forms;

namespace Habitaciones.UI
{
    public class MainWindowCore : Form
    {

        public MainWindowCore(MainWindowView mv)
        {
            //this.MainWindowView = new MainWindowView();
            this.MainWindowView = mv;
            this.Registro = RegistroHabitaciones.RecuperaXml();
            //this.MainWindowView.FormClosed += (sender, e) => this.OnQuit();
            //this.MainWindowView.opSalir.Click += (sender, e) => this.Salir();
            //this.MainWindowView.OpInsertarHabitacion.Click += (sender, e) => this.BuildHabitacion();
            this.MainWindowView.GrdLista.Click += (sender, e) => this.selectOperation();
            this.Actualiza();

        }

        public void BuildHabitacion()
        {
            var dlgHabitacion = new DlgInsertaHabitacion();

            if (dlgHabitacion.ShowDialog() == DialogResult.OK)
            {
                Habitacion habitacion = new Habitacion(dlgHabitacion.Tipo,
                                            dlgHabitacion.FechaReserva,
                                            dlgHabitacion.Identificador,
                                            dlgHabitacion.FechaRenovacion,
                                            dlgHabitacion.Comodidades);
                this.Registro.Add(habitacion);
                this.Registro.GuardaXml();
                this.Actualiza();
            }
        }

        void selectOperation()
        {
            this.MainWindowView.GrdLista.Enabled = false;


            if (this.MainWindowView.GrdLista.CurrentCell.ColumnIndex == 6)
            {
                
                //MessageBox.Show("Column button clicked ");
                this.Eliminar();
            }
            else if (this.MainWindowView.GrdLista.CurrentCell.ColumnIndex == 7)
            {

                //MessageBox.Show("Column button clicked ");
                int Identificador = (int)this.MainWindowView.GrdLista.CurrentRow.Cells[3].Value;
                this.UpdateHabitacion(Identificador);
            }
            else if (this.MainWindowView.GrdLista.CurrentCell.ColumnIndex == 8)
            {
                
                int Identificador = (int)this.MainWindowView.GrdLista.CurrentRow.Cells[3].Value;
                GestionDeHoteles.GUI.MainWindow main = new GestionDeHoteles.GUI.MainWindow(new GestionDeHoteles.XML.XMLBrowser(), 1280, 720);
                main.Show();
                main.setGraficoHabitacion(Identificador);

                Gtk.Application.Run();

            }

            this.MainWindowView.GrdLista.Enabled = true;
        }
        
        void Eliminar()
        {
            int Identificador = (int)this.MainWindowView.GrdLista.CurrentRow.Cells[3].Value;
            DialogResult result;
            string mensaje = "Estás seguro de querer borrar la habitacion " + Identificador + "? ";
            string title = "Eliminar Habitacion";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            result = MessageBox.Show(mensaje, title, buttons);

            if (result == DialogResult.Yes)
            {
                Registro.Remove(Registro.getHabitacion(Identificador));
                this.Registro.GuardaXml();
                this.Actualiza();
            }
        }

        void UpdateHabitacion(int Identificador)
        {
            Habitacion habitacion = this.Registro.getHabitacion(Identificador);
            var dlgModificarHabitacion = new DlgModificarHabitacion(habitacion);

            if (dlgModificarHabitacion.ShowDialog() == DialogResult.OK)
            {
                this.Registro.Remove(habitacion);

                Habitacion nuevoHabitacion = new Habitacion(dlgModificarHabitacion.Tipo,
                                                   dlgModificarHabitacion.FechaReserva,
                                                   dlgModificarHabitacion.Identificador,
                                                   dlgModificarHabitacion.FechaRenovacion,
                                                   dlgModificarHabitacion.Comodidades);
                this.Registro.Add(nuevoHabitacion);
                this.Registro.GuardaXml();

            }
            this.Actualiza();
        }


        void OnQuit()
        {
            this.Registro.GuardaXml();
            Application.Exit();
        }

        void Salir()
        {
            this.Registro.GuardaXml();
            Application.Exit();
        }


        public void Actualiza()
        {
            int numElementos = this.Registro.Count;
            //this.MainWindowView.SbStatus.Text = ("Numero reparaciones : " + numElementos);
            for (int i = 0; i < numElementos; i++)
            {
                if (this.MainWindowView.GrdLista.Rows.Count <= i)
                {
                    this.MainWindowView.GrdLista.Rows.Add();
                }
                this.ActualizarFilaLista(i);
            }

            // Eliminar filas sobrantes
            int numExtra = this.MainWindowView.GrdLista.Rows.Count - numElementos;
            for (; numExtra > 0; --numExtra)
            {
                this.MainWindowView.GrdLista.Rows.RemoveAt(numElementos);
            }
        }


        private void ActualizarFilaLista(int numFila)
        {
            if (numFila < 0 || numFila > this.MainWindowView.GrdLista.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException("Fila fuera de rango: " + nameof(numFila));
            }

            DataGridViewRow fila = this.MainWindowView.GrdLista.Rows[numFila];
            Habitacion habitacion = this.Registro.List[numFila];

            fila.Cells[0].Value = (numFila + 1).ToString().PadLeft(4, ' ');
            fila.Cells[1].Value = habitacion.Tipo;
            fila.Cells[2].Value = habitacion.FechaReserva;
            fila.Cells[3].Value = habitacion.Identificador;
            fila.Cells[4].Value = habitacion.FechaRenovacion;
            fila.Cells[5].Value = habitacion.Comodidades;


            foreach (DataGridViewCell celda in fila.Cells)
            {
                celda.ToolTipText = habitacion.ToString();
            }
        }


        public MainWindowView MainWindowView
        {
            get; private set;
        }
        public RegistroHabitaciones Registro { get; set; }
    }
}
