using UnityEngine;

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
