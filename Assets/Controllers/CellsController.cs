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
}
