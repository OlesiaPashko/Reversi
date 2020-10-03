using Assets.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CellsDrawer : MonoBehaviour
{
    public GameObject Cell;

    public GameObject AvailableCell;

    private List<List<GameObject>> matrix;
    public GameManager GameManager { get; set; }

    public Text FirstPlayerScore;

    public Text SecondPlayerScore;

    private Color emptyCellColor;

    private Color availableCellColor;
    public void Start()
    {
        ColorUtility.TryParseHtmlString("#FD46E4", out availableCellColor);
        ColorUtility.TryParseHtmlString("#CC5F5F", out emptyCellColor);
    }
    public void ListenTo(GameManager gameManager)
    {
        GameManager = gameManager;
        GameManager.GameStarted += GameStarted;
        GameManager.MoveMade += UpdateMatrix;
        GameManager.AvailableCellsCalculated += DisplayAvailableCells;
        GameManager.ScoresCalculated += DisplayScore;
        GameManager.GameRestarted += RestartGame;
        GameManager.GameFinished += GameFinished;
    }

    public void GameStarted(List<List<Cell>> cells)
    {
        SetMatrix(cells);

        UpdateMatrix(cells);
    }

    public void DisplayAvailableCells(List<Tuple<int, int>> coords)
    {
        foreach (var coord in coords)
        {
            matrix[coord.Item1][coord.Item2].GetComponent<SpriteRenderer>().material.color = availableCellColor;
        }
    }

    private void SetMatrix(List<List<Cell>> cells)
    {
        matrix = new List<List<GameObject>>();
        for(int i = 0; i < cells.Count; i++)
        {
            matrix.Add(new List<GameObject>());
            for(int j = 0;j<cells.Count; j++)
            {
                float topLeftX = -5.75f;
                float topLeftY = 5.65f;
                float yPos = topLeftY - 1.25f * (i + 1);
                float xPos = topLeftX + 1.25f * (j + 1);

                GameObject element = Instantiate(Cell, new Vector2(xPos, yPos), transform.rotation);

                matrix[i].Add(element);
            }
        }
    }

    private void UpdateMatrix(List<List<Cell>> cells)
    {
        for (int i = 0; i < matrix.Count; i++)
        {
            for (int j = 0; j < matrix.Count; j++)
            {
                SetAppropriateColor(cells[i][j], matrix[i][j]);
            }
        }
    }

    private void SetAppropriateColor(Cell cell, GameObject matrixElement)
    {
        if (cell.State == CellState.Black)
            matrixElement.GetComponent<SpriteRenderer>().material.color = Color.black;
        else if (cell.State == CellState.White)
            matrixElement.GetComponent<SpriteRenderer>().material.color = Color.white;
        else
            matrixElement.GetComponent<SpriteRenderer>().material.color = emptyCellColor;
    }
    public void DisplayScore(int firstPlayerScore, int secondPlayerScore)
    {
        FirstPlayerScore.text = firstPlayerScore.ToString();
        SecondPlayerScore.text = secondPlayerScore.ToString();
    }

    public void GameFinished(int firstPlayerScore, int secondPlayerScore)
    {
        FinishSceneManager.FirstPlayerScore = firstPlayerScore;
        FinishSceneManager.SecondPlayerScore = secondPlayerScore;
        SceneManager.LoadScene("FinishScene");
    }

    private void RestartGame()
    {
        FirstPlayerScore.text = "2";
        SecondPlayerScore.text = "2";
        matrix = new List<List<GameObject>>();
        SceneManager.LoadScene("StartScene");
    }
}
