using Assets.Models;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

public class Field 
{
    public List<List<Cell>> Cells { get; private set; } = new List<List<Cell>>();
    public Field(Action CellChangedColor)
    {
        int size = 8;
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                Cells[i][j] = new Cell(CellState.Empty);
                Cells[i][j].CellChangedColor += CellChangedColor;
            }
        }

        SetInitialFieldState();
    }

    private void SetInitialFieldState()
    {
        Cells[3][3].State = CellState.White;
        Cells[4][4].State = CellState.White;
        Cells[3][4].State = CellState.Black;
        Cells[4][3].State = CellState.Black;
    }

    public void SetCell(CellState playerColor, Tuple<int, int> coords)
    {
        Cells[coords.Item1][coords.Item2].State = playerColor;
        UpdateField(playerColor, coords);
    }

    private void UpdateField(CellState playerColor, Tuple<int, int> coords)
    {
        UpdateHorizontally(playerColor, coords);
        UpdateVertically(playerColor, coords);
    }

    private void UpdateHorizontally(CellState playerColor, Tuple<int, int> coords)
    {
        List<Cell> row = new List<Cell>(Cells[coords.Item1]);
        UpdateHorizontalLeft(playerColor, row, coords.Item2);
        UpdateHorizontalRight(playerColor, row, coords.Item2);
    }

    private void UpdateVertically(CellState playerColor, Tuple<int, int> coords)
    {
        UpdateVerticalUp(playerColor, coords);
        UpdateVerticalDown(playerColor, coords);
    }

    private void UpdateVerticalUp(CellState playerColor, Tuple<int, int> coords)
    {
        int rowIndex = coords.Item1;
        int columnIndex = coords.Item2;
        for (int i = 0; i < rowIndex; i++)
        {
            Cell cell = Cells[i][columnIndex];
            if (cell.State == CellState.Empty && CanGoUp(rowIndex, i, playerColor, columnIndex))
            {
                for (int paintIndex = i + 1; paintIndex < rowIndex; paintIndex++)
                {
                    Cells[paintIndex][columnIndex].ChangeColor();
                }
            }
        }
    }

    private void UpdateVerticalDown(CellState playerColor, Tuple<int, int> coords)
    {
        int rowIndex = coords.Item1;
        int columnIndex = coords.Item2;
        CellState oppositeColor = GetOppositeColor(playerColor);
        for (int i = rowIndex; i < Cells.Count; i++)
        {
            Cell cell = Cells[i][columnIndex];
            if (cell.State == oppositeColor)
            {
                int paintIndex = FromWhereGoDown(playerColor, columnIndex, i, playerColor);
                if (paintIndex >= 0)
                {
                    paintIndex++;
                    for (; paintIndex < rowIndex; paintIndex++)
                    {
                        Cells[paintIndex][columnIndex].ChangeColor();
                    }
                }
            }
        }
    }

    private void UpdateHorizontalLeft(CellState playerColor, List<Cell> row, int columnIndex)
    {
        for(int i = 0; i < columnIndex; i++)
        {
            if(row[i].State == playerColor&& CanGoLeft(i, columnIndex, row, playerColor))
            {
                for(int paintIndex = i+1; paintIndex < columnIndex; paintIndex++)
                {
                    row[paintIndex].ChangeColor();
                }
            }
        }
    }

    private void UpdateHorizontalRight(CellState playerColor, List<Cell> row, int columnIndex)
    {
        for (int i = columnIndex; i < row.Count; i++)
        {
            if (row[i].State == playerColor)
            {
                int indexOfPainting = FromWhereGoRight(i, row, columnIndex, playerColor);
                if (indexOfPainting>=0)
                {
                    for (; indexOfPainting < columnIndex; indexOfPainting++)
                    {
                        row[indexOfPainting].ChangeColor();
                    }
                }
            }
        }
    }

    public void ChangeColorOfAllCells()
    {
        foreach(var cellsRow in Cells)
        {
            foreach(var cell in cellsRow)
            {
                cell.ChangeColor();
            }
        }
    }

    private CellState GetOppositeColor(CellState color)
    {
        if(color == CellState.Black)
        {
            return CellState.White;
        }
        else if(color == CellState.White)
        {
            return CellState.Black;
        }
        return CellState.Empty;
    }

    //returns collection of coordinates (row and column in cells)
    public List<Tuple<int, int>> GetAvailableCells(CellState playerColor)
    {
        List<Tuple<int, int>> availableCells = new List<Tuple<int, int>>();
        for(int i = 0; i < Cells.Count; i++)
        {
            for(int j = 0; j < Cells[i].Count; j++)
            {
                Cell cell = Cells[i][j];
                if (cell.State == playerColor)
                {
                    availableCells.AddRange(GetAllAvailableFromCell(playerColor, i, j));
                }
            }
        }
        return availableCells;
    }

    public List<Tuple<int, int>> GetAllAvailableFromCell(CellState playerColor, int rowIndex, int columnIndex)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        result.AddRange(GetAvailableHorizontally(playerColor, rowIndex, columnIndex));
        result.AddRange(GetAvailableVertically(playerColor, rowIndex, columnIndex));
        return result;
    }

    private List<Tuple<int,int>> GetAvailableHorizontally(CellState playerColor, int rowIndex, int columnIndex)
    {
        List<Tuple<int, int>> availableCells = new List<Tuple<int, int>>();
        List<Cell> row = Cells[rowIndex];
        List<Tuple<int, int>> availableRight = GoRightAndFindAvailable(playerColor, row, columnIndex);
        List<Tuple<int, int>> availableLeft = GoLeftAndFindAvailable(playerColor, row, columnIndex);
        availableCells.AddRange(availableLeft);
        availableCells.AddRange(availableRight);
        return availableCells;
    }

    private List<Tuple<int, int>> GoLeftAndFindAvailable(CellState playerColor, List<Cell> row, int columnIndex)
    {
        var result = new List<Tuple<int, int>>();

        for (int i = 0; i < columnIndex; i++)
        {
            if (row[i].State == CellState.Empty && CanGoLeft(i, columnIndex, row, playerColor))
            {
                 var cell = new Tuple<int, int>(i, columnIndex);
                 result.Add(cell);
            }
        }
        return result;
    }

    private bool CanGoLeft(int i, int columnIndex, List<Cell> row, CellState color)
    {
        bool canGo = true;
        for (int j = i + 1; j < columnIndex; j++)
        {
            if (row[j].State != GetOppositeColor(color))
            {
                canGo = false;
                break;
            }
        }
        return canGo;
    }

    private List<Tuple<int, int>> GoRightAndFindAvailable(CellState playerColor, List<Cell> row, int columnIndex)
    {
        CellState oppositeColor = GetOppositeColor(playerColor);
        var result = new List<Tuple<int, int>>();

        for (int i = columnIndex+1; i < row.Count; i++)
        {
            if (row[i].State == oppositeColor)
            {
                int indexOfAvailableCell = FromWhereGoRight(i, row, columnIndex, playerColor);

                if (indexOfAvailableCell>=0)
                {
                    var cell = new Tuple<int, int>(indexOfAvailableCell, columnIndex);
                    result.Add(cell);
                }
            }
        }
        return result;
    }

    private int FromWhereGoRight(int i, List<Cell> row, int columnIndex, CellState color)
    {
        int j = i + 1;
        for (; j < columnIndex; j++)
        {
            if (row[j].State == color)
            {
                return j;
            }
            if (row[j].State == CellState.Empty)
            {
                return -1;
            }
        }
        return -1;
    }

    private List<Tuple<int, int>> GetAvailableVertically(CellState playerColor, int rowIndex, int columnIndex)
    {
        List<Tuple<int, int>> availableCells = new List<Tuple<int, int>>();
        List<Tuple<int, int>> availableUp = GoUpAndFindAvailable(playerColor, rowIndex, columnIndex);
        List<Tuple<int, int>> availableDown = GoDownAndFindAvailable(playerColor, rowIndex, columnIndex);
        availableCells.AddRange(availableUp);
        availableCells.AddRange(availableDown);
        return availableCells;
    }

    private List<Tuple<int, int>> GoUpAndFindAvailable(CellState playerColor, int rowIndex, int columnIndex)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        for (int i = 0; i < rowIndex; i++)
        {
            Cell cell = Cells[i][columnIndex];
            if (cell.State == CellState.Empty && CanGoUp(rowIndex, i, playerColor, columnIndex))
            {
                result.Add(new Tuple<int, int>(i, columnIndex));
            }
        }
        return result;
    }

    private bool CanGoUp(int rowIndex, int i, CellState playerColor, int columnIndex)
    {
        CellState oppositeColor = GetOppositeColor(playerColor);
        bool isAvailable = true;
        for (int j = i + 1; j < rowIndex; j++)
        {
            if (Cells[j][columnIndex].State != oppositeColor)
            {
                isAvailable = false;
                break;
            }
        }
        return isAvailable;
    }
    private List<Tuple<int, int>> GoDownAndFindAvailable(CellState playerColor, int rowIndex, int columnIndex)
    {
        CellState oppositeColor = GetOppositeColor(playerColor);
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        for (int i = rowIndex; i < Cells.Count; i++)
        {
            Cell cell = Cells[i][columnIndex];
            if (cell.State == oppositeColor)
            {
                int index = FromWhereGoDown(playerColor, columnIndex, i, CellState.Empty);
                if (index>=0)
                {
                    result.Add(new Tuple<int, int>(index, columnIndex));
                }
            }
        }
        return result;
    }

    public int FromWhereGoDown(CellState playerColor, int columnIndex, int i, CellState colorToStop)
    {
        bool isAvailable = true;
        int j = i + 1;
        for (; j < Cells.Count; j++)
        {
            if (Cells[j][columnIndex].State == colorToStop)
            {
                return j;
            }
            if (Cells[j][columnIndex].State == playerColor)
            {
                return -1;
            }
        }
        return -1;
    }


    private List<Tuple<int, int>> GetAvailableDiagonal(CellState playerColor, int rowIndex, int columnIndex)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        result.AddRange(GoInMainDiagonalToFindAvailable(playerColor, rowIndex, columnIndex));
    }

    private List<Tuple<int, int>> GoInAntidiagonalToFindAvailable(CellState playerColor, int rowIndex, int columnIndex)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        int i;
        int j;
        result.AddRange(GetAvailableInFirstPartOfAntidiagonal(out i, out j, rowIndex, columnIndex, playerColor));
        i++;
        j++;
        result.AddRange(GetAvailableInSecondPartOfAntidiagonal(i, j, playerColor));
        return result;
    }

    private List<Tuple<int, int>> GoInMainDiagonalToFindAvailable(CellState playerColor, int rowIndex, int columnIndex)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        int i;
        int j;
        result.AddRange(GetAvailableInFirstPartOfMainDiagonal(out i, out j, rowIndex, columnIndex, playerColor));
        i++;
        j++;
        result.AddRange(GetAvailableInSecondPartOfMainDiagonal(i, j, playerColor));
        return result;
    }

    public List<Tuple<int,int>> GetAvailableInFirstPartOfMainDiagonal(out int i, out int j, int rowIndex, int columnIndex, CellState playerColor)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        Tuple<int, int> startIndexes = GetIndexesToStart(rowIndex, columnIndex);
        i = startIndexes.Item1;
        j = startIndexes.Item2;
        for (; i < rowIndex && j < columnIndex; i++, j++)
        {
            if (Cells[i][j].State == CellState.Empty)
            {
                bool isAvailable = true;
                for (int innerI = i, innerJ = j; innerI < rowIndex && innerJ < columnIndex; innerI++, innerJ++)
                {
                    if (Cells[innerI][innerJ].State != GetOppositeColor(playerColor))
                    {
                        isAvailable = false;
                        break;
                    }
                }
                if (isAvailable)
                {
                    result.Add(new Tuple<int, int>(i, j));
                }
            }
        }
        return result;
    }

    private List<Tuple<int, int>> GetAvailableInSecondPartOfMainDiagonal(int i, int j, CellState playerColor)
    {
        List<Tuple<int, int>> result = new List<Tuple<int, int>>();
        for (; i < Cells.Count && j < Cells.Count; i++, j++)
        {
            if (Cells[i][j].State == GetOppositeColor(playerColor))
            {
                bool isAvailable = true;
                for (int innerI = i, innerJ = j; innerI < Cells.Count && innerJ < Cells.Count; innerI++, innerJ++)
                {
                    if (Cells[innerI][innerJ].State != GetOppositeColor(playerColor))
                    {
                        isAvailable = false;
                        break;
                    }
                    if (Cells[innerI][innerJ].State == CellState.Empty)
                        break;
                }
                if (isAvailable)
                {
                    result.Add(new Tuple<int, int>(i, j));
                }
            }
        }
        return result;
    }

    private Tuple<int,int> GetIndexesToStart(int rowIndex, int columnIndex)
    {
        if (rowIndex > columnIndex)
        {
            return new Tuple<int, int>(rowIndex - columnIndex, 0);
        }
        if (columnIndex > rowIndex)
        {
            return new Tuple<int, int>(0, columnIndex - rowIndex);
        }
        return new Tuple<int, int>(0, 0);
    }

}
