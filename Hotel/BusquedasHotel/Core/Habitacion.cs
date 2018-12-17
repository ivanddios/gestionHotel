using System;
using System.Collections.Generic;

namespace BusquedasHotel.Core
{
    public class Habitacion
    {
        public Habitacion(string tipo, string fechaReserva,int identificador,
                          string FechaRenovacion, string comodidades)
        {
            this.Tipo = tipo;
            this.Identificador = identificador;
            this.FechaRenovacion = FechaRenovacion;
            this.FechaReserva = fechaReserva;
            this.Comodidades = comodidades;
        }

        public string Tipo{
            get; private set;
        }

        public int Identificador{
            get; set;
        }
        public string FechaRenovacion{
            get; set;
        }

        public string FechaReserva{
            get;set;
        }

        public string Comodidades{
            get;set;
        }

        public override string ToString()
        {
            return "Tipo:" + this.Tipo + "\nIdentificador:" + this.Identificador + "\nFechaRenovacion:" + this.FechaRenovacion +
                                 "\nUltima Reserva:" + this.FechaReserva + "\nComodidades:" + this.Comodidades.ToString();
        }
    }
}
