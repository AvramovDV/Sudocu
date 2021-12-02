using UnityEngine;

namespace Sudocu
{
    public static class ArrayExtensions
    {
        private static float _perscentOfHidden = 0.7f;
        private static int _minSteps = 50;
        private static int _maxSteps = 100;
        private static int _methodsCount = 5;

        public static int[,] GetCopy(this int[,] array)
        {
            int[,] result = new int[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[i, j] = array[i, j];
                }
            }

            return result;
        }

        public static bool[,] GetCopy(this bool[,] array)
        {
            bool[,] result = new bool[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[i, j] = array[i, j];
                }
            }

            return result;
        }

        public static int[,] GetErasedGrid(this int[,] origin, int columnsAreasCount, int columnsAreasSize, int rowsAreasCount, int rowsAreasSize)
        {
            int[,] result = origin.GetCopy();

            int count = Mathf.CeilToInt(columnsAreasCount * columnsAreasSize * rowsAreasCount * rowsAreasSize * _perscentOfHidden);

            for (int i = 0; i < count; i++)
            {
                int y = Random.Range(0, result.GetLength(0));
                int x = Random.Range(0, result.GetLength(1));
                result[x, y] = 0;
            }

            return result;
        }

        public static bool[,] GetAccessGrid(this int[,] erasedGrid)
        {
            int x = erasedGrid.GetLength(0);
            int y = erasedGrid.GetLength(1);

            bool[,] result = new bool[x, y];

            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    result[i, j] = erasedGrid[i, j] == 0;
                }
            }

            return result;
        }

        public static int[,] GetRandomGrid(this int[,] origin, int columnsAreaCount, int columnsAreaSize, int rowsAreasCount, int rowsAreasSize)
        {
            int[,] result = origin.GetCopy();

            int steps = Random.Range(_minSteps, _maxSteps);

            for (int i = 0; i < steps; i++)
            {
                int method = Random.Range(0, _methodsCount);
                if ((result.GetLength(0) == 6 && method != 0) || result.GetLength(0) != 6)
                {
                    switch (method)
                    {
                        case 0:
                            result = Transpose(result);
                            break;
                        case 1:
                            result = ChangeRandomColumnAreas(result, columnsAreaCount, columnsAreaSize);
                            break;
                        case 2:
                            result = ChangeRandomRowsAreas(result, rowsAreasCount, rowsAreasSize);
                            break;
                        case 3:
                            result = ChangeRandomColumnsInRandomArea(result, columnsAreaCount, columnsAreaSize);
                            break;
                        case 4:
                            result = ChangeRandomRowsInRandomArea(result, rowsAreasCount, rowsAreasSize);
                            break;
                        default:
                            break;
                    }
                }
            }

            return result;
        }

        private static int[,] Transpose(int[,] array)
        {
            int[,] result = new int[array.GetLength(0), array.GetLength(1)];

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[i, j] = array[j, i];
                }
            }

            return result;
        }

        private static int[,] ChangeRandomColumnAreas(this int[,] array, int columnsAreasCount, int columnsAreasSize)
        {
            int[,] result = array.GetCopy();

            int area1 = Random.Range(0, columnsAreasCount);
            int area2 = Random.Range(0, columnsAreasCount);

            if (area1 != area2)
            {
                result = ChangeColumnsAreas(result, area1, area2, columnsAreasSize);
            }

            return result;
        }

        private static int[,] ChangeRandomRowsAreas(this int[,] array, int rowsAreasCount, int rowsAreasSize)
        {
            int[,] result = array.GetCopy();

            int area1 = Random.Range(0, rowsAreasCount);
            int area2 = Random.Range(0, rowsAreasCount);

            if (area1 != area2)
            {
                result = ChangeRowsAreas(result, area1, area2, rowsAreasSize);
            }

            return result;
        }

        private static int[,] ChangeRandomColumnsInRandomArea(this int[,] array, int columnsAreasCount, int columnsAreasSize)
        {
            int[,] result = array.GetCopy();

            int area = Random.Range(0, columnsAreasCount);
            int column1 = Random.Range(0, columnsAreasSize) + (area * columnsAreasSize);
            int column2 = Random.Range(0, columnsAreasSize) + (area * columnsAreasSize);

            if (column1 != column2)
            {
                result = ChangeColumns(result, column1, column2);
            }

            return result;
        }

        private static int[,] ChangeRandomRowsInRandomArea(this int[,] array, int rowsAreasCount, int rowsAreasSize)
        {
            int[,] result = array.GetCopy();

            int area = Random.Range(0, rowsAreasCount);
            int row1 = Random.Range(0, rowsAreasSize) + (area * rowsAreasSize);
            int row2 = Random.Range(0, rowsAreasSize) + (area * rowsAreasSize);

            if (row1 != row2)
            {
                result = ChangeRows(result, row1, row2);
            }

            return result;
        }

        private static int[,] ChangeColumnsAreas(this int[,] array, int area1, int area2, int areaSize)
        {
            int[,] result = array.GetCopy();

            for (int i = 0; i < areaSize; i++)
            {
                int column1 = i + (Mathf.Min(area1, area2) * areaSize);
                int column2 = i + (Mathf.Max(area1, area2) * areaSize);
                result = ChangeColumns(result, column1, column2);
            }

            return result;
        }

        private static int[,] ChangeRowsAreas(this int[,] array, int area1, int area2, int areaSize)
        {
            int[,] result = array.GetCopy();

            for (int i = 0; i < areaSize; i++)
            {
                int row1 = i + (Mathf.Min(area1, area2) * areaSize);
                int row2 = i + (Mathf.Max(area1, area2) * areaSize);
                result = ChangeRows(result, row1, row2);
            }

            return result;
        }

        private static int[,] ChangeColumns(this int[,] array, int column1, int column2)
        {
            int[,] result = array.GetCopy();

            for (int i = 0; i < array.GetLength(0); i++)
            {
                result[i, column1] = array[i, column2];
                result[i, column2] = array[i, column1];
            }

            return result;
        }

        private static int[,] ChangeRows(this int[,] array, int row1, int row2)
        {
            int[,] result = array.GetCopy();

            for (int i = 0; i < array.GetLength(1); i++)
            {
                result[row1, i] = array[row2, i];
                result[row2, i] = array[row1, i];
            }

            return result;
        }
    }
}
