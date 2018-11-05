using System;
namespace ReservasHotel {
    public class Habitacion {
        public Habitacion(string id) {
            this.IdHabitacion = id;
        }

        public string IdHabitacion {
            get; private set;
        }
    }
}
