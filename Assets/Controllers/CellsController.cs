using Assets.Models;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class CellsController : MonoBehaviour
{
    public static Player FirstPlayer;//sets is StartSceneManager

    public static Player SecondPlayer;//sets is StartSceneManager

    private bool isFirstPlayerTurn = true;

    public GameObject FirstPlayerCellExample;

    public GameObject SecondPlayerCellExample;
    public GameManager GameManager { get; set; }

    public Button PassButton;

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
        GameLoop();

        GameManager.CalculatePlayersScore();
    }

    public void GameLoop()
    {
        //pass if needed
        if (GameManager.GetAvailableCells().Count == 0 && ( isFirstPlayerTurn || ( !isFirstPlayerTurn && SecondPlayer is HumanPlayer)))
        {
            PassButton.gameObject.SetActive(true);
        }

        //makes moves in turn
        if (isFirstPlayerTurn)
        {
            if(FirstPlayer.MakeMove())
                isFirstPlayerTurn = !isFirstPlayerTurn;
        }
        else
        {
            if(SecondPlayer.MakeMove())
                isFirstPlayerTurn = !isFirstPlayerTurn;
        }
    }

    public void Pass()
    {
        GameManager.Pass();

        isFirstPlayerTurn = !isFirstPlayerTurn;

        PassButton.gameObject.SetActive(false);
    }

    public void Restart()
    {
        GameManager.RestartGame();
    }
}
