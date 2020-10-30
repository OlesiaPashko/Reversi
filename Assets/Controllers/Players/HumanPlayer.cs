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
        //pass if there is no available moves
        if(GameManager.GetAvailableCells().Count == 0) 
        {
            GameManager.Pass();
            return true;
        }

        //make move by mouse
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            return ClickIfCellIsAvailable(worldPoint);
        }
        return false;
    }

    private bool ClickIfCellIsAvailable(Vector3 point)
    {
        //get cell x and y in range (0, 0) -> (7, 7)
        float topLeftX = -5.75f;
        float topLeftY = 5.65f;
        int x = Mathf.RoundToInt(((point.x - topLeftX) / 1.25f) - 1);
        int y = Mathf.RoundToInt(((topLeftY - point.y) / 1.25f) - 1);

        //if this cell is available - make move
        var cells = GameManager.GetAvailableCells();
        foreach (var cell in cells)
        {
            if (cell.Item1 == y && cell.Item2 == x)
            {
                GameManager.MakeMove(cell);
                return true;
            }
        }
        return false;
    }

}
