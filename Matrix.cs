namespace vychmat1_2
{
    /// <summary>
    /// Класс двухмерных матриц.
    /// </summary>
    public class Matrix : ICloneable
    {
        private int _rows;
        public int Rows { get { return _rows; } set { _rows = value; } }
        private int _columns;
        public int Columns { get { return _columns; } set { _columns = value; } }
        public float[,] values;


        public Matrix(int size)
        {
            _rows = size;
            _columns = size;
            values = new float[size, size];    // квадратная
        }

        public Matrix(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            values = new float[_rows, _columns];    // строки, столбцы
        }

        public Matrix(float[] vector)
        {
            _rows = 1;
            _columns = vector.Length;
            values = new float[1, _columns];
            for (int i = 0; i < _columns; i++)
                values[0, i] = vector[i];
        }

        public Matrix(float[,] vector)
        {
            _rows = vector.GetUpperBound(0) + 1;
            _columns = vector.GetUpperBound(1) + 1;
            //values = new float[_rows, _columns];
            values = (float[,])vector.Clone();
        }


        public float this[int index1, int index2]
        {
            get { return values[index1, index2]; }
            set { values[index1, index2] = value; }
        }

        public object Clone()
        {
            Matrix matrix = new Matrix(this.Rows, this.Columns);
            for (int i = 0; i < this.Rows; i++)
                for (int j = 0; j < this.Columns; j++)
                    matrix[i,j] = this[i,j];
            return matrix;
        }
    }
}
