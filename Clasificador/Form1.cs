using System.Security;

namespace Clasificador
{
    public partial class FRM_Clasificador : Form
    {
        private string ruta = "";

        public FRM_Clasificador()
        {
            InitializeComponent();
        }

        private void btnRuta_Click(object sender, EventArgs e)
        {

            openFileDialog.Filter = "csv files (*.csv)|*.csv|data files (*.data)|*.data|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //var sr = new StreamReader(openFileDialog.FileName);
                    ruta = openFileDialog.FileName;
                    
                    txtRuta.Text = ruta;

                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
                
            }


        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {

            /*
             * Se debe validar:
             *-Que la ruta no este en blanco
             *-Que el porcentaje de entrenamiento no este en blanco y que solo se ingresen numeros
             *-Que la columna de clase no este en blanco, que solo se ingresen numeros y que no se puedan poner numeros más grandes a la cantidad de columnas en la matriz
             *-Que el intervalo de discretización no este en blanco y que solo se ingresen numeros
            */

            if (txtRuta.Text.Trim().Length == 0)
            {
                MessageBox.Show("Introduzca una ruta.", "Error");

            } else if (txtColClase.Text.Trim().Length == 0) {
                MessageBox.Show("Introduzca una columna de clase.", "Error");

            } else if (txtPorcentajeEnt.Text.Trim().Length == 0) {
                MessageBox.Show("Introduzca un porcentaje de entrenamiento.", "Error");

            } else if (txtCategorias.Text.Trim().Length == 0) {
                MessageBox.Show("Introduzca un intervalo de discretización.", "Error");

            } else if (int.Parse(txtPorcentajeEnt.Text) <= 0 || int.Parse(txtPorcentajeEnt.Text) >= 100) {
                MessageBox.Show("El porcentaje de entrenamiento debe ser mayor a 0 y menor que 100.", "Error");

            }
            else if (int.Parse(txtCategorias.Text) <= 1)
            {
                MessageBox.Show("El intervalo de discretización debe ser mayor a 1.", "Error");

            }
            else
            {
                // Lectura de archivo y construcción de la matriz

                try
                {

                    string[] lineas = File.ReadAllLines(ruta);  // Se lee el archivo
                    int renglonesMatriz = 0;     // Se obtienen los renglones

                    if (lineas.Length < 50)
                    {
                        renglonesMatriz = lineas.Length;

                    }
                    else
                    {
                        renglonesMatriz = lineas.Length - 1;

                    }

                    lineas = Desordenar(lineas.ToList());

                    int columnasMatriz = lineas[0].Split(',').Length;       // Se obtienen las columnas

                    if (int.Parse(txtColClase.Text) <= 0 || int.Parse(txtColClase.Text) > columnasMatriz)
                    {
                        MessageBox.Show("La columna de clase debe estar en el rango de columnas del dataset.", "Error");


                    }
                    else 
                    {

                        string[,] matrizDatos = new string[renglonesMatriz, columnasMatriz];    // Se crea la matriz de datos
                        string[,] matrizReal = new string[renglonesMatriz, columnasMatriz];    // Se crea la matriz de datos

                        //MessageBox.Show($"El archivo tiene {renglonesMatriz} renglones y {columnasMatriz} columnas.", "Infor");

                        //progresoBarra.Maximum = renglonesMatriz;
                        lblEstado.Text = "Estado: Cargando Matriz";
                        for (int i = 0; i < renglonesMatriz; i++)
                        {

                            string[] linea = lineas[i].Split(',');   // Se obtiene un renglon o linea del arreglo de lineas
                                                                     //progresoBarra.Value += 1;
                                                                     //MessageBox.Show($"{linea[0]},{linea[1]},{linea[2]},{linea[3]},{linea[4]}", "Linea");

                            for (int j = 0; j < columnasMatriz; j++)
                            {

                                matrizDatos[i, j] = linea[j];            // Se mete cada renglon a la matriz
                                matrizReal[i, j] = linea[j];
                            }


                        }


                        // Se guarda la matriz de datos en la matriz real para calculos posteriores


                        // Obtener clases a partir de renglon proporcionado
                        lblEstado.Text = "Estado: Recabando clases";
                        int colClase = Convert.ToInt32(txtColClase.Text) - 1;

                        Dictionary<String, double> clases = new Dictionary<String, double>(); // Diccionario que almacena las clases y el numero de estas

                        clases = BuscarClases(matrizDatos, renglonesMatriz, colClase);

                        //List<string> clases = new List<string>();       
                        //List<string> instSinClase = new List<string>(); // Lista que almacena las instancias sin clase

                        //progresoBarra.Maximum = renglonesMatriz;

                        //progresoBarra.Value = 0;


                        //for (int i = 0; i < clases.Count; i++)
                        //{
                        //    MessageBox.Show(clases.ElementAt(i).ToString(), "Infor");
                        //}


                        // Saber cuantas categorias tienen las columnas discretizadas
                        /* 
                         * Una lista almacenará los valores discretizados de la columna, si la columna no esta discretizada la lista no almacenará nada
                         * Se tendrá un vector que contendrá la cantidad de clases discretizadas en la columna, de forma que el indice de este correspondera al
                         * numero de columna. Si una columna no esta discretizada entonces el valor de la lista será 0.
                         */

                        // Vector que almacena la cantidad de categorias por columna
                        int[] infoCatColumna = BuscarCategoriasColumnas(matrizDatos, renglonesMatriz, columnasMatriz, colClase);

                        // Discretización
                        // Primero se sacan los mayores y menores de cada columna
                        double[,] mayoresMenores = CalcularValsMaxMin(matrizDatos, columnasMatriz, renglonesMatriz);   // Matriz que contendra los valores mayor y menor de cada columna

                        // Luego se realiza la discretización de cada valor del renglon
                        lblEstado.Text = "Estado: Discretizando columnas";
                        int categorias = int.Parse(txtCategorias.Text);

                        matrizDatos = DiscretizarDatos(matrizDatos, renglonesMatriz, columnasMatriz, colClase, mayoresMenores, categorias);

                        // En este punto los datos ya estan discretizados

                        // Cabmiar valores de prueba
                        lblEstado.Text = "Estado: Cambiando valores de prueba";

                        matrizDatos = CambiarValoresPrueba(matrizDatos, renglonesMatriz, columnasMatriz, colClase, clases, infoCatColumna, categorias);

                        // Matriz de confusión

                        /*
                         * Se recorreran las 2 matrices (Datos y Real) comparando su colClase, habrá una condición que haga esta comparación
                         * Si son iguales se obtiene el índice de la clase en las 2 matrices y estos indices indicaran la posición del contador a 
                         * aumentar en la matriz de confusión.
                        */

                        lblEstado.Text = "Estado: Generando Matriz de Confusión";
                        double[,] matrizConfusion = GenerarMatrizConfusion(matrizDatos, matrizReal, renglonesMatriz, colClase, clases);

                        // En este punto la matriz de confusión ya esta generada

                        // Calculo de métricas
                        dgvConfusion.Columns.Clear();
                        dgvConfusion.Rows.Clear();

                        lblEstado.Text = "Estado: Calculando Métricas";
                        for (int i = 0; i < clases.Count; i++)
                        {

                            dgvConfusion.Columns.Add("columna" + i, clases.ElementAt(i).Key);

                        }

                        dgvConfusion.Columns.Add("columnaTotal", "Total");
                        //MessageBox.Show($"El DGV tiene {dgvConfusion.Columns.Count} columnas.","Columnas de dgv");
                        //MessageBox.Show($"Hay {clases.Count} clases.", "Columnas de dgv");

                        double[] totalesVerticales = new double[clases.Count];
                        double totalDiagonal = 0;

                        for (int i = 0; i < clases.Count; i++)
                        {
                            double total = 0;

                            dgvConfusion.Rows.Add();

                            for (int j = 0; j < clases.Count; j++)
                            {
                                if (i == j)
                                {
                                    totalDiagonal += matrizConfusion[i, j];
                                }

                                //MessageBox.Show($"Matriz Confusión {matrizConfusion[i,j]}");
                                dgvConfusion[j, i].Value = matrizConfusion[i, j];

                                total += matrizConfusion[i, j];
                                totalesVerticales[i] += matrizConfusion[j, i];

                            }

                            dgvConfusion[clases.Count, i].Value = total;

                        }

                        for (int i = dgvConfusion.RowCount - 1; i < dgvConfusion.RowCount; i++)
                        {

                            //MessageBox.Show($"Totales Verticales {i} = {totalesVerticales[i]}", "DGV");

                            for (int j = 0; j < clases.Count; j++)
                            {
                                // La i representa la columna y la j el renglon
                                //MessageBox.Show($"Renglon {j}, Columna {i} = {dgvConfusion[j, i].Value}", "DGV");
                                dgvConfusion[j, i].Value = totalesVerticales[j];
                            }



                        }

                        dgvConfusion[clases.Count, clases.Count].Value = renglonesMatriz;

                        dgvMetricas.Rows.Clear();
                        dgvMetricas.Columns.Clear();

                        string[] nombres = { "Categoria", "Precisión", "Recall", "F1" };

                        if (clases.Count <= 2)
                        {
                            double precision = 0;
                            double recall = 0;
                            double f1 = 0;
                            double accuracy = 0;

                            for (int i = 0; i < nombres.Length; i++)
                            {
                                dgvMetricas.Columns.Add(nombres[i], nombres[i]);
                            }

                            //MessageBox.Show($"{matrizConfusion[0, 0]} / ({matrizConfusion[0, 0]} + {matrizConfusion[0, 1]})");

                            precision = matrizConfusion[0, 0] / (matrizConfusion[0, 0] + matrizConfusion[0, 1]);
                            recall = matrizConfusion[0, 0] / (matrizConfusion[0, 0] + matrizConfusion[1, 0]);
                            f1 = 2 * ((precision * recall) / (precision + recall));

                            for (int i = 0; i < clases.Count; i++)
                            {

                                for (int j = 0; j < clases.Count; j++)
                                {


                                    if (i == j)
                                    {
                                        accuracy += matrizConfusion[i, j];

                                    }

                                }



                            }

                            accuracy /= renglonesMatriz;

                            dgvMetricas.Rows.Add(clases.ElementAt(0).Key, precision.ToString("0.###"), recall.ToString("0.###"), f1.ToString("0.###"));

                            txtAccuracy.Text = accuracy.ToString("0.###");

                        }
                        else
                        {

                            double[] precision = new double[clases.Count];
                            double[] recall = new double[clases.Count];
                            double[] f1 = new double[clases.Count];
                            double accuracy = 0;

                            for (int i = 0; i < nombres.Length; i++)
                            {
                                dgvMetricas.Columns.Add(nombres[i], nombres[i]);
                            }


                            for (int i = 0; i < clases.Count; i++)
                            {

                                double precisionDiv = 0;
                                double recallDiv = 0;



                                for (int j = 0; j < clases.Count; j++)
                                {
                                    precisionDiv += matrizConfusion[i, j];

                                    recallDiv += matrizConfusion[j, i];

                                    if (i == j)
                                    {
                                        accuracy += matrizConfusion[i, j];

                                    }

                                }



                                precision[i] = matrizConfusion[i, i] / precisionDiv;
                                recall[i] = matrizConfusion[i, i] / recallDiv;

                                f1[i] = 2 * ((precision[i] * recall[i]) / (precision[i] + recall[i]));

                                dgvMetricas.Rows.Add(clases.ElementAt(i).Key, precision[i].ToString("0.###"), recall[i].ToString("0.###"), f1[i].ToString("0.###"));

                            }

                            accuracy /= renglonesMatriz;


                            txtAccuracy.Text = accuracy.ToString("0.###");
                        }

                        lblEstado.Text = "Estado: Presentación de Resultados";
                        pnlResultados.Visible = true;


                    }


                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "Error");

                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error");

                }
                // Tira ArgumentException cuando no hay ruta
                // Tira IO.IOException cuando el archivo esta siendo ocupado por otro proceso


            }

        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            pnlResultados.Visible = false;
            lblEstado.Text = "Estado: ";
        }

