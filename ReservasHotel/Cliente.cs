using System;
namespace ReservasHotel
{
    public class Cliente
    {
        public Cliente(string nombre, string apellidos)
        {
            this.Nombre = nombre;
            this.Apellidos = apellidos;
        }

        public string Nombre
        {
            get; private set;
        }

        public string Apellidos
        {
            get; private set;
        }


        public override string ToString()
        {

            return "Cliente: " + this.Apellidos + ", " + this.Nombre;
        }
    }
}