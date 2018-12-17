namespace BusquedasHotel
{
    using System;
    using System.Windows.Forms;

    public class Ppal
    {

        [STAThread]
        public static void Main()
        {
			var form = new BusquedasHotel.View.MainWindow();

            try
            {
                form.Show();
                Application.Run(form);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Unexpected: " + exc.Message, "Demo",
                                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