        private string[] Desordenar(List<string> arr)
        {


            List<string> arrDes = new List<string>();
            Random randNum = new Random();
            while (arr.Count > 0)
            {
                int val = randNum.Next(0, arr.Count - 1);
                arrDes.Add(arr[val]);
                arr.RemoveAt(val);
            }
            arr = arrDes;

            return arr.ToArray();

        }

        private Dictionary<string, double> BuscarClases(string[,] matrizDatos, double renglonesMatriz, int colClase)
        {

            Dictionary<String, double> clases = new Dictionary<String, double>();

            for (int i = 0; i < renglonesMatriz; i++)
            {
                //progresoBarra.Value += 1;
                // Si la clase ya esta contenida en el diccionario entonces se le suma 1 al contador
                if (clases.ContainsKey(matrizDatos[i, colClase]))
                {
                    clases[matrizDatos[i, colClase]] += 1;

                }
                else if (!(matrizDatos[i, colClase].Trim().Length == 0))
                {
                    // Si no esta contenida entonces se añade
                    clases.Add(matrizDatos[i, colClase], 1);
                }

                //if (matrizDatos[i, colClase].Trim().Length == 0) {
                //    instSinClase.Add(matrizDatos[i, colClase]);
                //}


            }

            return clases;

        }

        private int[] BuscarCategoriasColumnas(string[,] matrizDatos, double renglonesMatriz, int columnasMatriz, int colClase)
        {

            int[] infoCatColumna = new int[columnasMatriz];

            // Lista que almacenará las categorias de la columna
            List<string> catColumna = new List<string>();

            // Se recorre la matriz por columnas
            for (int i = 0; i < columnasMatriz; i++)
            {

                // La lista se limpia
                catColumna.Clear();

                for (int j = 0; j < renglonesMatriz; j++)
                {


                    if (!(Double.TryParse(matrizDatos[0, i], out _))) // Si la columna ya esta discretizada
                    {

                        if (!(i == colClase)) // Si la columna no es la columna con la clase
                        {

                            if (!catColumna.Contains(matrizDatos[j, i])) // Si la lista no contiene esa clase
                            {
                                catColumna.Add(matrizDatos[j, i]);  // Se añade la clase que no se contenia
                            }

                        }

                    }

                }


                infoCatColumna[i] = catColumna.Count;   // Se guarda la cantidad de clases que tenia la columna discretizada

                //if (infoCatColumna[i] == 0)
                //{
                //    MessageBox.Show($"La columna {i + 1} no esta discretizada.", "Categorias por columna");

                //}
                //else
                //{
                //    MessageBox.Show($"La columna {i + 1} tiene {infoCatColumna[i]} categorias.", "Categorias por columna");
                //}

            }

            return infoCatColumna;

        }

