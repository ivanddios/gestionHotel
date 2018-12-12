using System;
using System.Text;
using ReservasHotel;
using System.Globalization;
using Habitaciones.Core;

/// <summary>
///  La clase representa una reserva hecha por un cliente
///  ///  @author Patricia Martin Perez
/// </summary>
public class Reserva
{
    // Attributes
	public const double Iva = 0.21;      


    // Associations

    /// <summary>
    /// Se crea la clase con los atributos indicados y se crea un identificador a partir de ellos
    /// </summary>
    public Reserva(Habitacion h, Cliente c, DateTime fEntrada, DateTime fSalida, bool g,
                                double tarifa)
    {
        this.Habitacion = h;
		this.Tipo = h.Tipo;
        this.Cliente = c;
        this.FechaEntrada = fEntrada;
        this.FechaSalida = fSalida;
        this.UsaGaraje = g;
		this.TarifaDia = tarifa;
        this.IdReserva = crearIdentificador();

    }

    //Propiedades
    public Habitacion Habitacion
    {
        get; private set;
    }

	public string Tipo
    {
        get; private set;
    }

    public DateTime FechaEntrada
    {
        get; private set;
    }

    public DateTime FechaSalida
    {
        get; private set;
    }

    public Boolean UsaGaraje
    {
        get; private set;
    }

    public string IdReserva
    {
        get; private set;
    }

	public double TarifaDia
    {
        get; private set;
    }

	public Cliente Cliente
    {
        get; private set;
    }


    // Operations

    /// <summary>
    ///  Calcula el precio total de la reserva utilizando el numero de dias
    ///  de estancia en el hotel, la tarifa y el iva.
    /// </summary>
    /// <returns> El precio total como double
    /// </returns>
    public double calcularTotal()
    {

        double precio = calcularNumDias() * this.TarifaDia;
		precio += precio * Iva;
        return precio;
    }

    /// <summary>
    ///  Calcula el numero de dias entre la fecha de entrada y la de salida
    /// </summary>
    /// <returns> Devuelve un entero del numero de días totales entre las fechas
    /// </returns>
    private int calcularNumDias()
    {
    
        return (this.FechaSalida - this.FechaEntrada).Days;


    }

    /// <summary>
    /// string con la informacion de la reserva
    /// </summary>
    /// <returns>  Devuelve un string con la informacion de la reserva
    /// </returns>

    public override string ToString()
    {
        var toret = new StringBuilder();
        toret.Append("Reserva: ");
        toret.AppendLine(this.IdReserva);
		toret.Append("Tipo: ");
		toret.AppendLine(this.Tipo);
		toret.Append(this.FechaEntrada.ToString("yyyy/MM/dd HH:mm:ss"));
        toret.Append(" - ");
		toret.AppendLine(this.FechaSalida.ToString("yyyy/MM/dd HH:mm:ss"));
        toret.AppendLine(Cliente.ToString());
        toret.Append("Utiliza el garaje: ");
		toret.AppendLine(this.UsaGaraje.ToString());
        toret.Append(this.TarifaDia.ToString());
        toret.Append(" €/dia - iva: ");
		toret.AppendLine(Iva.ToString());
        toret.Append("Precio total: ");
        toret.AppendLine(calcularTotal().ToString());


        return toret.ToString();

    }

    /// <summary>
    /// Genera la factura de la reserva
    /// </summary>
    /// <returns>  Devuelve un string con la informacion de la factura
    /// </returns>
    public string GenerarFactura()
    {
        var toret = new StringBuilder();
        toret.AppendLine("------------------------------------------------------");
        toret.Append("Factura: ");
        toret.AppendLine(this.IdReserva);
        toret.Append(this.FechaEntrada.ToString("yyyy/MM/dd HH:mm:ss"));
        toret.Append(" - ");
        toret.AppendLine(this.FechaSalida.ToString("yyyy/MM/dd HH:mm:ss"));
        toret.AppendLine(Cliente.ToString());
        toret.Append(this.TarifaDia.ToString());
        toret.Append(" €/dia - iva: ");
        toret.AppendLine(Iva.ToString());
        toret.Append("Precio total: ");
        toret.AppendLine(calcularTotal().ToString());
		toret.AppendLine("------------------------------------------------------");


        return toret.ToString();

    }
    
    /// <summary>
    ///  Crea un identificardor propio de una reserva
    /// </summary>
    /// <returns> Devuelve un string del identificador generado
    /// </returns>
    private string crearIdentificador()
    {
		
		return this.FechaEntrada.Year.ToString() + this.FechaEntrada.Month.ToString() +
			       this.FechaEntrada.Day.ToString() + this.Habitacion.Identificador;

    }
} /* end class Reserva */