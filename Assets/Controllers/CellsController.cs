using Assets.Models;
using UnityEditor.PackageManager;
using UnityEngine;

public class CellsController : MonoBehaviour
{
    public static Player FirstPlayer;

    public static Player SecondPlayer;

    public GameObject FirstPlayerCellExample;

    public GameObject SecondPlayerCellExample;
    public GameManager GameManager { get; set; }

    public void StartGame()
    {
        FirstPlayer.GameManager = GameManager;
        SecondPlayer.GameManager = GameManager;
        SetPlayersColors();
        GameManager.StartGame(FirstPlayer.Color);
    }

    
    private void SetPlayersColors()
    {
        if(FirstPlayer.Color == CellState.Black)
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

    

    void Update()
    {
        if (FirstPlayer.MakeMove())
        {
            SecondPlayer.MakeMove();
        }

    }

    public void Restart()
    {
        GameManager.RestartGame();
    }
}
