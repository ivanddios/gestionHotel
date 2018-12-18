using BusquedasHotel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace BusquedasHotel.Core
{
    public class RegistroClientes
    {
        public const string ArchivoXML = "clientes.xml";
        public const string EtqUsuarios = "clientes";
        public const string EtqUsuario = "cliente";
        public const string EtqNombre = "nombre";
        public const string EtqDni = "dni";
        public const string EtqTelefono = "telefono";
        public const string EtqEmail = "email";
        public const string EtqDireccion = "direccion";

        public RegistroClientes()
        {
            this.usuarios = new List<Cliente>();
        }

        public List<Cliente> List
        {
            get { return this.usuarios; }
        }

        public void Add(Cliente usuario)
        {
            this.usuarios.Add(usuario);
        }

        public void GuardaXml()
        {
            this.GuardarXML(ArchivoXML);
        }

       


        public void GuardarXML(string nf)
        {
            var doc = new XDocument();
            var root = new XElement(EtqUsuarios);

            foreach (Cliente usuarios in this.usuarios)
            {
                var usuario = new XElement(EtqUsuario);
                var nombre = new XElement(EtqNombre, usuarios.Nombre);
                usuario.Add(nombre);
                var dni = new XElement(EtqDni, usuarios.Dni);
                usuario.Add(dni);
                var telefono = new XElement(EtqTelefono, usuarios.Telefono);
                usuario.Add(telefono);
                var email = new XElement(EtqEmail, usuarios.Email);
                usuario.Add(email);
                var direccion = new XElement(EtqDireccion, usuarios.Direccion);
                usuario.Add(direccion);

                root.Add(usuario);
            }
            doc.Add(root);
            doc.Save(nf);
        }

        public static RegistroClientes RecuperaXml()
        {
            var toret = new RegistroClientes();

            try
            {
                var doc = XDocument.Load(@"C:\Users\ivand\OneDrive\Escritorio\gestionHotel\Hotel\Clientes\Gestión Hotel\bin\Debug\clientes.xml");

                if (doc.Root != null && doc.Root.Name == EtqUsuarios)
                {
                    var usuarios = doc.Root.Elements(EtqUsuario);

                    foreach (XElement usuarioXML in usuarios)
                    {
                        //System.Console.WriteLine("*");

                        var elements = usuarioXML.Elements();
                        string nombre = "", dni = "", email = "", direccion = "";
                        int telefono = 0;

                        foreach (XElement elem in elements)
                        {
                            if (elem.Name == EtqNombre)
                            {
                                nombre = (string)usuarioXML.Element(EtqNombre);
                            }
                            else if (elem.Name == EtqDni)
                            {
                                dni = (string)usuarioXML.Element(EtqDni);
                            }
                            else if (elem.Name == EtqTelefono)
                            {
                                telefono = (int)usuarioXML.Element(EtqTelefono);
                            }
                            else if (elem.Name == EtqEmail)
                            {
                                email = (string)usuarioXML.Element(EtqEmail);
                            }
                            else if (elem.Name == EtqDireccion)
                            {
                                direccion = (string)usuarioXML.Element(EtqDireccion);
                            }

                        }
                        toret.Add(new Cliente(nombre, dni, telefono, email, direccion));
                    }
                }
            }
            catch (XmlException)
            {
                toret.Clear();
            }
            return toret;
        }



        public Cliente getUsuario(string dni)
        {
            foreach (Cliente c in this.usuarios)
            {
                if (c.Dni == dni)
                {
                    return c;
                }
            }

            return null;
        }

        public List<string> getDNIs()
        {
            List<string> dnisXML = new List<string>();
            foreach (Cliente c in this.usuarios)
            {
                dnisXML.Add(c.Dni);
            }
            return dnisXML;
        }


        public List<string> getEmails()
        {
            List<string> emailsXML = new List<string>();
            foreach (Cliente c in this.usuarios)
            {
                emailsXML.Add(c.Email);
            }
            return emailsXML;
        }


        public bool Remove(Cliente usuario)
        {
            return this.usuarios.Remove(usuario);
        }

        public void Clear()
        {
            this.usuarios.Clear();
        }

        public int Count
        {
            get { return this.usuarios.Count; }
        }

        // Indizador de los clientes
        public Cliente this[int i]
        {
            get { return this.usuarios[i]; }
            set { this.usuarios[i] = value; }
        }

        private List<Cliente> usuarios;
    }
}

