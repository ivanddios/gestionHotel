using System;
using Gestión_Hotel.Core;
using Gestión_Hotel.XML;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using BusquedasHotel.Core;

namespace Gestión_Hotel.UI
{
    public class MainWindowCore : Form
    {

        public MainWindowCore(MainWindowViewClientes mvc)
        {
            //this.MainWindowViewClientes = new MainWindowViewClientes();
            this.MainWindowViewClientes = mvc;
            this.RegistroClientes = Gestión_Hotel.XML.RegistroClientes.RecuperaXml();
            this.BusquedasView = new BusquedasHotel.View.MainWindow();
            //this.MainWindowViewClientes.FormClosed += (sender, e) => this.Salir();
            //this.MainWindowViewClientes.opSalir.Click += (sender, e) => this.Salir();
            //this.MainWindowViewClientes.OpInsertarCliente.Click += (sender, e) => this.InsertarCliente();
            //this.MainWindowViewClientes.OpVerClientes.Click += (sender, e) => this.ActualizaClientes();
            this.MainWindowViewClientes.GrdListaClientes.Click += (sender, e) => this.SeleccionarAccionCliente();
            this.ActualizaClientes();
        }


        //Funcionalidades de Clientes

        void SeleccionarAccionCliente()
        {

            this.MainWindowViewClientes.GrdListaClientes.Enabled = false;

            //Case Modificar-Cliente
            if (this.MainWindowViewClientes.GrdListaClientes.CurrentCell.ColumnIndex == 6)
            {
                string DNI = (string)this.MainWindowViewClientes.GrdListaClientes.CurrentRow.Cells[2].Value;
                this.ModificarCliente(DNI);
            }
            //Case Eliminar-Cliente
            else if (this.MainWindowViewClientes.GrdListaClientes.CurrentCell.ColumnIndex == 7)
            {
                this.EliminarCliente();
            }
            else if (this.MainWindowViewClientes.GrdListaClientes.CurrentCell.ColumnIndex == 8)
            {
                string DNI = (string)this.MainWindowViewClientes.GrdListaClientes.CurrentRow.Cells[2].Value;

                GestionDeHoteles.GUI.MainWindow main = new GestionDeHoteles.GUI.MainWindow(new GestionDeHoteles.XML.XMLBrowser(), 1280, 720);
                main.Show();
                main.setGraficoCliente(DNI);
                Gtk.Application.Run();

            }
            else if (this.MainWindowViewClientes.GrdListaClientes.CurrentCell.ColumnIndex == 9)
            {
                string DNIBusqueda = (string)this.MainWindowViewClientes.GrdListaClientes.CurrentRow.Cells[2].Value;

                BusquedasHotel.View.MainWindow ventana = new BusquedasHotel.View.MainWindow();
                //this.MainWindowViewClientes.Hide();
                ventana.Show();
                ventana.FiltrarPorPersona(DNIBusqueda);

                ventana.ActualizaConFiltroPorPersona();
                this.MainWindowViewClientes.pnlPpal.Controls.Add(this.MainWindowViewClientes.pnlBusquedasPpal);

                ventana.ResizeWindow();
                //this.ModificarCliente(DNI);

            }

            this.MainWindowViewClientes.GrdListaClientes.Enabled = true;
        }

        public void InsertarCliente()
        {
            var dlgInsertarCliente = new DlgInsertarCliente();

            if (dlgInsertarCliente.ShowDialog() == DialogResult.OK)
            {
                if (this.validarDNI(dlgInsertarCliente.Dni) && this.validarEmail(dlgInsertarCliente.Email))
                {
                    Gestión_Hotel.Core.Cliente cliente = new Gestión_Hotel.Core.Cliente(dlgInsertarCliente.Nombre,
                                               dlgInsertarCliente.Dni,
                                               dlgInsertarCliente.Telefono,
                                               dlgInsertarCliente.Email,
                                               dlgInsertarCliente.Direccion);
                    this.RegistroClientes.Add(cliente);
                }
                else
                {
                    this.InsertarCliente();
                }

            }
            this.ActualizaClientes();
        }

        void EliminarCliente()
        {
            string DNI = (string)this.MainWindowViewClientes.GrdListaClientes.CurrentRow.Cells[2].Value;
            DialogResult result;
            string title = "Eliminar Cliente";
            string mensaje = "Estás seguro de querer borrar al cliente " + DNI + "? ";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            result = MessageBox.Show(mensaje, title, buttons);

            if (result == DialogResult.Yes)
            {
                RegistroClientes.Remove(RegistroClientes.getUsuario(DNI));
                this.ActualizaClientes();
            }
        }

