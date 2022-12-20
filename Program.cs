namespace TestingKURS2022
{
    public class Program
    {
        /// <summary>
        /// общие переменные
        /// </summary>
        public int RowsAmount; //кол-во строк (размерность)
        public double[,] MatrixСoefficient; //матрица коэффициентов
        public double[] FreeСoefficient; //матрица свободных членов
        public double[] Result; //матрица результата
        public double Multi1, Multi2; //вспомогательные переменные для изменения строк в соответствии с алгоритмом Гаусса
        public int MatrixRow, MatrixCol; //вспомогательные переменные для кол-ва строк и столбцов матрицы
        public int testres;

        public int EnterCoefs(Program p)
        {
            while (true)
            {
                if (p.RowsAmount <= 8 & p.RowsAmount > 0)
                {
                    //для теста
                    //return 0;
                    //для теста
                    p.MatrixСoefficient = new double[p.RowsAmount, p.RowsAmount]; //создаем вложенный двумерный массив для коэффициентов по нашей размерности системы (размерность СЛАУ = кол-во уравнений = кол-во переменных)
                    p.FreeСoefficient = new double[p.RowsAmount]; //создаем массив для свободных членов
                    p.Result = new double[p.RowsAmount];


                    Console.WriteLine();
                    Console.WriteLine("Введите коэффициенты и свободные члены матрицы:");
                    Console.WriteLine();


                    for (int i = 0; i < p.RowsAmount; i++)
                    {
                        for (int j = 0; j < p.RowsAmount; j++)
                        {
                            Console.Write($"A[{i + 1}][{j + 1}] = ");
                            try
                            {
                                p.MatrixСoefficient[i, j] = double.Parse(Console.ReadLine());
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Введено не число!");
                                return 1;
                            }
                            //для теста
                            //return 0;
                            //для теста
                        }
                        Console.Write($"B[{i + 1}] = ");
                        try
                        {
                            p.FreeСoefficient[i] = double.Parse(Console.ReadLine());
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Введено не число!");
                            return 1;
                        }

                        Console.WriteLine();
                    }
                    break;
                }
                else
                {
                    // return 1; 
                    Console.WriteLine("Введите корректную размерность! (<8 & >0) : ");


                    try
                    {
                        p.RowsAmount = int.Parse(Console.ReadLine());
                    }
                    catch (Exception)
                    {

                        Console.WriteLine("Введено не число!");
                    }
                }
                
            }
            return 0;
        }

        /// <summary>
        /// ввод данных СЛАУ
        /// </summary>
        /// <param name="p"></param>
        public int EnterValue(Program p)
        {
            Console.WriteLine();
            Console.Write("Введите размерность матрицы (не более 8) : ");


            try
            {
                p.RowsAmount = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {

                Console.WriteLine("Введено не число!");
            }

            EnterCoefs(p);
            
            return 0;
        }
        /// <summary>
        /// выводим начальную систему
        /// </summary>
        /// <param name="p"></param>
        public void writeStart(Program p)
        {
            Console.WriteLine("Начальный вид матрицы:");
            Console.WriteLine();


            p.MatrixRow = p.MatrixСoefficient.GetLength(0);
            p.MatrixCol = p.MatrixСoefficient.GetLength(1);
            for (int i = 0; i < MatrixRow; i++)
            {
                for (int j = 0; j < p.MatrixCol; j++)
                {
                    Console.Write(p.MatrixСoefficient[i, j] + "\t");
                }
                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine("Свободные члены матрицы:");
            Console.WriteLine();


            for (int i = 0; i < p.RowsAmount; i++)
            {
                Console.WriteLine($"B[{i + 1}] = {p.FreeСoefficient[i]}");
            }
            Console.WriteLine();
        }
        /// <summary>
        /// работа алгоритма Гаусса
        /// </summary>
        /// <param name="p"></param>
        public void work(Program p)
        {
            for (int k = 0; k < p.RowsAmount; k++)
            {
                for (int j = k + 1; j < p.RowsAmount; j++)
                {
                    p.Multi1 = p.MatrixСoefficient[j, k] / p.MatrixСoefficient[k, k];
                    for (int i = k; i < p.RowsAmount; i++)
                    {
                        p.MatrixСoefficient[j, i] = p.MatrixСoefficient[j, i] - p.Multi1 * p.MatrixСoefficient[k, i];
                    }
                    p.FreeСoefficient[j] = p.FreeСoefficient[j] - p.Multi1 * p.FreeСoefficient[k];
                }
            }


            for (int k = p.RowsAmount - 1; k >= 0; k--)
            {
                p.Multi1 = 0;
                for (int j = k; j < p.RowsAmount; j++)
                {
                    p.Multi2 = p.MatrixСoefficient[k, j] * p.Result[j];
                    p.Multi1 += p.Multi2;
                }
                p.Result[k] = (p.FreeСoefficient[k] - p.Multi1) / p.MatrixСoefficient[k, k];
            }
        }
        /// <summary>
        /// выводим результат
        /// </summary>
        /// <param name="p"></param>
        public void writeRes(Program p)
        {
            Console.WriteLine("Конечный вид матрицы:");
            Console.WriteLine();


            for (int i = 0; i < p.MatrixRow; i++)
            {
                for (int j = 0; j < p.MatrixCol; j++)
                {
                    Console.Write("{0:0.00}" + "\t", p.MatrixСoefficient[i, j]);
                }
                Console.WriteLine();
            }


            Console.WriteLine();
            Console.WriteLine("Корни матрицы:");
            Console.WriteLine();


            for (int i = 0; i < p.RowsAmount; i++)
            {
                Console.Write($"X[{i + 1}] = ");
                Console.WriteLine("{0:0.00}", p.Result[i]);
            }

        }
  
        static void Main(string[] args)
        {
            Console.WriteLine("Решение СЛАУ методом Гаусса");
            Console.WriteLine();
            Console.WriteLine("Для запуска программы введите 'Запуск'.");
            Console.WriteLine("Для выхода из программы введите 'Выход'.");
            Console.WriteLine();

            Program p = new Program();

            string Control; //строка для управления программой (выйти, повторить)
            Control = Console.ReadLine(); //считываем изначальный выбор пользователя 


            while (true) //цикл закончится, если пользователь выберет "Выход"
            {
                if (Control == "Запуск" || Control == "запуск" || Control == "стартуем!")
                {
                    if(p.EnterValue(p) == 1)
                    {
                        Console.WriteLine("Программа завершила работу.");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                    }


                    p.writeStart(p);


                    p.work(p);


                    p.writeRes(p);
                    

                    Console.WriteLine();
                    Console.WriteLine("Программа завершила решение.");
                    Console.WriteLine();
                    Console.WriteLine("Для повторного запуска программы введите 'Запуск'.");
                    Console.WriteLine("Для выхода из программы введите 'Выход'.");
                    Console.WriteLine();


                    Control = Console.ReadLine();
                }


                if (Control == "Выход" || Control == "выход")
                {
                    Console.WriteLine();
                    Console.WriteLine("Программа завершила работу.");
                    Thread.Sleep(1000);
                    Environment.Exit(0);


                    break;
                }


                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Ошибка! Ввод недопустимого значения.");
                    Console.WriteLine();


                    Control = Console.ReadLine();
                }
            }
        }
    }
}