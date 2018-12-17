namespace GestionDeHoteles.XML
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.IO;
    using System.Diagnostics;

    public class XMLBrowser
    {
        private string nombreArchivoReservas;
        private string nombreArchivoClientes;
        private string nombreArchivoHabitaciones;
	    
        public const string RESERVA = "reserva";
        public const string ID_RESERVA = "id";

        public const string HABITACION = "habitacion";
        public const string ID_HABITACION = "identificador";
        public const string ID_ATT_HABITACION = "id";
        public const string COMODIDAD = "comodidades";

        public const string CLIENTE = "cliente";      
        public const string NOMBRE = "nombre";
        public const string DNI = "dni";

        private static int max = 0;
        private static int min = Int32.MaxValue;
        
        public XMLBrowser(string nombreArchivoReservas, string nombreArchivoHabitaciones, string nombreArchivoClientes){
            this.nombreArchivoReservas = nombreArchivoReservas;
            this.nombreArchivoClientes = nombreArchivoClientes;
            this.nombreArchivoHabitaciones = nombreArchivoHabitaciones;
        }
        
        public XMLBrowser(string nombreArchivoReservas, string nombreArchivoHabitaciones){
            this.nombreArchivoReservas = nombreArchivoReservas;
            this.nombreArchivoHabitaciones = nombreArchivoHabitaciones;
            this.nombreArchivoClientes = "";
        }

        /// <summary>
        /// Intentar no usar, nombres por defecto
        /// </summary>
        public XMLBrowser(){
            this.nombreArchivoReservas = "registro_reservas.xml";
            this.nombreArchivoClientes = "clientes.xml";
            this.nombreArchivoHabitaciones = "Habitaciones.xml";
        }

        public void setArchivoClientes(string nombre){
            this.nombreArchivoClientes = nombre;
        }

        public void setArchivoHabitaciones(string nombre){
            this.nombreArchivoHabitaciones = nombre;
        }

        public void setArchivoReservas(string nombre){
            this.nombreArchivoReservas = nombre;
        }

        public string getArchivoClientes(){
            return this.nombreArchivoClientes;
        }

        public string getArchivoHabitaciones(){
            return this.nombreArchivoHabitaciones;
        }

        public string getArchivoReservas(){
            return this.nombreArchivoReservas;
        }

        /// <summary>
        /// Devuelve una lista con los valores del mapa. Sirve para anhos
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="map">Map.</param>
        private List<int> toList(SortedDictionary<int, int> map){
            List<int> retorno = new List<int>();

            int valor;
            if(max > min){
                for (int i = min; i <= max; i++){
                    if(map.TryGetValue(i, out valor)){
                        retorno.Add(valor);
                    }else{
                        retorno.Add(0);
                    }
                }
            }else{            
                return new List<int>();
            }

            return retorno;
        }

        /// <summary>
        /// Pasa de una lsita a un mes. ES una modificacion del toList que maneja meses de 0 a 12
        /// </summary>
        /// <returns>The list mes.</returns>
        /// <param name="map">Map.</param>
        private List<int> toListMes(SortedDictionary<int, int> map){
            int max = 12;
            int min = 1;
            List<int> retorno = new List<int>();

            int valor;
            for (int i = min; i <= max; i++){
                if(map.TryGetValue(i, out valor)){
                    retorno.Add(valor);
                }else{
                    retorno.Add(0);
                }
            }

            return retorno;         
        }

        /// <summary>
        /// Devuelve el numero de anhos entre los que se mueven las reservas
        /// </summary>
        /// <returns>Nº de anhos.</returns>
        public int getNumAnhos(){
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA){
                        int anho = Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                        if(anho > max){
                            //max = anho;
                            max = 2018; //Por defecto segun el enunciado, cambiarlo al de arriba implica cambiar el grafico pasandole el año
                        }
                        if(anho < min){
                            min = anho;
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }
            
            if(max > min){
                return max - min + 1;
            }
            return 0;
        }

        /// <summary>
        /// Devuelve el numero de reservas por año, hasta como dice el enunciado año actual
        /// </summary>
        /// <returns>Numero de reservas</returns>
        public List<int> getNumReservas(){
            var map = new SortedDictionary<int, int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA){
                        int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                        int value = 0;
                        if(map.TryGetValue(anho, out value)){
                            map.Remove(anho);
                            map.Add(anho, ++value);
                        }else{
                            map.Add(anho, 1);
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            return toList(map);
        }

        /// <summary>
        /// Devuelve el numero de reservas por meses de un año
        /// </summary>
        /// <returns>Numero de reservas por meses.</returns>
        /// <param name="year">Año.</param>
        public List<int> getNumReservasPorMeses(int year){
            var map = new SortedDictionary<int, int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA){
                        int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                        if(anho == year){
                            int mes = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(4, 2));
                            int value = 0;
                            if(map.TryGetValue(mes, out value)){
                                map.Remove(mes);
                                map.Add(mes, ++value);
                            }else{
                                map.Add(mes, 1);
                            }
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            return toListMes(map);
        }

        /// <summary>
        /// Devuelve los clientes del hotel con reservas
        /// </summary>
        /// <returns>Los clientes.</returns>
        public List<string> getClientes(){
            List<string> retorno = new List<string>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA){
                        string cliente = x.Element(CLIENTE).Element(DNI).Value.ToString();
                        if(!retorno.Contains(cliente)){
                            retorno.Add(cliente);
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }         
            return retorno;
        }

        /// <summary>
        /// Devuelve los clientes del hotel. Solo funciona si se ha indicado el nombre del archivo de clientes
        /// </summary>
        /// <returns>Los clientes.</returns>
        public List<string> getClientesReales(){
            List<string> retorno = new List<string>();

            if(this.nombreArchivoClientes == null || this.nombreArchivoClientes.Equals("")){
                return getClientes();
            }

            try{
                XElement raiz = XElement.Load(nombreArchivoClientes);

                foreach (XElement x in raiz.Elements()){
                    if (x.Name.ToString() == CLIENTE) {
                        string cliente = x.Element(DNI).Value.ToString() + ":" + x.Element(NOMBRE).Value.ToString();
                        if (!retorno.Contains(cliente)){
                            retorno.Add(cliente);
                        }
                    }
                }
            }catch (Exception e){
                Console.WriteLine(e.ToString());
            }

            return retorno;
        }

        /// <summary>
        /// Devuelve el numero de reservas de un cliente por año
        /// </summary>
        /// <returns>Nº de reservas del cliente</returns>
        /// <param name="cliente">Cliente.</param>
        public List<int> getReservasClienteAnho(String cliente){
            var map = new SortedDictionary<int, int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA && x.Element(CLIENTE).Element(DNI).Value.ToString().Equals(cliente)){
                        int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                        int value = 0;
                        if(map.TryGetValue(anho, out value)){
                            map.Remove(anho);
                            map.Add(anho, ++value);
                        }else{
                            map.Add(anho, 1);
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            return toList(map);
        }
        
        /// <summary>
        /// Devuelve el numero de reservas de un cliente en un año por meses
        /// </summary>
        /// <returns>Reservas del cliente este año por meses</returns>
        /// <param name="cliente">Cliente.</param>
        /// <param name="year">Año.</param>
        public List<int> getReservasClientePorMeses(String cliente, int year){
            var map = new SortedDictionary<int, int>();
            
            try{
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA && x.Element(CLIENTE).Element(DNI).Value.ToString().Equals(cliente)){
                        int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                        if(anho == year){
                            int mes = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(4, 2));
                            int value = 0;
                            if(map.TryGetValue(mes, out value)){
                                map.Remove(mes);
                                map.Add(mes, ++value);
                            }else{
                                map.Add(mes, 1);
                            }
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            return toListMes(map);
        }
        
        /// <summary>
        /// Devuelve todas las habitaciones que tienen reserva
        /// </summary>
        /// <returns>Tipos de habitaciones</returns>
        public List<int> getHabitaciones(){
            List<int> retorno = new List<int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA){
                        int habitacion = Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(8));
                        if(!retorno.Contains(habitacion)){
                            retorno.Add(habitacion);
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            retorno.Sort();
            return retorno;
        }

        /// <summary>
        /// Devuelve numero de reservas de una habitacion
        /// </summary>
        /// <returns>Nº de reservas de una habitacion</returns>
        /// <param name="idHabitacion">Id habitacion</param>
        public List<int> getReservasHabitacionAnho(int idHabitacion){
            var map = new SortedDictionary<int, int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA && Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(8)).Equals(idHabitacion)){
                        int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                        int value = 0;
                        if(map.TryGetValue(anho, out value)){
                            map.Remove(anho);
                            map.Add(anho, ++value);
                        }else{
                            map.Add(anho, 1);
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            return toList(map);
        }

        /// <summary>
        /// Devuelve el numero de reservas de una habitacion por meses
        /// </summary>
        /// <returns>Nº de reservas de la habitacion por emses</returns>
        /// <param name="idHabitacion">Id habitacion</param>      
        public List<int> getReservasHabitacionPorMeses(int idHabitacion, int year){
            var map = new SortedDictionary<int, int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA && Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(8)).Equals(idHabitacion)){
                        int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                        if(anho == year){
                            int mes = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(4, 2));
                            int value = 0;
                            if(map.TryGetValue(mes, out value)){
                                map.Remove(mes);
                                map.Add(mes, ++value);
                            }else{
                                map.Add(mes, 1);
                            }
                        }
                    }
                }
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            return toListMes(map);
        }

        /// <summary>
        /// Devuelve las comodidades que hay
        /// </summary>
        /// <returns>Las comodidades</returns>
        public List<string> getComodidades(){
            List<string> retorno = new List<string>();

            try{            
                XElement raiz = XElement.Load(nombreArchivoHabitaciones);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == HABITACION){
                        try{
                            string[] comodidades = x.Element(COMODIDAD).Value.ToString().Split(',');
                            foreach(string comodidad in comodidades){
                                if(!retorno.Contains(comodidad.Trim())){
                                    retorno.Add(comodidad.Trim());
                                }
                            }
                        }catch(Exception e){
                            Console.WriteLine(e.ToString());
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            retorno.ForEach((string obj) => Console.WriteLine(obj));
            return retorno;
        }   

        /// <summary>
        /// Devuelve el numero de reservas con una comodidad para todos los años
        /// </summary>
        /// <returns>Nº de comoidaddes por año</returns>
        /// <param name="comodidad">Comodidad.</param>
        public List<int> getNumComodidadesAnho(String comodidad){
            var map = new SortedDictionary<int, int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA){
                        int habitacion = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(8));
                        Console.WriteLine();
                        if(getComodidadesDe(habitacion).Contains(comodidad.ToLower())){
                            int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                            int value = 0;
                            if(map.TryGetValue(anho, out value)){
                                map.Remove(anho);
                                map.Add(anho, ++value);
                            }else{
                                map.Add(anho, 1);
                            } 
                        }                 
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }       

            return toList(map);
        }   

        /// <summary>
        /// Devuelve el numero de reservas con una comodidad por meses para un año
        /// </summary>
        /// <returns>Nº de comoidaddes por mes</returns>
        /// <param name="comodidad">Comodidad.</param>      
        public List<int> getNumComodidadesPorMeses(String comodidad, int year){
            var map = new SortedDictionary<int, int>();
            
            try{            
                XElement raiz = XElement.Load(nombreArchivoReservas);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == RESERVA){
                        int habitacion = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(8));
                        if(getComodidadesDe(habitacion).Contains(comodidad.ToLower())){
                            int anho = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(0, 4));
                            if(anho == year){
                                int mes = System.Convert.ToInt32(x.Attribute(ID_RESERVA).Value.ToString().Substring(4, 2));
                                int value = 0;
                                if(map.TryGetValue(mes, out value)){
                                    map.Remove(mes);
                                    map.Add(mes, ++value);
                                }else{
                                    map.Add(mes, 1);
                                }
                            }
                        }                 
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }   

            return toListMes(map);
        }

        /// <summary>
        /// Devuelve la comodidad de una habitacion
        /// </summary>
        /// <returns>String de la comodidad</returns>
        /// <param name="idHabitacion">ID habitacion.</param>
        public List<string> getComodidadesDe(int idHabitacion){
            List<string> retorno = new List<string>();

            try{            
                XElement raiz = XElement.Load(nombreArchivoHabitaciones);
            
                foreach (XElement x in raiz.Elements()){
                    if(x.Name.ToString() == HABITACION && x.Element(ID_HABITACION).Value.ToString().Equals(Convert.ToString(idHabitacion))){
                        try{
                            string[] comodidades = x.Element(COMODIDAD).Value.ToString().Split(',');
                            foreach(string comodidad in comodidades){
                                if(!retorno.Contains(comodidad.Trim().ToLower())){
                                    retorno.Add(comodidad.Trim().ToLower());
                                }
                            }
                        }catch(Exception e){
                            Console.WriteLine(e.ToString());
                        }
                    }
                }            
            }catch(Exception e){
                Console.WriteLine(e.ToString());
            }

            return retorno;
        }
    }
}
