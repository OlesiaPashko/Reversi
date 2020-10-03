using Assets.Models;
using UnityEngine;

public abstract class Player:MonoBehaviour
{
    public CellState Color { get; set; }

    public GameManager GameManager;

    public Player(CellState color)
    {
        Color = color;
    }
    public abstract bool MakeMove();
}
class HumanPlayer : Player
{
    public HumanPlayer(CellState color) : base(color) { }
    public override bool MakeMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

             return ClickIfCellIsAvailable(worldPoint);
        }
        return false;
    }

    private bool ClickIfCellIsAvailable(Vector3 point)
    {
        float topLeftX = -5.75f;
        float topLeftY = 5.65f;
        int x = Mathf.RoundToInt(((point.x - topLeftX) / 1.25f) - 1);
        int y = Mathf.RoundToInt(((topLeftY - point.y) / 1.25f) - 1);
        Debug.Log("x = " + x);
        Debug.Log("y = " + y);
        var cells = GameManager.GetAvailableCells();
        foreach (var cell in cells)
        {
            if (cell.Item1 == y && cell.Item2 == x)
            {
                GameManager.MakeMove(cell);
                GameManager.CalculatePlayersScore();
                return true;
            }
        }
        return false;
    }

    
}
class AIPlayer : Player
{
    public AIPlayer(CellState color) : base(color) { }
    public override bool MakeMove()
    {
        var cells = GameManager.GetAvailableCells();
        var randomIndex = (int)Random.Range(0, cells.Count);
        GameManager.MakeMove(cells[randomIndex]);
        return true;
    }
}
