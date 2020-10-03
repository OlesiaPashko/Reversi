using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
