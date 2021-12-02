using System.Collections.Generic;

namespace Sudocu
{
    public class GameModel
    {
        public Dictionary<GridType, GridModel> GridDict { get; } = new Dictionary<GridType, GridModel>()
        {
            [GridType.Small] = new GridModel(
                origin: new int[4, 4]
                {
                    { 1, 2, 3, 4 },
                    { 3, 4, 1, 2 },
                    { 4, 1, 2, 3 },
                    { 2, 3, 4, 1 },
                },
                maxValue: 4,
                columnsAreasCount: 2,
                columnsAreasSize: 2,
                rowsAreasCount: 2,
                rowsAreasSize: 2,
                controlSum: 10),
            [GridType.Medium] = new GridModel(
                origin: new int[6, 6]
                {
                    { 1, 2, 3, 4, 5, 6 },
                    { 4, 5, 6, 1, 2, 3 },
                    { 2, 3, 4, 5, 6, 1 },
                    { 5, 6, 1, 2, 3, 4 },
                    { 3, 4, 5, 6, 1, 2 },
                    { 6, 1, 2, 3, 4, 5 },
                },
                maxValue: 6,
                columnsAreasCount: 2,
                columnsAreasSize: 3,
                rowsAreasCount: 3,
                rowsAreasSize: 2,
                controlSum: 21),
            [GridType.Big] = new GridModel(
                origin: new int[9, 9]
                {
                    { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                    { 7, 8, 9, 1, 2, 3, 4, 5, 6 },
                    { 4, 5, 6, 7, 8, 9, 1, 2, 3 },
                    { 2, 3, 4, 5, 6, 7, 8, 9, 1 },
                    { 5, 6, 7, 8, 9, 1, 2, 3, 4 },
                    { 8, 9, 1, 2, 3, 4, 5, 6, 7 },
                    { 3, 4, 5, 6, 7, 8, 9, 1, 2 },
                    { 6, 7, 8, 9, 1, 2, 3, 4, 5 },
                    { 9, 1, 2, 3, 4, 5, 6, 7, 8 },
                },
                maxValue: 9,
                columnsAreasCount: 3,
                columnsAreasSize: 3,
                rowsAreasCount: 3,
                rowsAreasSize: 3,
                controlSum: 45),
        };
    }
}
