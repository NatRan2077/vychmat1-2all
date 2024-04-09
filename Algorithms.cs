namespace vychmat1_2
{
    /// <summary>
    /// Класс алгоритмов решения СЛАУ.
    /// </summary>
    public static class Algorithms
    {
        public static float[] ShuttleSolve(Matrix matrix, float[] bVector)
        {
            float[] res = new float[matrix.Rows];
            Matrix buffer = (Matrix)matrix.Clone();
            float[] bufVector = (float[])bVector.Clone();
            //if (!MatrixConverter.Tridiagonal.IsTridiagonal(buffer))
                //throw new ArgumentException("Матрица не трехдиагональная!");
            int n = buffer.Rows;
            float[] a = new float[n];
            float[] b = new float[n];
            a[0] = 0;
            b[0] = 0;
            for (int i = 1; i < n; i++)
            {
                a[i] = matrix[i - 1, i] / buffer[i - 1, i - 1];
                b[i] = bufVector[i - 1] / buffer[i - 1, i - 1];

                buffer[i, i] = matrix[i, i] - matrix[i, i - 1] * a[i];
                bufVector[i] = bVector[i] - matrix[i, i - 1] * b[i];
                buffer[i, i - 1] = 0.0f;
            }
            res[n - 1] = bufVector[n - 1] / buffer[n - 1, n - 1];
            while (n > 1)
            {
                n--;
                res[n - 1] = b[n] - a[n] * res[n];
            }
            return res;
        }

        public static void SwapRows(Matrix matrix, int row1, int row2)
        {
            int cols = matrix.values.GetLength(1);
            for (int i = 0; i < cols; i++)
            {
                float temp = matrix[row1, i];
                matrix[row1, i] = matrix[row2, i];
                matrix[row2, i] = temp;
            }
        }

        public static float[,] AddVectorToMatrix(Matrix matrix, float[] vector)
        {
            int rows = matrix.values.GetLength(0);
            int cols = matrix.values.GetLength(1);
            float[,] result = new float[rows, cols + 1];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result[i, j] = matrix[i, j];
                }
                result[i, cols] = vector[i];
            }
            return result;
        }


        public static float[] GaussWithoutMainElement(Matrix matrix, float[] vector)
        {
            Matrix fullMatrix = new Matrix(AddVectorToMatrix(matrix, vector));
            int rows = fullMatrix.values.GetLength(0);
            int cols = fullMatrix.values.GetLength(1);

            for (int i = 0; i < rows - 1; i++)
            {
                int firstNonZero = i;
                while (firstNonZero < rows && fullMatrix[firstNonZero, i] == 0)
                {
                    firstNonZero++;
                }

                if (firstNonZero == rows)
                {
                    continue;
                }

                SwapRows(fullMatrix, i, firstNonZero);

                for (int j = i + 1; j < rows; j++)
                {
                    float factor = fullMatrix[j, i] / fullMatrix[i, i];
                    for (int k = i; k < cols; k++)
                    {
                        fullMatrix[j, k] -= factor * fullMatrix[i, k];
                    }
                }
            }

            float[] solution = new float[rows];
            for (int i = rows - 1; i >= 0; i--)
            {
                float sum = 0;
                for (int j = i + 1; j < cols - 1; j++)
                {
                    sum += fullMatrix[i, j] * solution[j];
                }
                solution[i] = (fullMatrix[i, cols - 1] - sum) / fullMatrix[i, i];
            }
            return solution;
        }

        public static float[] GaussWithMainElement(Matrix matrix, float[] vector)
        {
            Matrix fullMatrix = new Matrix(AddVectorToMatrix(matrix, vector));
            int rows = fullMatrix.values.GetLength(0);
            int cols = fullMatrix.values.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                int maxRow = i;
                float maxVal = Math.Abs(fullMatrix[i, i]);

                for (int j = i + 1; j < rows; j++)
                {
                    float absVal = Math.Abs(fullMatrix[j, i]);
                    if (absVal > maxVal)
                    {
                        maxVal = absVal;
                        maxRow = j;
                    }
                }

                if (maxRow != i)
                {
                    SwapRows(fullMatrix, i, maxRow);
                }

                for (int j = i + 1; j < rows; j++)
                {
                    float factor = fullMatrix[j, i] / fullMatrix[i, i];
                    for (int k = i; k < cols; k++)
                    {
                        fullMatrix[j, k] -= factor * fullMatrix[i, k];
                    }
                }
            }

            float[] solution = new float[rows];
            for (int i = rows - 1; i >= 0; i--)
            {
                float sum = 0;
                for (int j = i + 1; j < cols - 1; j++)
                {
                    sum += fullMatrix[i, j] * solution[j];
                }
                solution[i] = (fullMatrix[i, cols - 1] - sum) / fullMatrix[i, i];
            }
            return solution;
        }

    }
}
