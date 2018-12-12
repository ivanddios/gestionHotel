using System;
namespace ReservasHotel
{
    public class Habitacion
    {
        public Habitacion(string id, string tipo)
        {
            this.IdHabitacion = id;
			this.Tipo = tipo;
        }

        public string IdHabitacion
        {
            get; private set;
        }

		public string Tipo
        {
            get; private set;
        }

        public override string ToString()
        {
            return "Habitacion: " + IdHabitacion + " tipo: " + Tipo;
        }
    }
}