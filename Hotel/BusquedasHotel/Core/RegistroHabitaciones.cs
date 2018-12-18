using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using BusquedasHotel.Core;


namespace BusquedasHotel
{
    public class RegistroHabitaciones: ICollection<Habitacion>
    {
		public const string ArchivoXML = "habitaciones.xml";
        public const string EtqHabitaciones = "habitaciones";
        public const string EtqHabitacion = "habitacion";
        public const string EtqTipo = "tipo";
        public const string EtqFechaReserva = "fechaReserva";
        public const string EtqIdentificador = "identificador";
        public const string EtqFechaRenovacion = "fechaRenovacion";
        public const string EtqComodidades = "comodidades";

        public RegistroHabitaciones()
        {
            this.habitaciones = new List<Habitacion>();
        }

        public List<Habitacion> List
        {
            get { return this.habitaciones; }
        }

        public void Add(Habitacion habitacion)
        {
            this.habitaciones.Add(habitacion);
        }

        public void GuardaXml()
        {
            this.GuardarXML(ArchivoXML);
        }

        public void GuardarXML(string nf)
        {
            var doc = new XDocument();
            var root = new XElement(EtqHabitaciones);

            foreach (Habitacion habitaciones in this.habitaciones)
            {
                var habitacion = new XElement(EtqHabitacion);
                var tipo = new XElement(EtqTipo, habitaciones.Tipo);
                habitacion.Add(tipo);
                var fechaReserva = new XElement(EtqFechaReserva, habitaciones.FechaReserva);
                habitacion.Add(fechaReserva);
                var identificador = new XElement(EtqIdentificador, habitaciones.Identificador);
                habitacion.Add(identificador);
                var fechaRenovacion = new XElement(EtqFechaRenovacion, habitaciones.FechaRenovacion);
                habitacion.Add(fechaRenovacion);
                var comodidades = new XElement(EtqComodidades, habitaciones.Comodidades);
                habitacion.Add(comodidades);

                root.Add(habitacion);
            }
            doc.Add(root);
            doc.Save(nf);
        }

        public static RegistroHabitaciones RecuperaXml()
        {
            var toret = new RegistroHabitaciones();

            try
            {
                var doc = XDocument.Load((@"C:\Users\ivand\OneDrive\Escritorio\gestionHotel\Hotel\ReservasHotel\ReservasHotel\bin\Debug\habitaciones.xml"));

                if (doc.Root != null && doc.Root.Name == EtqHabitaciones)
                {
                    var habitaciones = doc.Root.Elements(EtqHabitacion);

                    foreach (XElement habitacionXML in habitaciones)
                    {
                        //System.Console.WriteLine("*");

                        var elements = habitacionXML.Elements();
                        string tipo = "", fechaReserva = "", fechaRenovacion = "", comodidades = "";
                        int identificador = 0;

                        foreach (XElement elem in elements)
                        {
                            if (elem.Name == EtqTipo)
                            {
                                tipo = (string)habitacionXML.Element(EtqTipo);
                            }
                            else if (elem.Name == EtqFechaReserva)
                            {
                                fechaReserva = (string)habitacionXML.Element(EtqFechaReserva);
                            }
                            else if (elem.Name == EtqIdentificador)
                            {
                                identificador = (int)habitacionXML.Element(EtqIdentificador);
                            }
                            else if (elem.Name == EtqFechaRenovacion)
                            {
                                fechaRenovacion = (string)habitacionXML.Element(EtqFechaRenovacion);
                            }
                            else if (elem.Name == EtqComodidades)
                            {
                                comodidades = (string)habitacionXML.Element(EtqComodidades);
                            }

                        }
                        toret.Add(new Habitacion(tipo, fechaReserva, identificador, fechaRenovacion, comodidades));
                    }
                }
            }
            catch (XmlException)
            {
                toret.Clear();
            }
            return toret;
        }
        
        public Habitacion getHabitacion(int identificador)
        {
            foreach (Habitacion c in this.habitaciones)
            {
				if (c.Identificador == identificador)
                {
                    return c;
                }
            }

            return null;
        }


        public bool Remove(Habitacion habitacion)
        {
            return this.habitaciones.Remove(habitacion);
        }

        public void RemoveAt(int i)
        {
            this.habitaciones.RemoveAt(i);
        }

        public void Clear()
        {
            this.habitaciones.Clear();
        }

        public int Count
        {
            get { return this.habitaciones.Count; }
        }

        public bool IsReadOnly => throw new NotImplementedException();

        // Indizador de las reservas
        public Habitacion this[int i]
        {
            get { return this.habitaciones[i]; }
            set { this.habitaciones[i] = value; }
        }

        //Enumerar con tipo
        public IEnumerator<Habitacion> GetEnumerator()
        {
            foreach (var r in this.habitaciones)
            {
                yield return r;
            }
        }

        //Enumerar sin tipo
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Habitacion item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Habitacion[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        private List<Habitacion> habitaciones;
    }
}
