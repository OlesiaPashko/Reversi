using System;

public enum CellState
{
    Black,
    White,
    Empty
}
public class Cell
{
    public CellState State;
    public event Action CellChangedColor;
    public void ChangeColor()
    {
        if (State == CellState.Black)
            State = CellState.White;
        else
            State = CellState.Black;
        CellChangedColor?.Invoke();
    }

    public Cell(CellState state)
    {
        this.State = state;
    }

}
