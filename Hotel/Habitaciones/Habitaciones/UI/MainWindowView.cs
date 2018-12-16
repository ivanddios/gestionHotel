using System;
using System.Drawing;
using System.Windows.Forms;

namespace Habitaciones.UI
{
    public class MainWindowView : Form
    {

        public MainWindowView()
        {
           // this.BuildGUI();
        }

        public Panel BuildGUI()
        {
            //this.BuildStatus();
            //this.BuildMenu();

            //this.SuspendLayout();
            this.pnlPpal = new Panel()
            {
                Dock = DockStyle.Fill
            };

            //this.pnlPpal.SuspendLayout();
            //this.Controls.Add(this.pnlPpal);
            this.pnlPpal.Controls.Add(this.BuildPanelLista());
            this.pnlPpal.ResumeLayout(false);
            this.pnlPpal.Resize += (obj, e) => this.ResizeWindow();
            /*
             this.MinimumSize = new Size(800, 500);
             this.Resize += (obj, e) => this.ResizeWindow();
             this.Text = "Habitaciones";

             this.ResumeLayout(true);*/
            this.ResumeLayout(true);
            this.ResizeWindow();
            

            return pnlPpal;
        }


        private void BuildMenu()
        {
            this.mPpal = new MainMenu();

            this.mArchivo = new MenuItem("&Archivo");
            this.mInsertar = new MenuItem("&Insertar");

            this.opSalir = new MenuItem("&Salir");
            this.opSalir.Shortcut = Shortcut.CtrlQ;

            this.OpInsertarHabitacion = new MenuItem("&Nueva Habitacion");

            this.mArchivo.MenuItems.Add(this.opSalir);
            this.mInsertar.MenuItems.Add(this.OpInsertarHabitacion);


            this.mPpal.MenuItems.Add(this.mArchivo);
            this.mPpal.MenuItems.Add(this.mInsertar);
            this.Menu = mPpal;
        }

        private void BuildStatus()
        {
            this.SbStatus = new StatusBar();
            this.SbStatus.Dock = DockStyle.Bottom;
            this.Controls.Add(this.SbStatus);
        }

        public Panel BuildPanelLista()
        {
            this.pnlLista = new Panel();
            this.pnlLista.SuspendLayout();
            this.pnlLista.Dock = DockStyle.Fill;
            this.pnlPpal.Controls.Add(this.pnlLista);

            // Crear gridview
            this.GrdLista = new DataGridView()
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

            this.GrdLista.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            this.GrdLista.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;


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
                
            };

            var column1 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate1,
                HeaderText = "Tipo",
                ReadOnly = true,
                  
            };

            var column2 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate2,
                HeaderText = "Fecha última reserva",
                ReadOnly = true,
                         
            };

            var column3 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate3,
                HeaderText = "Identificador",
                ReadOnly = true,
                Width = 15

            };

            var column4 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate4,
                HeaderText = "Fecha de renovación",
                ReadOnly = true,
                Width = 15

            };


            var column5 = new DataGridViewTextBoxColumn()
            {
                SortMode = DataGridViewColumnSortMode.NotSortable,
                CellTemplate = textCellTemplate5,
                HeaderText = "Comodidades",
                ReadOnly = true,
                Width = 30

            };

            var column6 = new DataGridViewButtonColumn()
            {
                HeaderText = "Opciones",
                Text = "Eliminar",
                UseColumnTextForButtonValue = true,
				ReadOnly = true

            };

			var column7 = new DataGridViewButtonColumn()
			{
				HeaderText = "Opciones",            
                Text = "Modificar",
                UseColumnTextForButtonValue = true,
                ReadOnly = true

            };

            var column8 = new DataGridViewButtonColumn()
            {
                HeaderText = "Acción",
                Text = "Gráfico",
                UseColumnTextForButtonValue = true,
                ReadOnly = true,
                Width = 15

            };




            this.GrdLista.Columns.AddRange(new DataGridViewColumn[] {
                column0, column1, column2, column3,column4,column5,column6, column7, column8
            });

            this.pnlLista.Controls.Add(this.GrdLista);
            this.pnlLista.ResumeLayout(false);

            return this.pnlLista;
        }


        public void ResizeWindow()
        {
            // Tomar las nuevas medidas
            int width = this.pnlPpal.ClientRectangle.Width;
           
            // Redimensionar la tabla
            this.GrdLista.Width = width;

            this.GrdLista.Columns[0].Width =
                                (int)System.Math.Floor(width * .04);
            this.GrdLista.Columns[1].Width =
                                (int)System.Math.Floor(width * .11);
            this.GrdLista.Columns[2].Width =
                                (int)System.Math.Floor(width * .11);
            this.GrdLista.Columns[3].Width =
                               (int)System.Math.Floor(width * .12);
            this.GrdLista.Columns[4].Width =
                                (int)System.Math.Floor(width * .13);
            this.GrdLista.Columns[5].Width =
                                (int)System.Math.Floor(width * .22);
            this.GrdLista.Columns[6].Width =
                                (int)System.Math.Floor(width * .09);
            this.GrdLista.Columns[7].Width =
                                (int)System.Math.Floor(width * .09);
            this.GrdLista.Columns[8].Width =
                               (int)System.Math.Floor(width * .09);
        }

        private MainMenu mPpal;
        public MenuItem mArchivo;
        public MenuItem mInsertar;
        public MenuItem opSalir;
        public MenuItem OpInsertarHabitacion;
        public StatusBar SbStatus;
        private Panel pnlLista;
        public Panel pnlPpal;
        public DataGridView GrdLista;


    }
}