        private double[,] CalcularValsMaxMin(string[,] matrizDatos, int columnasMatriz, int renglonesMatriz)
        {

            double[,] mayoresMenores = new double[2, columnasMatriz];   // Matriz que contendra los valores mayor y menor de cada columna

            for (int i = 0; i < columnasMatriz; i++)
            {

                // Si la columna es igual a la columna de las clases o ya esta discretizada entonces no se hace nada
                if (Double.TryParse(matrizDatos[0, i], out _))
                {

                    double mayor = 0;
                    double menor = 999999;

                    for (int j = 0; j < renglonesMatriz; j++)
                    {

                        //MessageBox.Show(matrizDatos[j, i], "Info");

                        if (Double.Parse(matrizDatos[j, i]) > mayor)
                        {
                            mayor = Double.Parse(matrizDatos[j, i]);
                        }

                        if (Double.Parse(matrizDatos[j, i]) < menor)
                        {
                            menor = Double.Parse(matrizDatos[j, i]);
                        }

                    }

                    //En este punto se tienen los valores mayor y menor de la columna

                    mayoresMenores[0, i] = mayor;   // Los mayores se almacenan en el 1er renglon
                    mayoresMenores[1, i] = menor;   // Los menores se almacenan en el 2do renglon

                    //MessageBox.Show($"La variable mayor de la columna {i} es: {mayor}", "Info");
                    //MessageBox.Show($"La variable menor de la columna {i} es: {menor}", "Info");

                }

            }

            return mayoresMenores;

        }

