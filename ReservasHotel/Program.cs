

namespace ReservasHotel
{
	using System;
	using System.Windows.Forms;
	using ReservasHotel.View;

    class MainClass
    {
        public static void Main(string[] args)
        {
			var dEntrada = new DateTime(2018, 11, 10);
			var dSalida = new DateTime(2018, 11, 15);
			var h1 = new Habitacion("223", "individual");
            var h2 = new Habitacion("214", "matrimonio");
            var h3 = new Habitacion("132", "individual");

            var c = new Cliente("Patri", "Martin");

			var r1 = new Reserva(h1, c, dEntrada, dSalida, true, 14.2);
			var r2 = new Reserva(h2, c, dEntrada, dSalida, false, 18.2);
			//Console.WriteLine(r1);
			//Console.WriteLine(r2);

			//var reservas = new RegistroReservas();
            RegistroReservas reservas;
            //registro.Add(r1);
            //registro.Add(r2);
            //comprobar que la lista no es null
            //registro.GuardarXml("registro_reservas.xml");

            //registro.GuardarXml("registro_reservas.xml");

            //reservas = RecuperarXml("registro_reservas.xml");

            reservas = RegistroReservas.RecuperarXml("registro_reservas.xml");
            Console.WriteLine(reservas[0].GenerarFactura());

			var mainForm = new MainWindow();
            Application.Run(mainForm);
        }
    }
}
