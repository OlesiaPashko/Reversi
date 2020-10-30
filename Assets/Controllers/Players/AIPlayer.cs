using UnityEngine;

class AIPlayer : Player
{
    public AIPlayer(CellState color) : base(color) { }
    public override bool MakeMove()
    {
        //pass if there is no available moves
        var cells = GameManager.GetAvailableCells();
        if (cells.Count == 0)
        {
            GameManager.Pass();
            return true;
        }

        //make random move otherwise
        var randomIndex = (int)Random.Range(0, cells.Count);
        GameManager.MakeMove(cells[randomIndex]);
        return true;
    }

}