        private string[,] DiscretizarDatos(string[,] matrizDatos, int renglonesMatriz, int columnasMatriz, int colClase, double[,] mayoresMenores, double categorias)
        {
            // Lista de rangos que varía
            List<double> listaRangos = new List<double>();

            // Se recorre cada renglon de la respectiva columna en la matriz
            for (int i = 0; i < columnasMatriz; i++)
            {
                listaRangos.Clear();
                //progresoBarra.Value += 1;
                // Si el valor es un número entonces se discretizara
                if (Double.TryParse(matrizDatos[0, i], out _) && i != colClase) // TryParse devuelve false si lo que se quiere convertir no es un numero
                {

                    double rango = (mayoresMenores[0, i] - mayoresMenores[1, i]) / categorias;      // Se obtiene el rango de la columna
                    listaRangos.Add(mayoresMenores[1, i] + rango); // Primer rango
                                                                   //MessageBox.Show($"El rango de la columna {i + 1} es {rango}", "Infor");

                    // For para calcular los demás rangos
                    for (int j = 1; j < categorias - 1; j++)
                    {
                        listaRangos.Add(listaRangos[j - 1] + rango);

                    }

                    //for (int j = 0; j < listaRangos.Count; j++)
                    //{
                    //    MessageBox.Show($"El rango {j} de la columna {j + 1} es {listaRangos[j]}", "Infor");
                    //}


                    //Se recorre cada renglon de la columna para cambiar su valor a un valor discretizado
                    for (int j = 0; j < renglonesMatriz; j++)
                    {
                        // Se recorre la lista de rangos
                        for (int x = 0; x < listaRangos.Count; x++)
                        {
                            // Si se esta parado en el ultimo rango
                            if (x == listaRangos.Count - 1)
                            {

                                if (Double.Parse(matrizDatos[j, i]) >= listaRangos[x])  // Si el valor es mayor o igual al ultimo rango de la lista
                                {
                                    //MessageBox.Show($"El valor {matrizDatos[j, i]} será de categoría {(x + 1)}");
                                    matrizDatos[j, i] = (x + 1).ToString(); // Se asigna la ultima categoria

                                }
                                else
                                {
                                    //MessageBox.Show($"El valor {matrizDatos[j, i]} será de categoría {x}");
                                    matrizDatos[j, i] = x.ToString();       // Se asigna la penultima categoria


                                }

                            }
                            else
                            {
                                if (Double.Parse(matrizDatos[j, i]) < listaRangos[x])
                                {
                                    //MessageBox.Show($"El valor {matrizDatos[j, i]} será de categoría {x}");
                                    matrizDatos[j, i] = x.ToString();   // Se asigna la categoria que corresponde
                                    break;  // Se rompe para que no modifique el 1er valor asignado
                                }

                            }

                        }


                    }

                }

            }

            return matrizDatos;

        }