        void ModificarCliente(string DNI)
        {
            Gestión_Hotel.Core.Cliente cliente = this.RegistroClientes.getUsuario(DNI);
            var dlgModificarCliente = new DlgModificarCliente(cliente);

            if (dlgModificarCliente.ShowDialog() == DialogResult.OK)
            {

                this.RegistroClientes.Remove(cliente);

                Gestión_Hotel.Core.Cliente nuevoUsuario = new Gestión_Hotel.Core.Cliente(dlgModificarCliente.Nombre,
                                                       dlgModificarCliente.Dni,
                                                       dlgModificarCliente.Telefono,
                                                       dlgModificarCliente.Email,
                                                       dlgModificarCliente.Direccion);
                this.RegistroClientes.Add(nuevoUsuario);

            }
            this.ActualizaClientes();
        }


        //ShowAll de Clientes
        void ActualizaClientes()
        {
            int numElementos = this.RegistroClientes.Count;
            //this.MainWindowView.SbStatus.Text = ("Numero reparaciones : " + numElementos);
            for (int i = 0; i < numElementos; i++)
            {
                if (this.MainWindowViewClientes.GrdListaClientes.Rows.Count <= i)
                {
                    this.MainWindowViewClientes.GrdListaClientes.Rows.Add();
                }
                this.ActualizarFilaListaClientes(i);
            }

            // Eliminar filas sobrantes
            int numExtra = this.MainWindowViewClientes.GrdListaClientes.Rows.Count - numElementos;
            for (; numExtra > 0; --numExtra)
            {
                this.MainWindowViewClientes.GrdListaClientes.Rows.RemoveAt(numElementos);
            }
        }

        private void ActualizarFilaListaClientes(int numFila)
        {
            if (numFila < 0 || numFila > this.MainWindowViewClientes.GrdListaClientes.Rows.Count)
            {
                throw new System.ArgumentOutOfRangeException("Fila fuera de rango: " + nameof(numFila));
            }

            DataGridViewRow fila = this.MainWindowViewClientes.GrdListaClientes.Rows[numFila];
            Gestión_Hotel.Core.Cliente cliente = this.RegistroClientes.List[numFila];

            fila.Cells[0].Value = (numFila + 1).ToString().PadLeft(4, ' ');
            fila.Cells[1].Value = cliente.Nombre;
            fila.Cells[2].Value = cliente.Dni;
            fila.Cells[3].Value = cliente.Telefono;
            fila.Cells[4].Value = cliente.Email;
            fila.Cells[5].Value = cliente.Direccion;


            foreach (DataGridViewCell celda in fila.Cells)
            {
                celda.ToolTipText = cliente.ToString();
            }
        }

        //Validaciones de Clientes
        private bool validarDNI(string DNI)
        {
            string expresion = @"^[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][A-Z]$";
            var dnisXML = this.RegistroClientes.getDNIs();
            bool existeDNI = true;

            if (Regex.Match(DNI, expresion).Success == true)
            {
                foreach (string dniXML in dnisXML)
                {
                    if (DNI == dniXML)
                    {
                        existeDNI = false;
                    }
                }

                if (existeDNI)
                {
                    return true;
                }
                else
                {
                    DialogResult result;
                    string title = "Error en la operación";
                    string mensaje = "El DNI introducido ya existe.";
                    result = MessageBox.Show(mensaje, title);
                    return false;
                }
            }
            else
            {
                DialogResult result;
                string title = "Error en la operación";
                string mensaje = "El formato del DNI es incorrecto.";
                result = MessageBox.Show(mensaje, title);
                return false;
            }

        }

        private bool validarEmail(string email)
        {
            string expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            var emailsXML = this.RegistroClientes.getEmails();
            bool existeEmail = true;

            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {

                    foreach (string emailXML in emailsXML)
                    {
                        if (emailXML == email)
                        {
                            existeEmail = false;
                        }
                    }

                    if (existeEmail)
                    {
                        return true;
                    }
                    else
                    {
                        DialogResult result;
                        string title = "Error en la operación";
                        string mensaje = "EL email introducido ya existe.";
                        result = MessageBox.Show(mensaje, title);
                        return false;
                    }

                }
                else
                {
                    DialogResult result;
                    string title = "Error en la operación";
                    string mensaje = "El formato del email es incorrecto.";
                    result = MessageBox.Show(mensaje, title);
                    return false;
                }
            }
            else
            {
                DialogResult result;
                string title = "Error en la operación";
                string mensaje = "El formato del email es incorrecto.";
                result = MessageBox.Show(mensaje, title);
                return false;
            }
        }



        void Salir()
        {
            this.RegistroClientes.GuardaXml();
            Application.Exit();
        }


        public MainWindowViewClientes MainWindowViewClientes
        {
            get; private set;
        }

        public BusquedasHotel.View.MainWindow BusquedasView
        {
            get; private set;
        }

        public Gestión_Hotel.XML.RegistroClientes RegistroClientes { get; set; }
    }
}
