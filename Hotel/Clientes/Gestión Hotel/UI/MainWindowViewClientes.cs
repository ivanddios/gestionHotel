using System;
using System.Drawing;
using System.Windows.Forms;

namespace Gestión_Hotel.UI
{
    public class MainWindowViewClientes : Form
    {

        public MainWindowViewClientes()
        {
            //this.BuildGUI();
        }

        public Panel BuildGUI()
        {

            //this.BuildMenu();

            this.SuspendLayout();
            this.pnlPpal = new Panel()
            {
                Dock = DockStyle.Fill
            };

            //this.pnlPpal.SuspendLayout();
            //this.Controls.Add(this.pnlPpal);
            this.pnlPpal.Controls.Add(this.BuildPanelLista());
            this.pnlPpal.ResumeLayout(false);
            this.pnlPpal.Resize += (obj, e) => this.ResizeWindow();

            /*this.MinimumSize = new Size(885, 500);
            this.Resize += (obj, e) => this.ResizeWindow();
            this.Text = "Clientes";*/

            this.ResumeLayout(true);
            this.ResizeWindow();
            return pnlPpal;
        }


        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            this.mArchivo = new MenuItem("&Archivo");
            this.mClientes = new MenuItem("&Clientes");
            this.mHabitaciones = new MenuItem("&Habitaciones");

            this.opSalir = new MenuItem("&Salir");
            this.opSalir.Shortcut = Shortcut.CtrlQ;

            this.OpInsertarCliente = new MenuItem("&Nuevo Cliente");
            this.OpVerClientes = new MenuItem("&Ver Clientes");

            this.OpInsertarHabitacion = new MenuItem("&Nueva Habitacion");
            this.OpVerHabitaciones = new MenuItem("&Ver Habitaciones");

            this.mArchivo.MenuItems.Add(this.opSalir);
            this.mClientes.MenuItems.Add(this.OpInsertarCliente);
            this.mClientes.MenuItems.Add(this.OpVerClientes);
            this.mHabitaciones.MenuItems.Add(this.OpInsertarHabitacion);
            this.mHabitaciones.MenuItems.Add(this.OpVerHabitaciones);


            this.mPpal.MenuItems.Add(this.mArchivo);
            this.mPpal.MenuItems.Add(this.mClientes);
            this.mPpal.MenuItems.Add(this.mHabitaciones);
            this.Menu = mPpal;
        }

        private void BuildStatus()
        {
            this.SbStatus = new StatusBar();
            this.SbStatus.Dock = DockStyle.Bottom;
            this.Controls.Add(this.SbStatus);
        }

        private Panel BuildPanelLista()
        {
            this.pnlLista = new Panel();
            this.pnlLista.SuspendLayout();
            this.pnlLista.Dock = DockStyle.Fill;
            this.pnlPpal.Controls.Add(this.pnlLista);
          

            // Crear gridview
            this.GrdListaClientes = new DataGridView()
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

            this.GrdListaClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.GrdListaClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;


            var textCellTemplate0 = new DataGridViewTextBoxCell();
            var textCellTemplate1 = new DataGridViewTextBoxCell();
            var textCellTemplate2 = new DataGridViewTextBoxCell();
            var textCellTemplate3 = new DataGridViewTextBoxCell();
            var textCellTemplate4 = new DataGridViewTextBoxCell();
            var textCellTemplate5 = new DataGridViewTextBoxCell();


            textCellTemplate0.Style.BackColor = Color.LightGray;
            textCellTemplate0.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.BackColor = Color.Wheat;
            textCellTemplate1.Style.ForeColor = Color.Black;
            textCellTemplate1.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            textCellTemplate2.Style.BackColor = Color.Wheat;
            textCellTemplate2.Style.ForeColor = Color.Black;
            textCellTemplate3.Style.BackColor = Color.Wheat;
            textCellTemplate3.Style.ForeColor = Color.Black;
            textCellTemplate4.Style.BackColor = Color.Wheat;
            textCellTemplate4.Style.ForeColor = Color.Black;
            textCellTemplate5.Style.BackColor = Color.Wheat;
            textCellTemplate5.Style.ForeColor = Color.Black;

            var column0 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate0,
                HeaderText = "#",
                ReadOnly = true,
                Width = 5

            };

            var column1 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Nombre",
                ReadOnly = true,
                Width = 15

            };

            var column2 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "DNI",
                ReadOnly = true,
                Width = 15

            };

            var column3 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Teléfono",
                ReadOnly = true,
                Width = 15

            };

            var column4 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Email",
                ReadOnly = true,
                Width = 15

            };


            var column5 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate5,
                HeaderText = "Dirección",
                ReadOnly = true,
                Width = 30

            };

            var column6 = new DataGridViewButtonColumn()
            {
                HeaderText = "Acción",
                Text = "Modificar",
                UseColumnTextForButtonValue = true,
                ReadOnly = true,
                Width = 15

            };

            var column7 = new DataGridViewButtonColumn()
            {
                HeaderText = "Acción",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
                ReadOnly = true,
                Width = 15

            };

            var column8 = new DataGridViewButtonColumn()
            {
                HeaderText = "Acción",
                Text = "Gráfico",
                UseColumnTextForButtonValue = true,
                ReadOnly = true,
                Width = 15

            };

            this.GrdListaClientes.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3,column4,column5,column6, column7,column8
            });

            this.pnlLista.Controls.Add(this.GrdListaClientes);
            this.pnlLista.ResumeLayout(false);

            return this.pnlLista;
        }


        public void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;

            // Redimensionar la tabla
            this.GrdListaClientes.Width = width;

            this.GrdListaClientes.Columns[0].Width =
                                (int)System.Math.Floor(width * .05);
            this.GrdListaClientes.Columns[1].Width =
                                (int)System.Math.Floor(width * .192);
            this.GrdListaClientes.Columns[2].Width =
                                (int)System.Math.Floor(width * .08);
            this.GrdListaClientes.Columns[3].Width =
                               (int)System.Math.Floor(width * .08);
            this.GrdListaClientes.Columns[4].Width =
                                (int)System.Math.Floor(width * .16);
            this.GrdListaClientes.Columns[5].Width =
                                (int)System.Math.Floor(width * .16);
            this.GrdListaClientes.Columns[6].Width =
                                (int)System.Math.Floor(width * .10);
            this.GrdListaClientes.Columns[7].Width =
                                (int)System.Math.Floor(width * .09);
            this.GrdListaClientes.Columns[8].Width =
                                (int)System.Math.Floor(width * .09);
        }

        public DataGridView GrdListaClientes;
        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem mClientes;
        public MenuItem mHabitaciones;
        public MenuItem opSalir;
        public MenuItem OpInsertarCliente;
        public MenuItem OpInsertarHabitacion;
        public MenuItem OpVerClientes;
        public MenuItem OpVerHabitaciones;
        public StatusBar SbStatus;
        public Panel pnlLista;
        public Panel pnlPpal;
  


    }
}
