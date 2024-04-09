

namespace vychmat1_2
{
    class Program
    {
        static void Main()
        {
            MatrixConsole.EnterSystem(out Matrix matrix, out float[] bVector);
            Console.WriteLine("\nВведено: \n");
            MatrixConsole.PrintSystem(matrix, bVector);
            Console.WriteLine("\nРезультат: \n");
            foreach (var r in Algorithms.ShuttleSolve(matrix, bVector)) Console.Write(Math.Round(r, 3) + " ");


            int matrixHeight, matrixWidth;
            float[,] matrixNumbers;
            string? matrixLine = "";
            string[] strNumbers;
            bool IsSuccess = true;

            int numbMethod;

            Console.WriteLine("Решение СЛАУ вида Ax=b. \nВвод матриц вместе со столбцом b");
            Console.Write("Введите высоту матрицы (кол-во строк): ");
            if (!int.TryParse(Console.ReadLine(), out matrixHeight))
            {
                Console.WriteLine("Неправильный ввод числа");
                IsSuccess = false;
            }


            Console.WriteLine();
            Console.Write("Введите ширину матрицы (кол-во столбцов вместе со столбцом b): ");
            if (!int.TryParse(Console.ReadLine(), out matrixWidth))
            {
                Console.WriteLine("Неправильный ввод числа");
                IsSuccess = false;
            }

            if (matrixHeight < 2 || matrixWidth < 3)
            {
                Console.WriteLine($"Матрицу размера {matrixHeight} на {matrixWidth} программа не посчитает");
                IsSuccess = false;
            }

            if (IsSuccess)
            {
                Console.WriteLine();
                Console.WriteLine("Ввод матрицы (запись десятичных дробей через запятую, разделитель - пробел) : ");

                matrixNumbers = new float[matrixHeight, matrixWidth];
                for (int i = 0; i < matrixHeight; i++)
                {
                    matrixLine = Console.ReadLine();
                    if (matrixLine == string.Empty || matrixLine == null)
                    {
                        Console.WriteLine("Неправильный ввод чисел");
                        return;
                    }
                    else
                    {
                        strNumbers = matrixLine.Split(' ');
                        if (strNumbers.Length == matrixWidth)
                        {
                            for (int k = 0; k < strNumbers.Length; k++)
                            {
                                if (!float.TryParse(strNumbers[k], out matrixNumbers[i, k]))
                                {
                                    Console.WriteLine("Неправильный ввод числа");
                                    return;
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Введено неверное кол-во чисел");
                            return;
                        }
                    }
                }

                Matrix2 matrix5 = new(matrixNumbers);
                SimpleIterationsMethod simpleIterationsMethod = new(matrix5);

                if (!IsRightMatrix(matrix5))
                    Console.WriteLine("Метод не применим: ноль на главной диагонали");
                else if (!simpleIterationsMethod.IsIterationsConverge())
                    Console.WriteLine("Метод не применим: не выполнено условие сходимости процесса итерации");
                else
                {
                    simpleIterationsMethod.CreateMatrixAlphaBeta();
                    float[] answers = simpleIterationsMethod.Approach();

                    PrintIntermediateValues(matrix5);

                    Console.WriteLine("\nОТВЕТЫ");
                    PrintResult(answers);
                }

            }

        }
        public static bool IsRightMatrix(Matrix2 matrix)
        {
            bool isSuccess = true;
            for (int i = 0; i < matrix.Height; i++)
            {
                for (int j = 0; j < matrix.Width; j++)
                    if (i == j && matrix[i, j] == 0)
                    {
                        isSuccess = false;
                        break;
                    }
                if (!isSuccess)
                    break;
            }
            if (isSuccess)
                return true;
            return false;
        }


        static private void PrintResult(float[] results)
        {
            for (int i = 0; i < results.Length; i++)
            {
                Console.WriteLine($"x{i + 1} = {results[i]} ");
            }
        }

        static private void PrintIntermediateValues(Matrix2 matrix)
        {
            Console.WriteLine("Промежуточные результаты итераций\n");
            for (int i = 0; i < SimpleIterationsMethod.answersList.Count; i++)
            {
                Console.Write(SimpleIterationsMethod.answersList[i] + " ");
                if ((i + 1) % (matrix.Width - 1) == 0)
                    Console.WriteLine("\n");
            }
        }
    }
}
    