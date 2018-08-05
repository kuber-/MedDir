using System;

namespace Web.Services
{
    public class PatientGroupsService : IPatientGroupsService
    {
        static readonly int[] xDelta = { -1, -1, -1, 0, 0, 1, 1, 1 };
        static readonly int[] yDelta = { -1, 0, 1, -1, 1, -1, 0, 1 };
        static readonly int numOfNeighbours = 8;

        public int Calculate(int[,] matrix)
        {
            var numberOfGroups = 0;

            if (matrix == null)
            {
                throw new ArgumentNullException();
            }

            var rows = matrix.GetUpperBound(0);
            var cols = matrix.GetUpperBound(1);
            var visited = new bool[rows + 1, cols + 1];

            for (var row = 0; row <= rows; row++)
            {
                for (var col = 0; col <= cols; col++)
                {
                    if (matrix[row, col] != 0 && matrix[row, col] != 1)
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(matrix),
                            $"Only 1s and 0s matrix values are supported"
                        );
                    }

                    if (!visited[row, col] && matrix[row, col] == 1)
                    {
                        numberOfGroups += 1;
                        Visit(matrix, rows, cols, row, col, visited);
                    }
                }
            }

            return numberOfGroups;
        }

        static void Visit(
            int[,] matrix,
            int rows,
            int cols,
            int row,
            int col,
            bool[,] visited
        )
        {
            visited[row, col] = true;

            for (var i = 0; i < numOfNeighbours; i++)
            {
                var x = row + xDelta[i];
                var y = col + yDelta[i];
                if (IsSafe(matrix, rows, cols, x, y, visited))
                {

                    Visit(matrix, rows, cols, x, y, visited);
                }
            }
        }

        static bool IsSafe(
            int[,] matrix,
            int rows,
            int cols,
            int row,
            int col,
            bool[,] visited)
        {
            return row >= 0 && col >= 0 &&
                row <= rows && col <= cols &&
                !visited[row, col] &&
                matrix[row, col] == 1;
        }

#if DEBUG
        static void Print<T>(T[,] matrix, int rows, int cols)
        {
            for (var row = 0; row <= rows; row++)
            {
                for (var col = 0; col <= cols; col++)
                {
                    Console.Write("{0} ", matrix[row, col]);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }
#endif
    }
}
