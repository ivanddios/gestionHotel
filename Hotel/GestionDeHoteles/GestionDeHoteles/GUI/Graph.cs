namespace GestionDeHoteles.GUI
{
	using System;
    using System.Collections.Generic;
	using Cairo;

	public class Graph : Gtk.DrawingArea
	{
        /// <summary>
        /// Crea un gráfico de una lista de objetos (resultado de una búsqueda) por años o por meses.
        /// </summary>
        /// <param name="width">Ancho del gráfico</param>
        /// <param name="height">Alto del gráfico</param>
        /// <param name="lista">Lista de objetos a mostrar (eje X)</param>
		/// <param name="PorMeses">True si es por meses. False si es por años</param>
		public Graph(int width, int height, List<int> lista, bool PorMeses){
			this.lista = new List<int>();
            lista.ForEach((obj) => this.lista.Add(obj)); //"Clonar" la lista
            this.Width = width;
			this.Height = height;
			this.PorMeses = PorMeses;
            this.NumLineas = lista.Count;
			this.Margin = new Rectangle(width * 0.1, height * 0.1, width * 0.9, height * 0.9);


			this.SetSizeRequest(width, height);
			this.ExposeEvent += (o, args) => this.OnExposeDrawingArea();
        }

		/// <summary>
		/// Crea un gráfico de una lista de objetos (resultado de una búsqueda) por años.
        /// </summary>
        /// <param name="width">Ancho del gráfico</param>
        /// <param name="height">Alto del gráfico</param>
		/// <param name="lista">Lista de objetos a mostrar (eje X)</param>
		public Graph(int width, int height, List<int> lista){
            this.lista = new List<int>();
			lista.ForEach((obj) => this.lista.Add(obj)); //"Clonar" la lista
			this.Width = width;
			this.Height = height;
			this.PorMeses = false;
            this.NumLineas = lista.Count;
            this.Margin = new Rectangle(width * 0.1, height * 0.1, width * 0.9, height * 0.9);

            this.SetSizeRequest(width, height);
            this.ExposeEvent += (o, args) => this.OnExposeDrawingArea();
		}

        /// <summary>
        /// Crea un gráfico sin lista de objetos. No se puede pintar hasta que se pase una lista. Gráfica creada por años
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
		public Graph(int width, int height){
			this.lista = new List<int>();
            this.Width = width;
            this.Height = height;
			this.PorMeses = false;
			this.NumLineas = 2;
            this.Margin = new Rectangle(width * 0.1, height * 0.1, width * 0.9, height * 0.9);

            this.SetSizeRequest(width, height);
            this.ExposeEvent += (o, args) => this.OnExposeDrawingArea();
        }

        /// <summary>
        /// Dibuja el gráfico
        /// </summary>
        private void OnExposeDrawingArea()
		{
			using (var canvas = Gdk.CairoHelper.Create(this.GdkWindow)) {
                this.DrawGrid(canvas);
				this.DrawAxis(canvas);
				this.DrawLegend(canvas);
				this.DrawData(canvas);

                // Clean
                canvas.GetTarget().Dispose();
            }
        }

        /// <summary>
		/// Dibuja una linea desde un punto a otro definidos por <see cref="Gdk.Point"/>
        /// </summary>
        /// <param name="canvas">Canvas en donde dibujar.</param>
        /// <param name="p1">Punto base</param>
        /// <param name="p2">Punto destino</param>
		private void DrawLine(Cairo.Context canvas, Gdk.Point p1, Gdk.Point p2){
			this.DrawLine(canvas, p1.X, p1.Y, p2.X, p2.Y);
		}

        /// <summary>
        /// Dibuja una linea desde un punto a otro definidos por 2 ints cada uno
        /// </summary>
        /// <param name="canvas">Canvas en donde dibujar.</param>
        /// <param name="x1">Coordenada x punto base</param>
		/// <param name="y1">Coordenada y punto base</param>
		/// <param name="x2">Coordenada x punto destino</param>
		/// <param name="y2">Coordenada y punto destino</param>
		private void DrawLine(Cairo.Context canvas, double x1, double y1, double x2, double y2){
			canvas.MoveTo(x1, y1);
			canvas.LineTo(x2, y2);
        }

        /// <summary>
		/// Escribe el string <paramref name="text"/> en un punto definido por Gdk.Point
        /// </summary>
        /// <param name="canvas">Canvas.</param>
        /// <param name="pos">Punto.</param>
        /// <param name="text">Texto.</param>
		private void DrawString(Cairo.Context canvas, Gdk.Point pos, string text){
            this.DrawString(canvas, pos.X, pos.Y, text);
		}

        /// <summary>
        /// Escribe el string <paramref name="text"/> en un punto definido por dos ints
        /// </summary>
		/// <param name="canvas">Canvas.</param>
        /// <param name="x">Coordenada x punto</param>
        /// <param name="y">Coordenada y punto</param>
        /// <param name="text">Texto.</param>
		private void DrawString(Cairo.Context canvas, double x, double y, string text){
            canvas.MoveTo(x, y);
            canvas.ShowText(text);
        }

        /// <summary>
        /// Actualizar la lista, sin crear un nuevo gráfico. No clona la lista
        /// </summary>
        /// <value>The width.</value>
		public void setLista(List<Object> lista){
			this.lista = new List<int>();
            lista.ForEach((obj) => this.lista.Add((int) obj)); //"Clonar" la lista
			this.NumLineas = lista.Count;
			this.OnExposeDrawingArea();
		}

        /// <summary>
		/// Dibuja el Grid
        /// </summary>
        /// <param name="canvas">Canvas.</param>
		private void DrawGrid(Cairo.Context canvas){
			double maxHeight = Margin.Height;
			double maxWidth = Margin.Width;
			double yGap = (maxHeight - Margin.Y) / (NumLineas - 1);
			double xGap = (maxWidth - Margin.X) / (NumLineas - 1);
			double x = Margin.X;
			double y = Margin.Y;

            canvas.LineWidth = 0.25;
			canvas.SetSourceRGB(0,0,0);

            // Draw horizontal lines
            while (y < maxHeight) {
				canvas.MoveTo(Margin.X, y);
				canvas.LineTo(maxWidth, y);

                y += yGap;
            }

            // Draw vertical lines
            while (x < maxWidth + 1){
				canvas.MoveTo(x, Margin.Y);
				canvas.LineTo(x, maxHeight);

                x += xGap;
            }         
			canvas.Stroke();
		}

        /// <summary>
        /// Dibuja los ejes del gráfico x e y
        /// </summary>
        /// <param name="canvas">Canvas</param>
		private void DrawAxis(Cairo.Context canvas){
			canvas.LineWidth = 2;
			canvas.SetSourceRGB(0,0,0);

			canvas.MoveTo(Margin.X, Margin.Y);
			canvas.LineTo(Margin.X, Margin.Height);
			canvas.MoveTo(Margin.X, Margin.Height);
			canvas.LineTo(Margin.Width, Margin.Height);

			canvas.Stroke();
		}
        
        /// <summary>
        /// Dibuja los nombres de los meses si es un gráfico por meses o los años si es por años
		/// También dibuja el rango de valores izquierdo
        /// </summary>
        /// <param name="canvas">Canvas.</param>
		private void DrawLegend(Cairo.Context canvas){
			canvas.SetFontSize(this.LegendsFontSize); //Hay que retocar esto
			canvas.SetSourceRGB(0, 0, 0);

            double maxHeight = Margin.Height;
            double maxWidth = Margin.Width;
            double yGap = (maxHeight - Margin.Y) / (NumLineas - 1);
            double xGap = (maxWidth - Margin.X) / (NumLineas - 1);
            double x = Margin.X;
            double y = Margin.Y;
            

            canvas.LineWidth = 0.25;
            canvas.SetSourceRGB(0,0,0);

			int max = 0;         
            int i = 0;

            this.lista.ForEach((obj) => max = System.Math.Max(max, obj));
            // Draw horizontal lines
			while (y < maxHeight + 1) {
				//Arreglar este %
				if(i % 4 == 0){
                    this.DrawString(canvas, Width * 0.05, y, Convert.ToString(max - (i++ * max / NumLineas)));
				}

                y += yGap;
				if(y >= maxHeight + 1){
                    this.DrawString(canvas, Width * 0.05, y, "0");
				}
            }

			if(!this.PorMeses){
				i = 2018 - this.lista.Count + 1;
			}else{
				i = 0;
			}
            // Draw vertical lines
			while (x < maxWidth + 1){
				if(this.PorMeses){
					this.DrawString(canvas, x - this.LegendsFontSize, Height * 0.95, getMesString(i++));
				}else{
					this.DrawString(canvas, x - this.LegendsFontSize, Height * 0.95, Convert.ToString(i++));
                }    

                x += xGap;
            }         
			this.DrawString(canvas, Margin.X - this.LegendsFontSize*1.5, Margin.Y - this.LegendsFontSize, "Cantidad");
			this.DrawString(canvas, Margin.Width/2 - this.LegendsFontSize, Margin.Height + 5*this.LegendsFontSize, this.Titulo);

			canvas.Stroke();
		}

        /// <summary>
        /// Dibuja los datos en la tabla. Grafico de lineas
        /// </summary>
        /// <param name="canvas">Canvas.</param>
		private void DrawData(Cairo.Context canvas){
			if(this.lista.Count >= 2){
				List<int> listaNormalizada = this.NormalizeData(this.lista);

				canvas.MoveTo(Margin.X, (Margin.Height) - listaNormalizada[0] + (Margin.Y));
                canvas.SetSourceRGB(255, 0, 0);
                canvas.LineWidth = 1;

                double maxWidth = Margin.Width;
				double xGap = (maxWidth - Margin.X) / (listaNormalizada.Count - 1);
                double x = Margin.X;

                for (int i = 0; i < listaNormalizada.Count; i++){
                    canvas.LineTo(x, (Margin.Height) - listaNormalizada[i] + (Margin.Y));
                    x += xGap;
                }
                canvas.Stroke();
			}
		}

        /// <summary>
        /// Normaliza los datos según el tamaño del gráfico
        /// </summary>
        /// <returns>Datos normalizados</returns>
		/// <param name="listaDatos">Lista de datos</param>
		private List<int> NormalizeData(List<int> listaDatos){
			double alturaGrafico = Margin.Height - Margin.Y;
            List<int> retorno = new List<int>();
			int max = 1;

			listaDatos.ForEach((obj) => max = System.Math.Max(max, obj));
            
			listaDatos.ForEach((obj) => retorno.Add((int) (((alturaGrafico * (int) obj) / max) + Margin.Y)));

			return retorno;
		}

		public string Titulo{
			get{
				if(strTitulo == null || strTitulo.Equals("")){
					return "Gráfico";
				}else{
					return this.strTitulo;
				}
			} 
			set{
				this.strTitulo = value;
			}
		}

		private string strTitulo;

        /// <summary>
        /// Ancho del gráfico
        /// </summary>
        /// <value>The width.</value>
        public int Width{
            get; private set;
		}

        /// <summary>
        /// Alto del gráfico
        /// </summary>
        /// <value>The height.</value>
        public int Height{
            get; private set;
        }

        /// <summary>
        /// Rectangulo en el que se va a pintar el gráfico, dentro de Width y Height
        /// </summary>
        /// <value>El margen</value>
		public Rectangle Margin{
			get; private set;
		}

        /// <summary>
        /// Numero de lineas del grid
        /// </summary>
        /// <value>Numero de lineas.</value>
		public int NumLineas{
			get{
				return this.numLineas;
			} 
			private set{
				if(value == 0){
					this.numLineas = 1;
				}else{
					numLineas = value;
				}
			}
		}
		private int numLineas;

        /// <summary>
		/// Tamaño de las leyendas del gráfico (meses/años y cantidad).
		/// Referirse a FontSize para tamaño de concretaciones
        /// </summary>
        /// <value>The size of the legends font.</value>
		public int LegendsFontSize{
			get{
				return 14; //Habría que hacerlo propoicional al tamaño del gráfico pero buena suerte
			}
		}

        /// <summary>
        /// Tamaño de letra para cosas como el año o el nombre del mes, el numero por cantidad a la izquierda y tal
        /// </summary>
        /// <value>The size of the font.</value>
		public int FontSize{
			get{
				return System.Convert.ToInt32(LegendsFontSize*0.6); //Lo mismo que arriba
			}
		}

		/// <summary>
		/// Lista a representar en el gráfico
		/// </summary>
		private List<int> lista;

        /// <summary>
        /// Indica si es un gráfico por meses o por años
        /// </summary>
		private bool PorMeses{
			get{
				return this.porMeses;
			}
			set{
				if(this.lista.Count != 12 && value == true){
					while(this.lista.Count < 12){
						this.lista.Add(0);
					}
					this.porMeses = value;
				}else{
					this.porMeses = value;
				}
			}
		}

		private bool porMeses;

        private static string[] meses = {   "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre",
                                            "Noviembre", "Diciembre"};

        private string getMesString(int valor){
            try{
                return meses[valor];
            }catch (Exception){
                return "Mes";
            }
        }
    }
}
