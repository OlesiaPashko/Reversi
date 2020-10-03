using Assets.Models;
using UnityEditor.PackageManager;
using UnityEngine;

public class CellsController : MonoBehaviour
{
    public static CellState firstPlayerColor;

    public static bool IsTwoPlayers;

    public GameObject FirstPlayerCellExample;

    public GameObject SecondPlayerCellExample;
    public GameManager GameManager { get; set; }
    public void StartGame()
    {
        SetPlayersColors();
        GameManager.StartGame(IsTwoPlayers, firstPlayerColor);
    }

    
    private void SetPlayersColors()
    {
        if(firstPlayerColor == CellState.Black)
        {
            FirstPlayerCellExample.GetComponent<SpriteRenderer>().material.color = Color.black;
            SecondPlayerCellExample.GetComponent<SpriteRenderer>().material.color = Color.white;
        }
        else
        {
            FirstPlayerCellExample.GetComponent<SpriteRenderer>().material.color = Color.white;
            SecondPlayerCellExample.GetComponent<SpriteRenderer>().material.color = Color.black;
        }
    }

    public void ClickIfCellIsAvailable(Vector3 point)
    {
        float topLeftX = -5.75f;
        float topLeftY = 5.65f;
        int x = Mathf.RoundToInt(((point.x - topLeftX) / 1.25f) - 1);
        int y = Mathf.RoundToInt(((topLeftY - point.y) / 1.25f) - 1);
        Debug.Log("x = " + x );
        Debug.Log("y = " + y);
        var cells = GameManager.GetAvailableCells();
        foreach(var cell in cells)
        {
            if (cell.Item1 == y && cell.Item2 == x)
            {
                GameManager.MakeMove(cell);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("wp.x = " + worldPoint.x);
            Debug.Log("wp.y = " + worldPoint.y);
            ClickIfCellIsAvailable(worldPoint);
        }
    }
}
