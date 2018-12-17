using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusquedasHotel.Core
{
    public class Cliente
    {

        public Cliente(string nombre, string dni, int telefono, string email, string direccion)
        {
            this.Nombre = nombre;
            this.Dni = dni;
            this.Telefono = telefono;
            this.Email = email;
            this.Direccion = direccion;
        }


        public string Nombre
        {
            get;
        }
        public string Dni
        {
            get;
        }
        public int Telefono
        {
            get;
        }

        public string Email
        {
            get;
        }

        public string Direccion
        {
            get;
        }

        public override string ToString()
        {
            return "\n\tUsuario" + "\n\tNombre: " + this.Nombre + "\n\tDNI: " + this.Dni + "\n\tTeléfono: " + this.Telefono + "\n\tEmail: " + this.Email + "\n\tDireccion: " + this.Direccion;
        }

    }
}
