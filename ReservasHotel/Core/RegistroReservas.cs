using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

// contains devuelve bool, boolean-bool, meto dos de ICollection
// arhivo es string
//agregar variable archivo

/// <summary>
///  A class that represents ...
/// 
///  @see OtherClasses
///  @author Patricia Martin Perez
/// </summary>
public class RegistroReservas : ICollection<Reserva> {
    // Attributes

    private List<Reserva> reservas;

    // Associations

    //@element-type Reserva
    public Reserva myReserva;

    public int Count {
        get { return this.reservas.Count; }
    }

    public bool IsReadOnly {
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
    public void Add(Reserva r) {
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
    public bool Remove(Reserva r) {
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
    public bool Contains(Reserva r) {
        return this.reservas.Contains(r);

    }

    /// <summary>
    ///  An operation that does...
    /// 
    ///  @param firstParam a description of this parameter
    /// </summary>
    /// <returns>
    /// </returns>
    public override string ToString() {
        var toret = new StringBuilder();

        foreach (Reserva r in this.reservas) {
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
    public void GuardarXml(string archivo) {
        var doc = new XDocument();
        var root = new XElement("reservas");

        foreach (Reserva reserva in this.reservas) {
            root.Add(
                new XElement("reserva",
                             new XAttribute("id", reserva.IdReserva),
                             new XElement("fechaEntrada", reserva.FechaEntrada.ToString("yyyyMMddTHH:mm:ssZ")),
                             new XElement("fechaSalida", reserva.FechaSalida.ToString("yyyyMMddTHH:mm:ssZ")),
                             new XElement("garaje", reserva.UsaGaraje),
                             new XElement("tarifa", reserva.tar),
                            )

            );
        }

        doc.Add(root);
        doc.Save(nf);

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
    public RegistroReservas RecuperarXml(archivo) {
        // section -64--88-1-34--68184155:166dbec18c0:-8000:0000000000000BDE begin
        // section -64--88-1-34--68184155:166dbec18c0:-8000:0000000000000BDE end

    }

    public void Clear() {
        throw new NotImplementedException();
    }

    bool ICollection<Reserva>.Contains(Reserva item) {
        throw new NotImplementedException();
    }

    public void CopyTo(Reserva[] array, int arrayIndex) {
        throw new NotImplementedException();
    }

    bool ICollection<Reserva>.Remove(Reserva item) {
        throw new NotImplementedException();
    }

    public IEnumerator<Reserva> GetEnumerator() {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        throw new NotImplementedException();
    }
} /* end class RegistroReservas */
