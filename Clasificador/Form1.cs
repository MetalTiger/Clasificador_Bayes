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
             *-Que la columna de clase no este en blanco, que solo se ingresen numeros y que no se puedan poner numeros m�s grandes a la cantidad de columnas en la matriz
             *-Que el intervalo de discretizaci�n no este en blanco y que solo se ingresen numeros
            */

            if (txtRuta.Text.Trim().Length == 0)
            {
                MessageBox.Show("Introduzca una ruta.", "Error");

            } else if (txtColClase.Text.Trim().Length == 0) {
                MessageBox.Show("Introduzca una columna de clase.", "Error");

            } else if (txtPorcentajeEnt.Text.Trim().Length == 0) {
                MessageBox.Show("Introduzca un porcentaje de entrenamiento.", "Error");

            } else if (txtCategorias.Text.Trim().Length == 0) {
                MessageBox.Show("Introduzca un intervalo de discretizaci�n.", "Error");
            }
            else
            {
                // Lectura de archivo y construcci�n de la matriz
                // Tira ArgumentException cuando no hay ruta
                // Tira IO.IOException cuando el archivo esta siendo ocupado por otro proceso
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


                int columnasMatriz = lineas[0].Split(',').Length;       // Se obtienen las columnas

                string[,] matrizDatos = new string[renglonesMatriz, columnasMatriz];    // Se crea la matriz de datos
                string[,] matrizReal = new string[renglonesMatriz, columnasMatriz];    // Se crea la matriz de datos

                //MessageBox.Show($"El archivo tiene {renglonesMatriz} renglones y {columnasMatriz} columnas.", "Infor");

                for (int i = 0; i < renglonesMatriz; i++)
                {

                    string[] linea = lineas[i].Split(',');   // Se obtiene un renglon o linea del arreglo de lineas

                    //MessageBox.Show($"{linea[0]},{linea[1]},{linea[2]},{linea[3]},{linea[4]}", "Linea");

                    for (int j = 0; j < columnasMatriz; j++)
                    {

                        matrizDatos[i, j] = linea[j];            // Se mete cada renglon a la matriz
                        matrizReal[i, j] = linea[j];
                    }


                }

                // Se guarda la matriz de datos en la matriz real para calculos posteriores


                // Obtener clases a partir de renglon proporcionado

                int colClase = Convert.ToInt32(txtColClase.Text) - 1;

                Dictionary<String, double> clases = new Dictionary<String, double>(); // Diccionario que almacena las clases y el numero de estas

                //List<string> clases = new List<string>();       
                //List<string> instSinClase = new List<string>(); // Lista que almacena las instancias sin clase

                for (int i = 0; i < renglonesMatriz; i++)
                {

                    // Si la clase ya esta contenida en el diccionario entonces se le suma 1 al contador
                    if (clases.ContainsKey(matrizDatos[i, colClase]))
                    {
                        clases[matrizDatos[i, colClase]] += 1;

                    }
                    else if (!(matrizDatos[i, colClase].Trim().Length == 0))
                    {
                        // Si no esta contenida entonces se a�ade
                        clases.Add(matrizDatos[i, colClase], 1);
                    }

                    //if (matrizDatos[i, colClase].Trim().Length == 0) {
                    //    instSinClase.Add(matrizDatos[i, colClase]);
                    //}


                }

                //for (int i = 0; i < clases.Count; i++)
                //{
                //    MessageBox.Show(clases.ElementAt(i).ToString(), "Infor");
                //}



                // Saber cuantas categorias tienen las columnas discretizadas
                /* 
                 * Una lista almacenar� los valores discretizados de la columna, si la columna no esta discretizada la lista no almacenar� nada
                 * Se tendr� un vector que contendr� la cantidad de clases discretizadas en la columna, de forma que el indice de este correspondera al
                 * numero de columna. Si una columna no esta discretizada entonces el valor de la lista ser� 0.
                 */

                // Lista que almacenar� las categorias de la columna
                List<string> catColumna = new List<string>();

                // Vector que almacena la cantidad de categorias por columna
                int[] infoCatColumna = new int[columnasMatriz];

                // Se recorre la matriz por columnas
                for (int i = 0; i < columnasMatriz; i++) {

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
                                    catColumna.Add(matrizDatos[j, i]);  // Se a�ade la clase que no se contenia
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

                // Discretizaci�n
                // Primero se sacan los mayores y menores de cada columna

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

                // Luego se realiza la discretizaci�n de cada valor del renglon
                int categorias = int.Parse(txtCategorias.Text);

                List<double> listaRangos = new List<double>();


                // Se recorre cada renglon de la respectiva columna en la matriz
                for (int i = 0; i < columnasMatriz; i++) {
                    listaRangos.Clear();

                    // Si el valor es un n�mero entonces se discretizara
                    if (Double.TryParse(matrizDatos[0, i], out _)) // TryParse devuelve false si lo que se quiere convertir no es un numero
                    {

                        double rango = (mayoresMenores[0, i] - mayoresMenores[1, i]) / categorias;      // Se obtiene el rango de la columna
                        listaRangos.Add(mayoresMenores[1, i] + rango); // Primer rango
                        //MessageBox.Show($"El rango de la columna {i + 1} es {rango}", "Infor");

                        // For para calcular los dem�s rangos
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
                                        //MessageBox.Show($"El valor {matrizDatos[j, i]} ser� de categor�a {(x + 1)}");
                                        matrizDatos[j, i] = (x + 1).ToString(); // Se asigna la ultima categoria

                                    }
                                    else
                                    {
                                        //MessageBox.Show($"El valor {matrizDatos[j, i]} ser� de categor�a {x}");
                                        matrizDatos[j, i] = x.ToString();       // Se asigna la penultima categoria


                                    }

                                }
                                else
                                {
                                    if (Double.Parse(matrizDatos[j, i]) < listaRangos[x])
                                    {
                                        //MessageBox.Show($"El valor {matrizDatos[j, i]} ser� de categor�a {x}");
                                        matrizDatos[j, i] = x.ToString();   // Se asigna la categoria que corresponde
                                        break;  // Se rompe para que no modifique el 1er valor asignado
                                    }

                                }

                            }


                        }

                    }

                }

                // En este punto los datos ya estan discretizados

                double porcentajeEnt = Double.Parse(txtPorcentajeEnt.Text) / 100;   // Porcentaje de entrenamiento proporcionado por el usuario que indica cuantos datos ser�n de prueba

                double cantidadDatos = Math.Ceiling(renglonesMatriz * porcentajeEnt);

                int indiceFinEnt = Convert.ToInt32(renglonesMatriz - cantidadDatos);

                MessageBox.Show("El indice done se termina el entrenamiento es " + indiceFinEnt.ToString(), "Indice Fin de entrenamiento");

                // Se necesita una lista de probabilidades ya que estas variaran dependiendo del numero de clases
                double[] probabilidades = new double[clases.Count];     // Lista de probabilidades
                int k = 1;

                // Tabla que primero almacenar� los contadores de cada atributo y clase, y luego almacenar� las probabilidades
                double[,] tablaProbs = new double[columnasMatriz, clases.Count];

                for (int i = indiceFinEnt; i < renglonesMatriz; i++)    // Este for comienza en el indice desde donde se deben hacer las pruebas
                {

                    // Se inicializa la tabla con los valores de k
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

                        if (!(j == colClase)) {

                            // En este for se buscan los valores del valor en el que se esta parado
                            for (int x = 0; x < indiceFinEnt; x++)          // Este for recorre los indices que son de entrenamiento
                            {
                                // Se recorre las clases para verificar si el valor pertenece a alguna de ellas
                                for (int c = 0; c < clases.Count; c++) {

                                    if (matrizDatos[i, j].Equals(matrizDatos[x, j]) && clases.ElementAt(c).Key.Equals(matrizDatos[x, colClase]))
                                    {

                                        tablaProbs[j, c] += 1;   // Aqu� se va almacenando la cantidad de veces que se tiene el valor y pertenece a la clase

                                    }

                                }


                            }



                        }

                    }

                    // Se obtienen las probabilidades del renglon
                    for (int y = 0; y < clases.Count; y++) {

                        double probFinal = 1;

                        for (int x = 0; x < columnasMatriz; x++)
                        {
                            if (!(x == colClase)) {

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

                        // Se guardan las probabilidades del rengl�n de cada clase 
                        probabilidades[y] = probFinal * (clases.ElementAt(y).Value / renglonesMatriz);
                        //MessageBox.Show($"La probabilidad de la clase {y} = {probabilidades[y]}", "Probabilidades");

                    }

                    // Se obtiene el indice del numero mayor y se asigna la clase al rengl�n mediante el indice
                    for (int x = 0; x < probabilidades.Length; x++) {


                        if (probabilidades[x] == probabilidades.Max()) {

                            //MessageBox.Show($"El valor de la matriz es {matrizDatos[i, colClase]}", "Probabilidades Antes");
                            //MessageBox.Show($"El valor de clase es {clases.ElementAt(x).Key}", "Probabilidades Antes");
                            //MessageBox.Show($"Valor en matrizDatos: {matrizDatos[i, colClase]}, Valor en matrizReal: {matrizReal[i, colClase]}", "Antes");
                            matrizDatos[i, colClase] = clases.ElementAt(x).Key;
                            //MessageBox.Show($"Valor en matrizDatos: {matrizDatos[i, colClase]}, Valor en matrizReal: {matrizReal[i, colClase]}", "Despues");
                            //MessageBox.Show($"El valor de la matriz es {matrizDatos[i, colClase]}", "Probabilidades Despues");
                        }

                    }


                }

                //for (int i = indiceFinEnt; i < renglonesMatriz; i++)
                //{


                //    for (int j = 0; j < columnasMatriz; j++)
                //    {

                //        MessageBox.Show(matrizDatos[i, j], "Clases");

                //    }


                //}


                // Matriz de confusi�n

                /*
                 * Se recorreran las 2 matrices (Datos y Real) comparando su colClase, habr� una condici�n que haga esta comparaci�n
                 * Si son iguales se obtiene el �ndice de la clase en las 2 matrices y estos indices indicaran la posici�n del contador a 
                 * aumentar en la matriz de confusi�n.
                */



                double[,] matrizConfusion = new double[clases.Count, clases.Count];

                for (int i = 0; i < renglonesMatriz; i++) {

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


                // En este punto la matriz de confusi�n ya esta generada

                // Calculo de m�tricas
                dgvConfusion.Columns.Clear();
                dgvConfusion.Rows.Clear();


                for (int i = 0; i < clases.Count; i++)
                {


                    dgvConfusion.Columns.Add("columna" + i, "Clase " + i);

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

                        //MessageBox.Show($"Matriz Confusi�n {matrizConfusion[i,j]}");
                        dgvConfusion[j, i].Value = matrizConfusion[i, j];

                        total += matrizConfusion[i, j];
                        totalesVerticales[i] += matrizConfusion[j, i];

                    }

                    dgvConfusion[clases.Count, i].Value = total;

                }

                //dgvConfusion.Rows.Add();

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

                string[] nombres = { "Categoria", "Precisi�n", "Recall", "F1" };

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

                    MessageBox.Show($"{matrizConfusion[0, 0]} / ({matrizConfusion[0, 0]} + {matrizConfusion[0, 1]})");

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

                    dgvMetricas.Rows.Add(clases.ElementAt(0).Key, precision, recall, f1);

                    txtAccuracy.Text = accuracy.ToString();

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

                        dgvMetricas.Rows.Add(clases.ElementAt(i).Key, precision[i], recall[i], f1[i]);

                    }

                    accuracy /= renglonesMatriz;


                    txtAccuracy.Text = accuracy.ToString();
                }


            }

        }


    }
}