//Cambiar boolean-bool, Date-DateTime

using System;
using System.Text;
using ReservasHotel;

/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
///  @author your_name_here
/// </summary>
public class Reserva {
    // Attributes

    private string idReserva;

    private Habitacion habitacion;

    private Cliente cliente;

    private DateTime fechaEntrada;

    private DateTime fechaSalida;

    private bool usaGaraje;

    private double tarifaDia;

    private int iva;



    // Associations

    /// <summary> 
    /// </summary>

    public Reserva(Habitacion h, Cliente c, DateTime fEntrada, DateTime fSalida, bool g,
                                double tarifa, int iva) {
        this.habitacion = h;
        this.cliente = c;
        this.FechaEntrada = fEntrada;
        this.FechaSalida = fSalida;
        this.UsaGaraje = g;
        this.tarifaDia = tarifa;
        this.iva = iva;
        this.IdReserva = crearIdentificador();

    }


    public DateTime FechaEntrada {
        get; private set;
    }

    public DateTime FechaSalida {
        get; private set;
    }

    public Boolean UsaGaraje {
        get; private set;
    }

    public string IdReserva {
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
    public double calcularTotal() {

        double precio = calcularNumDias() * this.tarifaDia;
        precio += precio * this.iva;
        return precio;
    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
    /// </returns>
    private int calcularNumDias() {
        return (this.fechaSalida - this.fechaEntrada).Days;

    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
    /// </returns>
    public string toString() {
        var toret = new StringBuilder();
        toret.AppendLine("Reserva: ");
        toret.Append(this.IdReserva);
        toret.AppendLine(this.FechaEntrada.ToString("yyyyMMddTHH:mm:ssZ"));
        toret.Append(" - ");
        toret.Append(this.FechaSalida.ToString("yyyyMMddTHH:mm:ssZ"));
        toret.AppendLine(cliente.ToString());
        toret.AppendLine("Utiliza el garaje: ");
        toret.Append(this.UsaGaraje);
        toret.AppendLine(this.tarifaDia.ToString());
        toret.Append(" €/dia - iva: ");
        toret.Append(this.iva);
        toret.AppendLine("Precio total: ");
        toret.Append(calcularTotal().ToString());


        return toret.ToString();

    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
    /// </returns>
    private string crearIdentificador() {

        return this.FechaEntrada.Year + this.FechaEntrada.Month +
            this.FechaEntrada.Day + this.habitacion.IdHabitacion;

    }
} /* end class Reserva */
