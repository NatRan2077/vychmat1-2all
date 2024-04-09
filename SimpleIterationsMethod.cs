using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace vychmat1_2
{
    internal class SimpleIterationsMethod
    {
       
        private Matrix2 _matrix;
        private Matrix2 _matrixAlphaBeta;
        private float _accuracy = 0.001f;

        public static List<float> answersList = new();
        public Matrix2 Matrix
        {
            get => _matrix;
            set => _matrix = value;
        }

        public Matrix2 MatrixAlphaBeta
        {
            get => _matrixAlphaBeta;
            set => _matrixAlphaBeta = value;
        }

        public SimpleIterationsMethod(Matrix2 matrix)
        {
            Matrix = matrix;
        }


        public void CreateMatrixAlphaBeta()
        {
            float[,] numbers = new float[Matrix.Height, Matrix.Width];

            for (int i = 0; i < Matrix.Height; i++)
            {
                for (int j = 0; j < Matrix.Width; j++)
                {
                    if (i != j)
                    {
                        if (j == Matrix.Width - 1)
                            numbers[i, j] = Matrix[i, j] / Matrix[i, i];
                        else
                            numbers[i, j] = -Matrix[i, j] / Matrix[i, i];
                    }
                    else
                    {
                        numbers[i, j] = 0;
                    }
                }
            }
            MatrixAlphaBeta = new(numbers);
        }

        private float NormMatrixRows()
        {
            float normMatrix = 0;
            float sumNumbInStr = 0;

            for (int i = 0; i < MatrixAlphaBeta.Height; i++)
            {
                sumNumbInStr = 0;
                for (int j = 0; j < MatrixAlphaBeta.Width - 1; j++)
                    sumNumbInStr += Math.Abs(MatrixAlphaBeta[i, j]);

                if (sumNumbInStr > normMatrix)
                    normMatrix = sumNumbInStr;
            }
            return normMatrix;
        }

        private float NormMatrixColumns()
        {
            float normMatrix = 0;
            float sumNumbInColumn = 0;

            for (int j = 0; j < MatrixAlphaBeta.Width - 1; j++)
            {
                sumNumbInColumn = 0;
                for (int i = 0; i < MatrixAlphaBeta.Height; i++)
                    sumNumbInColumn += Math.Abs(MatrixAlphaBeta[i, j]);

                if (sumNumbInColumn > normMatrix)
                    normMatrix = sumNumbInColumn;
            }
            return normMatrix;
        }

        private float NormMatrixEuclid(Matrix2 matrix)
        {
            float normMatrix = 0;

            for (int i = 0; i < matrix.Height; i++)
                for (int j = 0; j < matrix.Width - 1; j++)
                    normMatrix += matrix[i, j] * matrix[i, j];

            return (float)Math.Sqrt((double)normMatrix);
        }

        private float NormMassiveEuclid(float[] massive)
        {
            float normMatrix = 0;

            for (int i = 0; i < massive.Length; i++)
                normMatrix += massive[i] * massive[i];

            return (float)Math.Sqrt((double)normMatrix);
        }

        private float[] DifferenceMassives(float[] a, float[] b)
        {
            float[] answer = new float[a.Length];
            for (int i = 0; i < a.Length; i++)
                answer[i] = a[i] - b[i];
            return answer;
        }

        public float[] Approach()
        {
            float[] normMatrix = new float[3];
            normMatrix[0] = NormMatrixRows();
            normMatrix[1] = NormMatrixColumns();
            normMatrix[2] = NormMatrixEuclid(MatrixAlphaBeta);

            float normMinAlpha;

            float[] answersN = new float[Matrix.Height];
            float[] answersN1 = new float[Matrix.Height];
            float[] answersNorm = new float[Matrix.Height];
            float sumOfMultiMatrix;
            float normAnswersForEnd;



            if (normMatrix[0] <= 1 || normMatrix[1] <= 1 || normMatrix[2] <= 1)
            {
                normMinAlpha = normMatrix[2];

                for (int i = 0; i < Matrix.Height; i++)
                    answersN[i] = _matrixAlphaBeta[i, MatrixAlphaBeta.Width - 1];

                while (true)
                {
                    for (int i = 0; i < Matrix.Height; i++)
                    {
                        sumOfMultiMatrix = 0;

                        for (int j = 0; j < Matrix.Width - 1; j++)
                            sumOfMultiMatrix += _matrixAlphaBeta[i, j] * answersN[j];

                        answersN1[i] = _matrixAlphaBeta[i, MatrixAlphaBeta.Width - 1] + sumOfMultiMatrix;
                    }


                    normAnswersForEnd = NormMassiveEuclid(DifferenceMassives(answersN, answersN1));

                    if (normMinAlpha > 0.5)
                    {
                        if (normAnswersForEnd <= ((1 - normMinAlpha) / normMinAlpha * _accuracy))
                        {
                            for (int i = 0; i < Matrix.Height; i++)
                                answersList.Add(answersN1[i]);
                            return answersN1;
                        }
                    }
                    else
                    {
                        if (normAnswersForEnd <= _accuracy)
                        {
                            for (int i = 0; i < Matrix.Height; i++)
                                answersList.Add(answersN1[i]);
                            return answersN1;
                        }
                    }


                    for (int i = 0; i < Matrix.Height; i++)
                    {
                        answersN[i] = answersN1[i];
                        answersList.Add(answersN1[i]);
                    }
                }
            }
            else
                return answersN1;
        }

        public bool IsIterationsConverge()
        {
            float a1 = 0;
            float a2 = 0;

            for (int i = 0; i < Matrix.Height; i++)
                for (int j = 0; j < i; j++)
                    a1 += Math.Abs(Matrix[i, j]);

            for (int i = 0; i < Matrix.Height; i++)
                for (int j = i + 1; j < Matrix.Width - 1; j++)
                    a2 += Math.Abs(Matrix[i, j]);


            for (int i = 0; i < Matrix.Height; i++)
                for (int j = 0; j < Matrix.Width - 1; j++)
                    if (i == j && Math.Abs(Matrix[i, j]) <= a1 + a2)
                        return true;

            return false;
        }

    }
}
