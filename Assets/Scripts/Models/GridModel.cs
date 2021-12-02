using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sudocu
{
    public class GridModel
    {
        private int[,] _originGrid;
        private int[,] _mixedGrid;
        private int[,] _erasedGrid;
        private bool[,] _accessGrid;
        private int[,] _activeGrid;

        public GridModel(int[,] origin, int maxValue, int columnsAreasCount, int columnsAreasSize, int rowsAreasCount, int rowsAreasSize, int controlSum)
        {
            _originGrid = origin;
            MaxValue = maxValue;
            ColumnsAreasCount = columnsAreasCount;
            ColumnsAreaSize = columnsAreasSize;
            RowsAreasCount = rowsAreasCount;
            RowsAreaSize = rowsAreasSize;
            ControlSum = controlSum;
        }

        public event Action ChangeActiveGridEvent = () => { };

        public event Action EndEvent = () => { };

        public event Action<int, int> ChangeActiveGridMemberEvent = (arg1, arg2) => { };

        public int MaxValue { get; }

        public int ColumnsAreasCount { get; }

        public int ColumnsAreaSize { get; }

        public int RowsAreasCount { get; }

        public int RowsAreaSize { get; }

        public int ControlSum { get; }

        public void GenerateGrid()
        {
            _mixedGrid = _originGrid.GetRandomGrid(ColumnsAreasCount, ColumnsAreaSize, RowsAreasCount, RowsAreaSize);
            _erasedGrid = _mixedGrid.GetErasedGrid(ColumnsAreasCount, ColumnsAreaSize, RowsAreasCount, RowsAreaSize);
            _accessGrid = _erasedGrid.GetAccessGrid();
            SetActiveGrid(_erasedGrid.GetCopy());
        }

        public void ResetActiveGrid()
        {
            SetActiveGrid(_erasedGrid.GetCopy());
        }

        public void IncreaseActiveGridMember(int x, int y)
        {
            if (_accessGrid[y, x])
            {
                int value = _activeGrid[y, x];
                value++;
                value = value > MaxValue ? 0 : value;
                _activeGrid[y, x] = value;
                ChangeActiveGridMemberEvent.Invoke(x, y);
                CheckEnd();
            }
        }

        public void SetActiveGridMember(int x, int y, int value)
        {
            if (_accessGrid[y, x])
            {
                value = value > MaxValue ? 0 : value;
                _activeGrid[y, x] = value;
                ChangeActiveGridMemberEvent.Invoke(x, y);
                CheckEnd();
            }
        }

        public void ShowRandomMember()
        {
            List<Vector2Int> points = new List<Vector2Int>();

            for (int i = 0; i < _erasedGrid.GetLength(0); i++)
            {
                for (int j = 0; j < _erasedGrid.GetLength(1); j++)
                {
                    if (_activeGrid[i, j] != _mixedGrid[i, j] && _accessGrid[i, j])
                    {
                        points.Add(new Vector2Int(i, j));
                    }
                }
            }

            if (points.Count > 0)
            {
                int index = Random.Range(0, points.Count);
                Vector2Int point = points[index];
                int x = point.x;
                int y = point.y;
                _activeGrid[x, y] = _mixedGrid[x, y];
                ChangeActiveGridMemberEvent.Invoke(y, x);
                CheckEnd();
            }
        }

        public int[,] GetActiveGrid() => _activeGrid.GetCopy();

        public bool[,] GetAccessGrid() => _accessGrid.GetCopy();

        private void SetActiveGrid(int[,] activeGrid)
        {
            _activeGrid = activeGrid;
            ChangeActiveGridEvent.Invoke();
        }

        private void CheckEnd()
        {
            bool step1 = CheckColumns();
            bool step2 = CheckRows();
            bool step3 = CheckAreas();

            if (step1 && step2 && step3)
            {
                EndEvent.Invoke();
            }
        }

        private bool CheckColumns()
        {
            bool isEnd = true;

            for (int i = 0; i < _mixedGrid.GetLength(0); i++)
            {
                int sum = 0;

                for (int j = 0; j < _mixedGrid.GetLength(1); j++)
                {
                    sum += _activeGrid[i, j];
                }

                if (sum != ControlSum)
                {
                    isEnd = false;
                    break;
                }
            }

            return isEnd;
        }

        private bool CheckRows()
        {
            bool isEnd = true;

            for (int j = 0; j < _mixedGrid.GetLength(0); j++)
            {
                int sum = 0;

                for (int i = 0; i < _mixedGrid.GetLength(1); i++)
                {
                    sum += _activeGrid[i, j];
                }

                if (sum != ControlSum)
                {
                    isEnd = false;
                    break;
                }
            }

            return isEnd;
        }

        private bool CheckAreas()
        {
            bool isEnd = true;

            int[,] sums = new int[RowsAreasCount, ColumnsAreasCount];

            for (int i = 0; i < _mixedGrid.GetLength(1); i++)
            {
                for (int j = 0; j < _mixedGrid.GetLength(0); j++)
                {
                    int x = Mathf.FloorToInt(j / ColumnsAreaSize);
                    int y = Mathf.FloorToInt(i / RowsAreaSize);
                    sums[y, x] += _activeGrid[i, j];
                }
            }

            for (int i = 0; i < sums.GetLength(0); i++)
            {
                for (int j = 0; j < sums.GetLength(1); j++)
                {
                    if (sums[i, j] != ControlSum)
                    {
                        isEnd = false;
                        break;
                    }
                }
            }

            return isEnd;
        }
    }
}
