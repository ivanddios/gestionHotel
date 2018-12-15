
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ReservasHotel;
using Habitaciones.Core;
using Gestión_Hotel.Core;
using Gestión_Hotel.XML;

/// <summary>
///  Una clase que representa una coleccion de objetos de tipo reserva
///  @author Patricia Martin Perez
/// </summary>
public class RegistroReservas : ICollection<Reserva>
{
    // Attributes

    private List<Reserva> reservas;

    // Associations

    //@element-type Reserva
    public Reserva myReserva;

	public RegistroReservas(){
		this.reservas = new List<Reserva>();
	}

    public int Count
    {
        get { return this.reservas.Count; }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    // Operations

    /// <summary>
    ///  Agrega una reserva a la coleccion.
    /// </summary>
    /// <param name="r"> La reserva a agregar.
    /// </param>
    public void Add(Reserva r)
    {
        this.reservas.Add(r);

    }

    /// <summary>
    /// Elimina una reserva dada.
    /// </summary>
    /// <returns>True si se ha eliminado, False en otro caso.</returns>
    /// <param>La reserva eliminar.</param>
    public bool Remove(Reserva r)
    {
        return this.reservas.Remove(r);

    }

    /// <summary>
    /// Comprueba si la reserva dada se encuentra guardada.
    /// </summary>
    /// <returns>True si se encuentra, False en otro caso.</returns>
    /// <param>La reserva a comprobar.</param>
    public bool Contains(Reserva r)
    {
        return this.reservas.Contains(r);

    }

    // Indizador de las reservas
    public Reserva this[int i]
    {
        get { return this.reservas[i]; }
        set { this.reservas[i] = value; }
    }

    public Reserva getReserva(string idReserva)
    {
        foreach(Reserva r in this.reservas)
        {
            if(r.IdReserva == idReserva)
            {
                return r;
            }
        }

        return null;

    }

    /// <summary>
    /// Crea un string con la infromacion de una reserva.
    /// </summary>
    /// <returns> Devuelve un string de la reserva.
    /// </returns>
    public override string ToString()
    {
        var toret = new StringBuilder();

        foreach (Reserva r in this.reservas)
        {
            toret.AppendLine(r.ToString());
        }

        return toret.ToString();

    }

    /// <summary>
    /// Guarda la lista de las reservas como XML.
    /// <param>El nombre del archivo.</param>
    /// </summary>
    public static void GuardarXml(string archivo, RegistroReservas reservas)
    {
        var doc = new XDocument();
        var root = new XElement("reservas");

        foreach (Reserva reserva in reservas)
        {
            root.Add(
                new XElement("reserva",
                             new XAttribute("id", reserva.IdReserva),
                             new XElement("cliente",
                                                    new XElement("nombre", reserva.Cliente.Nombre),
                                                    new XElement("dni", reserva.Cliente.Dni),
                                                    new XElement("telefono", reserva.Cliente.Telefono),
                                                    new XElement("email", reserva.Cliente.Email),
                                                    new XElement("direccion", reserva.Cliente.Direccion)
                                         ),
                             new XElement("habitacion", 
                                          new XAttribute("id", reserva.Habitacion.Identificador),
				                          new XElement("tipo", reserva.Habitacion.Tipo),
                                          new XElement("fechaReserva", reserva.Habitacion.FechaReserva),
                                          new XElement("fechaRenovacion", reserva.Habitacion.FechaRenovacion),
                                          new XElement("comodidades", reserva.Habitacion.Comodidades)
                                          ),
				             new XElement("fechaEntrada", reserva.FechaEntrada.ToString("yyyy/MM/dd HH:mm:ss")),
				             new XElement("fechaSalida", reserva.FechaSalida.ToString("yyyy/MM/dd HH:mm:ss")),
                             new XElement("garaje", reserva.UsaGaraje),
				             new XElement("tarifa", reserva.TarifaDia, new XAttribute("iva", Reserva.Iva)),            
				             new XElement("total", reserva.calcularTotal())

                            )

				    );
        }

        doc.Add(root);
        doc.Save(archivo);

    }

 
    /// <summary>
    /// Recupera las reservas desde un archivo XML.
    /// </summary>
    /// <returns>Una coleccion con las reservas
    /// <param>El nombre del archivo.</param>
    public static RegistroReservas RecuperarXml(string archivo)
    {
		var toret = new RegistroReservas();
		var doc = XDocument.Load(archivo);
              
        var reservasElement = doc.Root.Elements("reserva");

		foreach (XElement reservaElement in reservasElement)
        {

			XElement clienteElement = reservaElement.Element("cliente");
			Cliente cliente = new Cliente(
                                        (string)clienteElement.Element("nombre"),
                                        (string)clienteElement.Element("dni"),
                                        (int)clienteElement.Element("telefono"),
                                        (string)clienteElement.Element("email"),
                                        (string)clienteElement.Element("direccion")
                                        );
            

			XElement habitacionElement = reservaElement.Element("habitacion");
			Habitacion habitacion = new Habitacion((string)habitacionElement.Element("tipo"),
                                            (string)habitacionElement.Element("fechaReserva"),
                                            (int)habitacionElement.Attribute("id"), 
                                            (string)habitacionElement.Element("fechaRenovacion"),
                                            (string)habitacionElement.Element("comodidades"));
             
			var reserva = new Reserva(habitacion, cliente, (DateTime)reservaElement.Element("fechaEntrada"), 
			                          (DateTime) reservaElement.Element("fechaSalida"), 
			                          (bool)reservaElement.Element("garaje"), (double)reservaElement.Element("tarifa"));
            
            
            
            toret.Add(reserva);
			Console.WriteLine(reserva);


        }

		return toret;
    }

    /// <summary>
    /// Elimina todos las reservas almacenadas.
    /// </summary>
    public void Clear()
    {
        this.reservas.Clear();
    }

    /// <summary>
    /// Comprueba si la reserva dada se encuentra guardada.
    /// </summary>
    /// <returns>True si se encuentra, False en otro caso.</returns>
    /// <param>La reserva a comprobar.</param>
    bool ICollection<Reserva>.Contains(Reserva r)
    {
        return this.reservas.Contains(r);
    }

    /// <summary>
    /// Copia las reservas a partir de la posicion dada a un vector.
    /// </summary>
    /// <param name="r">El vector al que copiar las reservas.</param>
    /// <param name="i">La posisicon desde la que copiar.</param>
    public void CopyTo(Reserva[] r, int i)
    {
        this.reservas.CopyTo(r, i);
    }

    /// <summary>
    /// Elimina una reserva dada.
    /// </summary>
    /// <returns>True si se ha eliminado, False en otro caso.</returns>
    /// <param>La reserva a eliminar.</param>
    bool ICollection<Reserva>.Remove(Reserva r)
    {
        return this.reservas.Remove(r);
    }

    //Enumerar con tipo
    public IEnumerator<Reserva> GetEnumerator()
    {
        foreach (var r in this.reservas)
        {
            yield return r;
        }
    }

    //Enumerar sin tipo
    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
} /* end class RegistroReservas */