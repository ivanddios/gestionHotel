
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using ReservasHotel;

// contains devuelve bool, boolean-bool, meto dos de ICollection
// arhivo es string
//agregar variable archivo

/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
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
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <param name="r">
    /// </param>
    /// <returns>
    /// </returns>
    public void Add(Reserva r)
    {
        this.reservas.Add(r);

    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <param name="r">
    /// </param>
    /// <returns>
    /// </returns>
    public bool Remove(Reserva r)
    {
        return this.reservas.Remove(r);

    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <param name="r">
    /// </param>
    /// <returns>
    /// </returns>
    public bool Contains(Reserva r)
    {
        return this.reservas.Contains(r);

    }
    
	public Reserva this[int i]
    {
        get { return this.reservas[i]; }
        set { this.reservas[i] = value; }
    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
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
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <param name="archivo">
    /// </param>
    /// <returns>
    /// </returns>
    public void GuardarXml(string archivo)
    {
        var doc = new XDocument();
        var root = new XElement("reservas");

        foreach (Reserva reserva in this.reservas)
        {
            root.Add(
                new XElement("reserva",
                             new XAttribute("id", reserva.IdReserva),
				             new XElement("cliente", new XElement("apellidos", reserva.Cliente.Apellidos),
				                          new XElement("nombre", reserva.Cliente.Nombre)),
				             new XElement("habitacion", new XAttribute("id", reserva.Habitacion.IdHabitacion),
				                          new XElement("tipo", reserva.Habitacion.Tipo)),
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
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <param name="archivo">
    /// </param>
    /// <returns>
    /// </returns>
    public RegistroReservas RecuperarXml(string archivo)
    {
		var toret = new RegistroReservas();
		var doc = XDocument.Load(archivo);
              
        var reservasElement = doc.Root.Elements("reserva");
		Habitacion hab = new Habitacion("122", "individual");

		foreach (XElement reservaElement in reservasElement)
        {

			XElement clienteElement = reservaElement.Element("cliente");
			Cliente cliente = new Cliente((string)clienteElement.Element("nombre"), (string)clienteElement.Element("apellidos"));

			XElement habitacionElement = reservaElement.Element("habitacion");
			Habitacion habitacion = new Habitacion((string)habitacionElement.Attribute("id"), (string)habitacionElement.Element("tipo"));

			var reserva = new Reserva(habitacion, cliente, (DateTime)reservaElement.Element("fechaEntrada"), 
			                          (DateTime) reservaElement.Element("fechaSalida"), 
			                          (bool)reservaElement.Element("garaje"), (double)reservaElement.Element("tarifa"));
            
            
            
            toret.Add(reserva);
			Console.WriteLine(reserva);


        }

		return toret;
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    bool ICollection<Reserva>.Contains(Reserva item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo(Reserva[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    bool ICollection<Reserva>.Remove(Reserva item)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<Reserva> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
} /* end class RegistroReservas */