
using System;
using System.Collections.Generic;

public class Field 
{
    private List<Cell> cells;
    public Field(Action CellChangedColor)
    {
        Cell cell1 = new Cell(0, 0, false);
        Cell cell2 = new Cell(0, 1, false);
        Cell cell3 = new Cell(1, 0, false);
        cell1.CellChangedColor += CellChangedColor;
        cell2.CellChangedColor += CellChangedColor;
        cell3.CellChangedColor += CellChangedColor;

        cells = new List<Cell> { cell1, cell2, cell3 };
    }

    public List<Cell> GetCells()
    {
        return cells;
    }

    public void ChangeColorOfAllCells()
    {
        foreach(var cell in cells)
        {
            cell.ChangeColor();
        }
    }
}
