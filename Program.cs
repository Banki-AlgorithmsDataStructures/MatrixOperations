namespace Matrices
{
	internal class Program
	{
		/// <summary>
		/// Entry point of the application that demonstrates matrix operations including
		/// creation, display, addition, and determinant calculation.
		/// </summary>
		/// <param name="args">Command line arguments (not used in this implementation).</param>
		/// <remarks>
		/// This method performs the following operations:
		/// <list type="number">
		/// <item>Creates two random 3x3 matrices (a and b) with values between 10 and 100.</item>
		/// <item>Displays both matrices to the console.</item>
		/// <item>Attempts to add the matrices and displays the result if successful.</item>
		/// <item>Creates a predefined 3x3 matrix (d) with specific values.</item>
		/// <item>Calculates and displays the determinant of matrix d using Gaussian elimination.</item>
		/// </list>
		/// </remarks>
		static void Main(string[] args)
		{
			// rows+columns
			double[,] a = CreateRandomMatrix(3,3);
			DisplayMatrix(a, "a");
			double[,] b = CreateRandomMatrix(3, 3);
			DisplayMatrix(b, "b");
			if (AddMatrices(a, b, out double[,] c) == false)
				Console.WriteLine("Matrices cannot be added!");
			else
				DisplayMatrix(c, "a+b");

			double[,] d = { { 4, 2, 1 }, 
				              { 3, 5, 2 }, 
											{ 1, 1, 3 } };
      DisplayMatrix(d, "d");
			if (!GetDeterminant(d, out double det))
				Console.WriteLine("The matrix is not a square matrix!");
			else
			{
				Console.WriteLine($"\ndet(d)={det,7:F2}");
      }
		}

		/// <summary>
		/// Calculates the determinant of a square matrix using Gaussian elimination method.
		/// </summary>
		/// <param name="a">The input matrix for which to calculate the determinant.</param>
		/// <param name="det">Output parameter that receives the calculated determinant value.</param>
		/// <returns>
		/// Returns <c>true</c> if the determinant was successfully calculated; 
		/// <c>false</c> if the matrix is not square.
		/// </returns>
		/// <remarks>
		/// This method uses Gaussian elimination to transform the matrix into upper triangular form.
		/// The determinant is calculated as the product of all pivot elements.
		/// The original matrix is not modified; a clone is used for calculations.
		/// Time complexity: O(n³) where n is the number of rows/columns.
		/// </remarks>
		private static bool GetDeterminant(double[,] a, out double det)
		{	det = 0;
			int rows= a.GetLength(0);
			int cols= a.GetLength(1);
      if (rows != cols)
				return false;
			double[,] c=(double[,])a.Clone();
			det = 1;
			for (int i = 0; i < rows; i++)
			{
				double pivot = c[i, i];
				det = det * pivot;
				if (det == 0)
					break;
				for (int j = i; j < cols; j++)
					c[i, j] = c[i, j] / pivot;
        for (int k = i + 1; k < rows; k++)
        {
          double m = -c[k, i];
          for (int j = i; j < cols; j++)
          {
            c[k, j] = c[k, j] + m* c[i, j]; ;
          }
        }
      }
			det = det*c[rows -1,cols-1];
			return true;	
		}

		/// <summary>
		/// Adds two matrices element-wise and returns the result.
		/// </summary>
		/// <param name="a">The first matrix to add.</param>
		/// <param name="b">The second matrix to add.</param>
		/// <param name="c">Output parameter that receives the resulting matrix from the addition.</param>
		/// <returns>
		/// Returns <c>true</c> if the matrices were successfully added; 
		/// <c>false</c> if the matrices have different dimensions and cannot be added.
		/// </returns>
		/// <remarks>
		/// Matrix addition requires both matrices to have the same dimensions (same number of rows and columns).
		/// The resulting matrix has the same dimensions as the input matrices.
		/// Each element c[i,j] = a[i,j] + b[i,j].
		/// Time complexity: O(m×n) where m is rows and n is columns.
		/// </remarks>
		private static bool AddMatrices(
			double[,] a, double[,] b, out double[,] c)
		{
			int rows=a.GetLength(0);	
			int cols=a.GetLength(1);
			c = new double[rows, cols];
			if (rows != b.GetLength(0)||cols != b.GetLength(1))
				return false;
			for (int i = 0; i < rows; i++)
				for (int j = 0; j < cols; j++)
					c[i,j] = a[i,j] + b[i,j];
			return true;
		}

		/// <summary>
		/// Displays a matrix to the console in tabular format.
		/// </summary>
		/// <param name="a">The matrix to display.</param>
		/// <param name="name">The name to display before the matrix.</param>
		/// <remarks>
		/// Each element is displayed with 7 characters width and 2 decimal places (F2 format).
		/// The matrix is displayed in row-major order with each row on a separate line.
		/// The output format is: name= followed by the matrix elements.
		/// </remarks>
		private static void DisplayMatrix(double[,] a, string name)
		{
			Console.WriteLine("\n"+name+"= ");
			for (int i = 0; i < a.GetLength(0); i++)
			{
				for (int j = 0; j < a.GetLength(1); j++)
				{
					Console.Write($"{a[i,j],7:F2}");
				}
				Console.WriteLine();
			}
		}

    /// <summary>
    /// Creates a matrix with random double values from the interval [10,100).
    /// </summary>
    /// <param name="rows">The number of rows in the matrix.</param>
    /// <param name="columns">The number of columns in the matrix.</param>
    /// <returns>
    /// A two-dimensional array of doubles with the specified dimensions,
    /// populated with random values between 10 and 100 (exclusive of 100).
    /// </returns>
    /// <remarks>
    /// Random values are generated using the Random class.
    /// The range transformation: [0,1) → [10,100) is achieved by: value = random * 90 + 10.
    /// </remarks>
    private static double[,] CreateRandomMatrix(int rows, int columns)
		{
			double[,] matrix=new double[rows, columns];
			Random rnd= new Random();
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{  // [0,1)-->[10,100)
					matrix[i,j]= rnd.NextDouble()*90+10;
				}
			}
			return matrix;
		}
	}
}
