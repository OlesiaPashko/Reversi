using System;

public enum CellState
{
    Black,
    White,
    Empty
}
public class Cell
{
    public CellState State { get; set; }
    
    public Cell(CellState state)
    {
        this.State = state;
    }

}