        private string[,] CambiarValoresPrueba(string[,] matrizDatos, int renglonesMatriz, int columnasMatriz, int colClase, Dictionary<string, double> clases, int[] infoCatColumna, int categorias)
        {
            double porcentajeEnt = Double.Parse(txtPorcentajeEnt.Text) / 100;   // Porcentaje de entrenamiento proporcionado por el usuario que indica cuantos datos serán de prueba

            double cantidadDatos = Math.Ceiling(renglonesMatriz * porcentajeEnt);

            int indiceFinEnt = Convert.ToInt32(renglonesMatriz - cantidadDatos);

            //MessageBox.Show("El indice donde se termina el entrenamiento es " + indiceFinEnt.ToString(), "Indice Fin de entrenamiento");

            // Se necesita una lista de probabilidades ya que estas variaran dependiendo del numero de clases
            double[] probabilidades = new double[clases.Count];     // Lista de probabilidades
            int k = 1;

            // Tabla que primero almacenará los contadores de cada atributo y clase, y luego almacenará las probabilidades
            double[,] tablaProbs = new double[columnasMatriz, clases.Count];

            //progresoBarra.Maximum = renglonesMatriz;

            //progresoBarra.Value = 0;
            //progresoBarra.Value += 1;

            // Se inicializa la tabla con los valores de k
            

            for (int i = indiceFinEnt; i < renglonesMatriz; i++)    // Este for comienza en el indice desde donde se deben hacer las pruebas
            {

                //progresoBarra.Value += 1;

                for (int j = 0; j < columnasMatriz; j++)
                {

                    for (int z = 0; z < clases.Count; z++)
                    {
                        tablaProbs[j, z] = k;

                    }

                }

                // Comienza la asignacion de valores
                for (int j = 0; j < columnasMatriz; j++)            // Este for recorre las columnas
                {

                    if (!(j == colClase))
                    {

                        // En este for se buscan los valores del valor en el que se esta parado
                        for (int x = 0; x < indiceFinEnt; x++)          // Este for recorre los indices que son de entrenamiento
                        {
                            // Se recorre las clases para verificar si el valor pertenece a alguna de ellas
                            for (int c = 0; c < clases.Count; c++)
                            {

                                if (matrizDatos[i, j].Equals(matrizDatos[x, j]) && clases.ElementAt(c).Key.Equals(matrizDatos[x, colClase]))
                                {

                                    tablaProbs[j, c] += 1;   // Aquí se va almacenando la cantidad de veces que se tiene el valor y pertenece a la clase

                                }

                            }


                        }



                    }

                }

                // Se obtienen las probabilidades del renglon
                for (int y = 0; y < clases.Count; y++)
                {

                    double probFinal = 1;

                    for (int x = 0; x < columnasMatriz; x++)
                    {
                        if (!(x == colClase))
                        {

                            // Se debe saber cuantas categorias hay por columna, es decir, si el dato ya viene discretizado se necesita saber cuantas categorias tiene esa columna
                            // De forma que si es igual a 0 entonces la columna no estaba discretizada
                            if (infoCatColumna[x] == 0)
                            {
                                tablaProbs[x, y] /= (clases.ElementAt(y).Value + categorias);   // Se suma el numero categorias del atributo
                            }
                            else
                            {

                                tablaProbs[x, y] /= (clases.ElementAt(y).Value + infoCatColumna[x]);   // Se suma el numero categorias de la columna que ya estaba discretizada al atributo

                            }

                            probFinal *= tablaProbs[x, y];

                        }

                    }

                    // Se guardan las probabilidades del renglón de cada clase 
                    probabilidades[y] = probFinal * (clases.ElementAt(y).Value / renglonesMatriz);
                    //MessageBox.Show($"La probabilidad de la clase {y} = {probabilidades[y]}", "Probabilidades");

                }

                // Se obtiene el indice del numero mayor y se asigna la clase al renglón mediante el indice
                for (int x = 0; x < probabilidades.Length; x++)
                {


                    if (probabilidades[x] == probabilidades.Max())
                    {

                        //MessageBox.Show($"El valor de la matriz es {matrizDatos[i, colClase]}", "Probabilidades Antes");
                        //MessageBox.Show($"El valor de clase es {clases.ElementAt(x).Key}", "Probabilidades Antes");
                        //MessageBox.Show($"Valor en matrizDatos: {matrizDatos[i, colClase]}, Valor en matrizReal: {matrizReal[i, colClase]}", "Antes");
                        matrizDatos[i, colClase] = clases.ElementAt(x).Key;
                        //MessageBox.Show($"Valor en matrizDatos: {matrizDatos[i, colClase]}, Valor en matrizReal: {matrizReal[i, colClase]}", "Despues");
                        //MessageBox.Show($"El valor de la matriz es {matrizDatos[i, colClase]}", "Probabilidades Despues");
                    }

                }


            }

            return matrizDatos;
        }

