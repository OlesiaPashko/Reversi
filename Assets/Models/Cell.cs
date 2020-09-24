
using System;

public class Cell
{
    public bool isBlack;
    private int x;
    private int y;
    public event Action CellChangedColor;
    public void ChangeColor()
    {
        isBlack = !isBlack;
        CellChangedColor?.Invoke();
    }

    public Cell(int x, int y, bool isBlack)
    {
        this.x = x;
        this.y = y;
        this.isBlack = isBlack;
    }

}
