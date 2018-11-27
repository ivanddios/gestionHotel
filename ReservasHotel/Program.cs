using System;

namespace ReservasHotel
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			var dEntrada = new DateTime(2018, 11, 10);
			var dSalida = new DateTime(2018, 11, 15);
			var h = new Habitacion("223", "individual");

			var c = new Cliente("Patri", "Martin");

			var r1 = new Reserva(h, c, dEntrada, dSalida, true, 14.2);
			var r2 = new Reserva(h, c, dEntrada, dSalida, false, 18.2);
			//Console.WriteLine(r1);
			//Console.WriteLine(r2);

			var reservas = new RegistroReservas();

			//registro.Add(r1);
			//registro.Add(r2);
            //comprobar que la lista no es null
			//registro.GuardarXml("registro_reservas.xml");

			//registro.GuardarXml("registro_reservas.xml");
            
			reservas = reservas.RecuperarXml("registro_reservas.xml");
			Console.WriteLine(reservas[0].GenerarFactura());


        }
    }
}