        private double[,] GenerarMatrizConfusion(string[,] matrizDatos, string[,] matrizReal, int renglonesMatriz, int colClase, Dictionary<string, double> clases) {

            double[,] matrizConfusion = new double[clases.Count, clases.Count];

            for (int i = 0; i < renglonesMatriz; i++)
            {

                if (matrizDatos[i, colClase].Equals(matrizReal[i, colClase]) || matrizDatos[i, colClase] != matrizReal[i, colClase])
                {

                    //MessageBox.Show($"Valor en matrizDatos: {matrizDatos[i, colClase]}, Valor en matrizReal: {matrizReal[i, colClase]}", "Matriz Confusion");

                    int indice1 = clases.Keys.ToList().IndexOf(matrizDatos[i, colClase]);
                    int indice2 = clases.Keys.ToList().IndexOf(matrizReal[i, colClase]);

                    matrizConfusion[indice1, indice2] += 1;
                    //MessageBox.Show($"El valor de la posicion {indice1}, {indice2} es {matrizConfusion[indice1, indice2]}", "Matriz Confusion");
                    //MessageBox.Show($"", "Matriz Confusion");

                }

            }

            return matrizConfusion;

        }

        private void txtcolClase_KeyPress(object sender, KeyPressEventArgs e) {
            
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

    }
}