using System;
using UnityEngine;

namespace Sudocu
{
    public class SelectedCellModel
    {
        public event Action ChangedSelectedCell = () => { };

        public Vector2Int SelectedCell { get; private set; }

        public void SetSelectedCell(Vector2Int cell)
        {
            SelectedCell = cell;
            ChangedSelectedCell.Invoke();
        }
    }
}
