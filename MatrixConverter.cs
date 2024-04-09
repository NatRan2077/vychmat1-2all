namespace vychmat1_2
{
    /// <summary>
    /// Класс для проверки типа матрицы или перевода ее в другой тип.
    /// </summary>
    public static class MatrixConverter
    {
        public static bool IsSquare(Matrix matrix)
        {
            return matrix.Rows == matrix.Columns;
        }

        public static class Tridiagonal
        {
            public static bool IsTridiagonal(Matrix matrix)
            {
                if (!IsSquare(matrix)) return false;
                for (int i = 0; i < matrix.Rows; i++)
                    for (int j = 0; j < i - 1; j++)
                        if (matrix[i, j] != 0.0 || matrix[j, i] != 0.0) return false;
                return true;
            }
        }

        public static class Triangular
        {
            public static bool IsTriangular(Matrix matrix)
            {
                if (!IsSquare(matrix)) return false;
                for (int i = 0; i < matrix.Rows; i++)
                    for (int j = 0; j < i; j++)
                        if (matrix[i, j] != 0.0) return false;
                return true;
            }

            /*public static (Matrix, float[]) ToTriangular(Matrix matrix, float[] bVector)
            {
                Matrix res = (Matrix)matrix.Clone();
                if (matrix.Rows != matrix.Columns) throw new ArgumentException("Некорректная система!");
                if (IsTriangular(matrix)) return (res, bVector);
                //for (int k = 0; k < res.Rows; k++)
                if (res[0, 0] == 0.0)
                {
                    //if (res[k, k] == 0.0)
                    {
                        for (int n = 0; n < res.Columns; n++)
                            (res[0, n], res[0 + 1, n]) = (res[0 + 1, n], res[0, n]);
                        (bVector[0], bVector[0 + 1]) = (bVector[0 + 1], bVector[0]);
                        //k--;
                    }
                }
                for (int k = 0; k < res.Rows; k++)
                    for (int i = k + 1; i < res.Rows; i++)
                    {
                        float buf = res[i, k];
                        for (int j = k; j < res.Columns; j++)
                        {
                            res[i, j] -= res[k, j] * (buf / res[k, k]);
                        }
                        bVector[i] -= (buf / res[k, k]) * bVector[k];
                    }
                return (res, bVector);
            }*/
        }
    }
}
