using Assets.Models;

public abstract class Player
{
    public CellState Color { get; set; }

    public GameManager GameManager;

    public Player(CellState color)
    {
        Color = color;
    }
    public abstract bool MakeMove();
}


