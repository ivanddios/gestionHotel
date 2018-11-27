//Cambiar boolean-bool, Date-DateTime

using System;
using System.Text;
using ReservasHotel;
using System.Globalization;

/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
///  @author your_name_here
/// </summary>
public class Reserva
{
    // Attributes
	public const double Iva = 0.21; 

    private Habitacion habitacion;   


    // Associations

    /// <summary> 
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
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
    /// </returns>
    public double calcularTotal()
    {

        double precio = calcularNumDias() * this.TarifaDia;
		precio += precio * Iva;
        return precio;
    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
    /// </returns>
    private int calcularNumDias()
    {
    
        return (this.FechaSalida - this.FechaEntrada).Days;


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
		toret.AppendLine("------------------------------------------------------");
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
		toret.AppendLine("------------------------------------------------------");


        return toret.ToString();

    }


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
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
    /// </returns>
    private string crearIdentificador()
    {
		
		return this.FechaEntrada.Year.ToString() + this.FechaEntrada.Month.ToString() +
			       this.FechaEntrada.Day.ToString() + this.Habitacion.IdHabitacion;

    }
} /* end class Reserva */